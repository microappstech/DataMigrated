using Microsoft.Data.SqlClient;

using Migratedata.Data;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Migratedata
{
    public partial class DbSturcture : Form
    {
        List<string> SrcTables;
        List<string> DestTables;
        string ConnectionStringSrc, ConnectionStringDest;
        IDbStructureService dbStructureService;
        public DbSturcture(List<string> srcTables, List<string> destTables, string ConnectionStringSrc, string ConnectionStringDest)
        {
            InitializeComponent();
            this.ConnectionStringSrc = ConnectionStringSrc;
            this.ConnectionStringDest = ConnectionStringDest;
            this.SrcTables = srcTables;
            this.DestTables = destTables;
            dbStructureService = new DbStructureService();
        }

        private void DbSturcture_Load(object sender, EventArgs e)
        {
            PBAction.Minimum = 0;
            PBAction.Maximum = 100;
            PBAction.Value= 0;
            PBAction.Step = 1;
            PBAction.Style = ProgressBarStyle.Continuous;



            BtnCopydata.Text = Variables.MigrationType == MigrationType.Schema ? "Create Database" : Variables.MigrationType == MigrationType.Data ? "Copy Data" : "Create Db & Copy";
            tablesList.Items.Clear();
            LbDestTables.Items.Clear();
            for (int i = 0; i < SrcTables.Count; i++)
            {
                tablesList.Items.Add(SrcTables[i]);
                if (chSelectAll.Checked)
                    tablesList.SetSelected(i, true);
            }
            for (int i = 0; i < DestTables.Count; i++)
            {
                LbDestTables.Items.Add(DestTables[i]);
                LbDestTables.SetSelected(i, true);
            }

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chSelectAll.Checked = false;
        }

        private void Next_Click(object sender, EventArgs e)
        {
            var ScriptTables = GenerateScriptTables(ConnectionStringSrc, tablesList.SelectedItems.Cast<string>());

        }

        private void SelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chSelectAll.Checked)
                for (int i = 0; i < tablesList.Items.Count; i++)
                {
                    //tablesList.Items[i].Checked = true;
                    tablesList.SetSelected(i, true);
                }
            else
                for (int i = 0; i < tablesList.Items.Count; i++)
                {
                    //tablesList.Items[i].Checked = true;
                    tablesList.SetSelected(i, false);
                }
        }
        string GenerateScriptTables(string connectionString, IEnumerable<string> TableNames)
        {
            StringBuilder FullQuery = new StringBuilder();
            foreach (string TableName in TableNames)
            {
                var tableScript = GenerateCreateTableSQL(connectionString, TableName);
                FullQuery.Append(tableScript);
            }
            return FullQuery.ToString();
        }

        static string GenerateCreateTableSQL(string connectionString, string tableName)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine($"CREATE TABLE {tableName} (");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $@"
                SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE, COLUMN_DEFAULT
                FROM INFORMATION_SCHEMA.COLUMNS 
                WHERE TABLE_NAME = @TableName";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string columnName = reader["COLUMN_NAME"].ToString();
                            string dataType = reader["DATA_TYPE"].ToString();
                            string maxLength = reader["CHARACTER_MAXIMUM_LENGTH"].ToString();
                            string isNullable = reader["IS_NULLABLE"].ToString();
                            string columnDefault = reader["COLUMN_DEFAULT"]?.ToString();

                            if (!string.IsNullOrEmpty(maxLength) && maxLength != "-1")
                            {
                                dataType += $"({maxLength})";
                            }else if(maxLength == "-1" && dataType.ToLower().Contains("varchar"))
                            {
                                dataType += $"(MAX)";
                            }

                            sql.Append($"    {columnName} {dataType}");

                            sql.Append(isNullable == "YES" ? " NULL" : " NOT NULL");

                            if (!string.IsNullOrEmpty(columnDefault))
                            {
                                sql.Append($" DEFAULT {columnDefault}");
                            }

                            sql.AppendLine(",");
                        }
                    }
                }
                connection.Close();
            }

            sql.Length -= 3;
            sql.AppendLine("\n);");

            return sql.ToString();
        }

        private void tablesLlist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnCopydata_Click(object sender, EventArgs e)
        {
            try { 
            
                PBAction.Value = 0;

                if (Variables.MigrationType == MigrationType.Data)
                {
                    PBAction.Style = ProgressBarStyle.Marquee;
                    var res = dbStructureService.CopyData(SrcTables, DestTables, ConnectionStringSrc, ConnectionStringDest);
                    PBAction.Value = 100;
                    if (res)
                    {
                        MessageBox.Show("Data copied successfully", "Success");
                    }
                    else
                    {
                        MessageBox.Show($"Data copied failed ", "Error");
                    }

                }
                else if (Variables.MigrationType == MigrationType.Schema)
                {
                    var res = dbStructureService.Createdatabase("Test", ConnectionStringSrc, GenerateScriptTables(ConnectionStringSrc, tablesList.SelectedItems.Cast<string>()));
                    PBAction.Value = 100;
                    if (res)
                    {
                        MessageBox.Show("Database created successfully", "Success");
                    }
                    else
                    {
                        MessageBox.Show("Database creation failed", "Error");
                    }
                }
                else
                {
                    using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var res = dbStructureService.Createdatabase("Test", ConnectionStringSrc, GenerateScriptTables(ConnectionStringSrc, tablesList.SelectedItems.Cast<string>()));
                        if (!res)
                        {
                            MessageBox.Show("Database creation failed", "Error");
                            throw new Exception();
                        }
                        var res2 = dbStructureService.CopyData(SrcTables, DestTables, ConnectionStringSrc, ConnectionStringDest);

                        PBAction.Value = 100;
                        if (!res2)
                        {
                            MessageBox.Show("Data copied failed", "Error");
                            throw new Exception();
                        }
                        scope.Complete();
                    }
                }
            }
            finally
            {
                PBAction.Value = 100;
                PBAction.Style = ProgressBarStyle.Continuous;
            }
        }

        private void PBAction_Click(object sender, EventArgs e)
        {

        }
    }

}

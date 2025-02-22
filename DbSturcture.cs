using Microsoft.Data.SqlClient;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Migratedata
{
    public partial class DbSturcture : Form
    {
        List<string> Tables;
        List<string> SelecetdTables;
        string ConnectionStringSrc, ConnectionStringDest;
        public DbSturcture(List<string> tables, string ConnectionStringSrc,string ConnectionStringDest)
        {
            InitializeComponent();
            this.ConnectionStringSrc = ConnectionStringSrc;
            this.ConnectionStringDest = ConnectionStringDest;
            this.Tables = tables;
        }

        private void DbSturcture_Load(object sender, EventArgs e)
        {
            tablesList.Items.Clear();
            tablesList.Items.AddRange(Tables.ToArray());
            tablesList.SelectionMode =SelectionMode.MultiSimple;
             if(chSelectAll.Checked)
                for(int i=0 ; i < Tables.Count ; i++)
                {
                    tablesList.SetSelected(i, true);
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
                    tablesList.SetSelected(i, true);
                }
            else
                for (int i = 0; i < tablesList.Items.Count; i++)
                {
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
    }

}

using Microsoft.Data.SqlClient;

using System.Data.Common;
using System.Linq.Expressions;



namespace Migratedata
{
    public partial class Form1 : Form
    {
        private readonly IDbStructureService _dbStructureService;
        public List<string> Databases { get; set; } = new List<string>();
        public List<string> Tables { get; set; } = new List<string>();
        string ConnectionStringSrc, ConnectionStringDest;
        public Form1(IDbStructureService bStructureService)
        {
            InitializeComponent();
            _dbStructureService = bStructureService;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ServerTypeDest.Items.Add(ServerType.MSSQL);
            ServerTypeDest.Items.Add(ServerType.MySQL);

            ServerTyppSrc.Items.Add(ServerType.MSSQL);
            ServerTyppSrc.Items.Add(ServerType.MySQL);

            SeverSource.Enabled = serverDest.Enabled = DbNameSrc.Enabled = DbNameDest.Enabled
                = BtnDest.Enabled = BtnTestSource.Enabled = false;
            SeverSource.Text = "DESKTOP-3M7KJOK\\SQLEXPRESS01";



        }

        private void SeverSource_TextChanged(object sender, EventArgs e)
        {
            string _connectionString = $"Server={SeverSource.Text};Integrated Security=True;TrustServerCertificate=True";
            

            try
            {
                var dbs=_dbStructureService.GetDatabases(ConnectionStringSrc);
                DbNameSrc.Enabled = true;
                DbNameSrc.Items.AddRange(dbs.ToArray());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ServerTyppSrc_ValueMemberChanged(object sender, EventArgs e)
        {


        }

        private void ServerTyppSrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ServerTyppSrc.SelectedItem != "" && ServerTyppSrc.SelectedItem != null)
                SeverSource.Enabled = true;
        }

        private void DbNameSrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DbNameSrc.SelectedItem != null)
                BtnTestSource.Enabled = true;

        }

        private void BtnTestSource_Click(object sender, EventArgs e)
        {
            string _connectionString = $"Server={SeverSource.Text};Database={DbNameSrc.Text};Integrated Security=True;TrustServerCertificate=True";
            ConnectionStringSrc = _connectionString;

            try
            {
                var test = _dbStructureService.GetTables(ConnectionStringSrc);
                Tables = test.ToList();
                MessageBox.Show("Connection succeful etablished", "Success");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void DbNameSrc_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (DbNameSrc.SelectedItem != null)
                BtnTestSource.Enabled = true;
        }

        private void serverDest_TextChanged(object sender, EventArgs e)
        {
            string _connectionString = $"Server={SeverSource.Text};Integrated Security=True;TrustServerCertificate=True";

            try
            {
                var databases = _dbStructureService.GetDatabases(_connectionString);
                //using (SqlConnection connection = new SqlConnection(_connectionString))
                //{
                //    if (connection.State == System.Data.ConnectionState.Open) connection.Close();
                //    connection.Open();
                //    string query = "SELECT name FROM sys.databases where owner_sid != 0x01";

                //    using (SqlCommand command = new SqlCommand(query, connection))
                //    using (SqlDataReader reader = command.ExecuteReader())
                //    {
                //        while (reader.Read())
                //            if (reader["name"] != DBNull.Value)
                //            {
                //                string dbname = reader.GetString(0);
                //                databases.Add(dbname);
                //            }
                //    }
                //    connection.Close();
                //}
                DbNameDest.Enabled = true;
                DbNameDest.Items.AddRange(databases.ToArray());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ServerTypeDest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ServerTypeDest.SelectedItem != "" && ServerTypeDest.SelectedItem != null)
                serverDest.Enabled = true;
        }

        private void DbNameDest_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DbNameDest.SelectedItem != null)
                BtnDest.Enabled = true;
        }

        private void BtnDest_Click(object sender, EventArgs e)
        {
            var tables = new List<string>();
            string _connectionString = $"Server={SeverSource.Text};Database={DbNameSrc.Text};Integrated Security=True;TrustServerCertificate=True";
            try
            {
                tables = _dbStructureService.GetTables(_connectionString).ToList();
                //using (SqlConnection connection = new SqlConnection(_connectionString))
                //{
                //    if (connection.State == System.Data.ConnectionState.Open) connection.Close();
                //    connection.Open();
                //    using (SqlCommand cmd = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';", connection))
                //    using (SqlDataReader reader = cmd.ExecuteReader())
                //    {
                //        while (reader.Read())
                //        {
                //            string table = reader.GetString(0);
                //            tables.Add(table);
                //        }
                //    }

                //}
                MessageBox.Show("Connection succeful etablished", "Success");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnMigration_Click(object sender, EventArgs e)
        {
            DbSturcture dbSturctureForm = new DbSturcture(Tables, ConnectionStringSrc, ConnectionStringDest);
            dbSturctureForm.Show();
        }
    }
    public enum ServerType
    {
        MSSQL=1,
        MySQL=2
    }
}

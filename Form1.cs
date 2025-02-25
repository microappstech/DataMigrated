using Microsoft.Data.SqlClient;

using Migratedata.Data;

using System.Data.Common;
using System.Linq.Expressions;



namespace Migratedata
{
    public partial class Form1 : Form
    {
        private readonly IDbStructureService _dbStructureService;
        public List<string> Databases { get; set; } = new List<string>();
        public List<string> DestTables { get; set; } = new List<string>();
        public List<string> SrcTables { get; set; } = new List<string>();
        string ConnectionStringSrc, ConnectionStringDest;
        bool srcvalid, Destvalid;
        public Form1(IDbStructureService bStructureService)
        {
            InitializeComponent();
            _dbStructureService = bStructureService;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CBMigrationType.Items.Add(MigrationType.Data);
            CBMigrationType.Items.Add(MigrationType.Schema);
            CBMigrationType.Items.Add(MigrationType.DataSchema);
            CBMigrationType.SelectedIndex = 0;
            ServerTypeDest.Items.Add(ServerType.MSSQL);
            ServerTypeDest.Items.Add(ServerType.MySQL);

            ServerTyppSrc.Items.Add(ServerType.MSSQL);
            ServerTyppSrc.Items.Add(ServerType.MySQL);

            SeverSource.Enabled = serverDest.Enabled = DbNameSrc.Enabled = DbNameDest.Enabled
                = BtnDest.Enabled = BtnTestSource.Enabled = false;
            SeverSource.Text = "DESKTOP-3M7KJOK\\SQLEXPRESS01";
            btnMigration.Enabled = false;


        }

        private void SeverSource_TextChanged(object sender, EventArgs e)
        {
            string _connectionString = $"Server={SeverSource.Text};Integrated Security=True;TrustServerCertificate=True";
            try
            {
                var dbs = _dbStructureService.GetDatabases(_connectionString, Variables.SourceServer);
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

            Variables.SourceServer = (ServerType)ServerTyppSrc.SelectedItem;
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
                var test = _dbStructureService.GetTables(ConnectionStringSrc, Variables.SourceServer);
                SrcTables = test.ToList();
                srcvalid = true;
                if(Variables.MigrationType == MigrationType.Schema || Variables.MigrationType == MigrationType.DataSchema)
                    btnMigration.Enabled = true;
                else
                    btnMigration.Enabled =  srcvalid && Destvalid;

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
            string _connectionString = $"Server={serverDest.Text};";
            try
            {
                var databases = _dbStructureService.GetDatabases(_connectionString, Variables.DestServer);
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

            Variables.DestServer = (ServerType)ServerTypeDest.SelectedItem;
        }

        private void DbNameDest_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DbNameDest.SelectedItem != null)
                BtnDest.Enabled = true;
        }

        private void BtnDest_Click(object sender, EventArgs e)
        {
            var tables = new List<string>();
            string _connectionString = $"Server={SeverSource.Text};Database={DbNameDest.Text};Integrated Security=True;TrustServerCertificate=True";
            ConnectionStringDest = _connectionString;
            try
            {
                tables = _dbStructureService.GetTables(_connectionString, Variables.DestServer).ToList();
                DestTables = tables;
                Destvalid = true;
                btnMigration.Enabled = srcvalid && Destvalid;
                MessageBox.Show("Connection succeful etablished", "Success");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnMigration_Click(object sender, EventArgs e)
        {
            DbSturcture dbSturctureForm = new DbSturcture(SrcTables, DestTables, ConnectionStringSrc, ConnectionStringDest);
            dbSturctureForm.Show();
        }

        private void CBMigrationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(CBMigrationType.SelectedItem != null && (MigrationType)CBMigrationType.SelectedItem != MigrationType.Data)
            {
                DestTables.Clear();
                BtnDest.Enabled = false;
                ServerTypeDest.Enabled = false;
                serverDest.Enabled = false;
                DbNameDest.Enabled = false;
            }
                Variables.MigrationType = (MigrationType)CBMigrationType.SelectedItem;
        }
    }

}

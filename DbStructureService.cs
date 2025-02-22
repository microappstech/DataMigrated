using Microsoft.Data.SqlClient;

using System;
using System.Data;
using System.Text;

using static System.Net.Mime.MediaTypeNames;

public class DbStructureService: IDbStructureService
{
    public string connectionStringSource { get; private set; }
    public string connectionStringDest { get; private set; }
    
    string IDbStructureService.GenerateTablesStructure(IEnumerable<string> tablesName, string connection)
    {
        StringBuilder fullScript = new StringBuilder();
        foreach (string tableName in tablesName)
        {
            string script = this.GenerateTableStructure(tableName, connection);
        }
        return fullScript.ToString();
    }

    public string GenerateTableStructure(string tableName ,string connectionString)
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


    public IEnumerable<string> GetDatabases(string connectionString)
    {
        List<string> Databases = new List<string>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            if (connection.State == System.Data.ConnectionState.Open) connection.Close();
            connection.Open();
            string query = "SELECT name FROM sys.databases where owner_sid != 0x01";

            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                    if (reader["name"] != DBNull.Value)
                    {
                        string dbname = reader.GetString(0);
                        Databases.Add(dbname);
                    }
            }
            connection.Close();
        }
        return Databases;
    }

    public IEnumerable<string> GetTables(string connectionString)
    {
        List<string> Tables = new List<string>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            if (connection.State == System.Data.ConnectionState.Open) connection.Close();
            connection.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';", connection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string table = reader.GetString(0);
                    Tables.Add(table);


                }
            }
            connection.Close();
        }
        return Tables;
    }
    
    public void InitializeConnectionStrings(string connectionStringSrc, string connectionStringDest)
    {
        this.connectionStringSource = connectionStringSrc;
        this.connectionStringDest = connectionStringDest;

    }
    public IEnumerable<string> GeSrctDatabases()
    {
        var dbs = this.GetDatabases(connectionStringSource);
        return dbs;
    }
    public IEnumerable<string> GeDesttDatabases()
    {
        var dbs = this.GetDatabases(connectionStringDest);
        return dbs;
    }
    public IEnumerable<string> GetSrcTables() 
    {
        var tabs = GetTables(connectionStringSource);
        return tabs;
    }
    public IEnumerable<string> GetDestTables()
    {
        var tabs = GetTables(connectionStringDest);
        return tabs;
    }
    public IEnumerable<string> GetColumns(string connectionString, string tableName)
    {
        List<string> Columns = new List<string>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            if (connection.State == System.Data.ConnectionState.Open) connection.Close();
            connection.Open();
            string query = $@"
                SELECT COLUMN_NAME
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
                        Columns.Add(columnName);
                    }
                }
            }
            connection.Close();
        }
        return Columns;
    }
    bool IDbStructureService.CopyData(IEnumerable<string> TablesSrc, IEnumerable<string> TablesDest, string connectionStringSrc, string connectionStringDest)
    {
        try
        {
            if (TablesSrc.Count() != TablesDest.Count())
            {
                MessageBox.Show("The number of tables in the source and destination tables do not match.");
                return false;
            }
            TablesDest.Order();
            TablesSrc.Order();
            for (int i = 0; i < TablesSrc.Count(); i++)
            {
                string TableSrc = TablesSrc.ElementAt(i);
                string TableDest = TablesDest.ElementAt(i);
                var srcColumns = GetColumns(connectionStringSrc, TableSrc);
                var destColumns = GetColumns(connectionStringDest, TableDest);
                if (srcColumns.Count() != destColumns.Count())
                {
                    MessageBox.Show("The number of columns in the source and destination tables do not match.");
                    return false;
                }
                DataTable dataTable = new DataTable();
                using (SqlConnection srcConn = new SqlConnection(connectionStringSrc))
                {
                    srcConn.Open();
                    string query = $"SELECT * FROM {TableSrc}";
                    using (SqlCommand cmd = new SqlCommand(query, srcConn))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }

                using (SqlConnection destConn = new SqlConnection(connectionStringDest))
                {
                    destConn.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destConn))
                    {
                        bulkCopy.DestinationTableName = TableDest;
                        bulkCopy.WriteToServer(dataTable);
                    }
                }
            }

            return true; // Success
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error copying data: {ex.Message}");
            return false; // Failure
        }

        throw new NotImplementedException();
    }
    public bool Createdatabase(string databaseName, string connectionString , string Script)
    {
        try
        {
            Guid newGuid = Guid.NewGuid();
            databaseName=$"{databaseName}_{newGuid.ToString().Substring(5)}".Replace("-","_");
            var connectparts = connectionString.Split(';').ToList();
            var DbPart = connectparts.Where(onnectparts => onnectparts.Contains("Database")).FirstOrDefault();
            DbPart = $"Database={databaseName}_{newGuid.ToString().Substring(5)}";
            connectparts = connectparts.Where(onnectparts => !onnectparts.Contains("Database")).ToList();
            //connectparts.Insert(1,DbPart);
            connectionString = string.Join(";", connectparts);

            string createDb = $"CREATE DATABASE {databaseName}; ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(createDb, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                }
                finally
                {
                    conn.Close();
                }
            }
            string sqlScript = $"USE {databaseName}; {Script}";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlScript, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                }
                finally
                {
                    conn.Close();
                }
                return true;
            }
        }
        catch(Exception ex)
        {
            MessageBox.Show($"Error creating database: {ex.Message}");
            return false;
        }
    }

}

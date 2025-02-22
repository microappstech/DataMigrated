using Microsoft.Data.SqlClient;

using System;
using System.Text;

public class DbStructureService: IDbStructureService
{
    public string connectionStringSource { get; private set; }
    public string connectionStringDest { get; private set; }
    
    string IDbStructureService.GenerateTablesStructure(IEnumerable<string> tablesName)
    {
        StringBuilder fullScript = new StringBuilder();
        foreach (string tableName in tablesName)
        {
            string script = this.GenerateTableStructure(tableName);
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
}

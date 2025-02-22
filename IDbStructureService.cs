using System;

public interface IDbStructureService
{
	string GenerateTableStructure(string tableName, string connectionString);
	string GenerateTablesStructure(IEnumerable<string> tablesName, string connection);
	IEnumerable<string> GetDatabases(string connectionString);
	IEnumerable<string> GetTables(string connectionString);
	IEnumerable<string> GetColumns(string connectionString, string tableName);
    bool CopyData(IEnumerable<string> TableSrc, IEnumerable<string> TableDest, string connectionStringSrc, string connectionStringDest);
	bool Createdatabase(string databaseName, string connectionString, string Script);
}


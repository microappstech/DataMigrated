using Migratedata.Data;

using System;

public interface IDbStructureService
{
	string GenerateTableStructure(string tableName, string connectionString, ServerType serverType);
	string GenerateTablesStructure(IEnumerable<string> tablesName, string connection, ServerType serverType);
	IEnumerable<string> GetDatabases(string connectionString, ServerType serverType);
	IEnumerable<string> GetTables(string connectionString, ServerType serverType);
	IEnumerable<string> GetColumns(string connectionString, string tableName, ServerType serverType);
    bool CopyData(IEnumerable<string> TableSrc, IEnumerable<string> TableDest, string connectionStringSrc, string connectionStringDest);
	bool Createdatabase(string databaseName, string connectionString, string Script);
}


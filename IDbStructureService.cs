using System;

public interface IDbStructureService
{
	string GenerateTableStructure(string tableName, string connectionString);
	string GenerateTablesStructure(IEnumerable<string> tablesName);
	IEnumerable<string> GetDatabases(string connectionString);
	IEnumerable<string> GetTables(string connectionString);
	private void TestPrivate()
	{

	}
}


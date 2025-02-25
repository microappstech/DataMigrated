using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migratedata.Data
{
    public class Procedurescs
    {
        public static string getQuery(string procName, ServerType serverType)
        {
            if(serverType.Equals(ServerType.MSSQL))
                return sqlProc[procName].ToString()!;
            else if(serverType.Equals(ServerType.MySQL))
                return MysqlProc[procName].ToString()!;
            return "";
        }
        public static Hashtable sqlProc = new Hashtable();
        public static Hashtable MysqlProc = new Hashtable();

        public Procedurescs()
        {
            sqlProc["GetTableStructure"] = $@"SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE, COLUMN_DEFAULT
                FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName";
            sqlProc["GetDatabases"] = "SELECT name FROM sys.databases where owner_sid != 0x01";
            sqlProc["GetTables"] = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';";
            sqlProc["GetColumns"] = $@"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName";
        }
    }
}

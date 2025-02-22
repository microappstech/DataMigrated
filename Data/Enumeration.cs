using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migratedata.Data
{
    public enum MigrationType
    {
        Schema= 0,
        Data = 1,
        DataSchema = 2
    }
    public enum ServerType
    {
        MSSQL = 1,
        MySQL = 2
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migratedata.Data
{
    public static class Variables
    {
        public static MigrationType MigrationType { get; set; }
        public static ServerType SourceServer { get; set; }
        public static ServerType DestServer { get; set; }
    }

}

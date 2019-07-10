using System;
using System.Collections.Generic;
using System.Linq;
using HB.Framework.Database;

namespace HB.Infrastructure.MySQL
{
    public class MySQLOptions
    {
        public DatabaseSettings DatabaseSettings { get; set; } = new DatabaseSettings();

        public IList<SchemaInfo> Schemas { get; } = new List<SchemaInfo>();

    }
}

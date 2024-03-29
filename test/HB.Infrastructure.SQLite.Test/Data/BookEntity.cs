﻿using HB.Framework.Database.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HB.Infrastructure.SQLite.Test
{
    public class BookEntity : DatabaseEntity
    {
        [UniqueGuidEntityProperty]
        public string Guid { get; set; }

        [EntityProperty]
        public string Name { get; set; }

        [EntityProperty]
        public double Price { get; set; }
    }
}

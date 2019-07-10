﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HB.Framework.Database.Entity
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EntitySchemaAttribute : Attribute
    {
        public string DatabaseName { get; set; }

        public string TableName { get; set; }

        public string Description { get; set; }

        public bool ReadOnly { get; set; }

        public string SuffixToRemove = "Entity";

        public EntitySchemaAttribute(string databaseName)
        {
            DatabaseName = databaseName;
        }
    }
}

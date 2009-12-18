﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace Migrator.Fluent
{
    public class TableRemoveAction : TableAction
    {
        public TableRemoveAction(SchemaBuilder database, string name) : base(database, name) { }

        public override void Execute(ITransformationProvider provider)
        {
            provider.RemoveTable(this.Name);
            base.Execute(provider);
        }
    }
}
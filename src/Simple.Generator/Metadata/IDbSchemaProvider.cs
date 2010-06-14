﻿//
//  Data.Common.DbSchema - http://dbschema.codeplex.com
//
//  The contents of this file are subject to the New BSD
//  License (the "License"); you may not use this file
//  except in compliance with the License. You may obtain a copy of
//  the License at http://www.opensource.org/licenses/bsd-license.php
//
//  Software distributed under the License is distributed on an 
//  "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or
//  implied. See the License for the specific language governing
//  rights and limitations under the License.
//


using System.Data;
using System;
using System.Collections.Generic;

namespace Simple.Metadata
{
    public interface IDbSchemaProvider : IDisposable
    {
        MetaContext Context { get; }

        IEnumerable<DbTable> GetTables(IList<string> includedTables, IList<string> excludedTables);
        IEnumerable<DbRelation> GetConstraints(IList<string> includedTables, IList<string> excludedTables);
        IEnumerable<DbColumn> GetColumns(DbTableName table);
        string QualifiedTableName(DbTableName table);
        
        DbType GetDbColumnType(string providerDbType);
    }
}

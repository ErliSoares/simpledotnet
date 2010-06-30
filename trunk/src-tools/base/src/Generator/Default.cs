﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.IO;
using Simple.NVelocity;
using Simple.Metadata;
using System.Collections;

namespace Sample.Project.Generator
{
    public static class Default
    {
        public const string DefaultNamespace = "Sample.Project";
        public const string ContractsAssembly = DefaultNamespace + ".Contracts";
        public const bool LazyLoad = true;

        public const string ToolsProject = "src/Generator/??_Generator.csproj";
        public const string ContractsProject = "src/Contracts/??_Contracts.csproj";
        public const string ServerProject = "src/Server/??_Server.csproj";

        public static IEnumerable<string> TableNames
        {
            get
            {
                yield return "%";
                yield return "-SchemaInfo";
            }
        }

        public static Conventions Convention
        {
            get { return new Conventions(); }
        }

        public static ProjectFileWriter Writer(this string str)
        {
            var file = Directory.GetFiles(".", str).First();
            return new ProjectFileWriter(file).AutoCommit();
        }

        public static SimpleTemplate SetDefaults(this SimpleTemplate template, DbTable table)
        {
            var re = Convention;
            return template.SetMany(new
            {
                re = re,
                table = table,
                lazyload = LazyLoad,
                @namespace = DefaultNamespace,
                contracts = ContractsAssembly,
                classname = re.NameFor(table),
                count = new Func<IEnumerable, int>(x => x.Cast<object>().Count()),
            });
        }

    }
}
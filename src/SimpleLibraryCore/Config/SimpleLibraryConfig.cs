﻿using System;
using System.Collections.Generic;

using System.Text;
using BasicLibrary.Configuration;

namespace SimpleLibrary.Config
{
    [DefaultFile("SimpleLibrary.config", false)]
    public class SimpleLibraryConfig : ConfigRoot<SimpleLibraryConfig>
    {
        [ConfigElement("business", Required = true)]
        public BusinessElement Business { get; set; }

        [ConfigElement("serviceModel", Required = true)]
        public ServiceModelElement ServiceModel { get; set; }

        [ConfigElement("dataConfig", Required = true)]
        public DataConfigElement DataConfig { get; set; }

        [ConfigElement("threading", Default=InstanceType.New)]
        public ThreadingElement Threading { get; set; }

        public override string DefaultXmlString
        {
            get
            {
                return DefaultConfigContent.SimpleLibraryConfig;
            }
        }
    }
}
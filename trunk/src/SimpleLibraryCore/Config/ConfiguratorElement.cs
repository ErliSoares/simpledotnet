﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicLibrary.Configuration;

namespace SimpleLibrary.Config
{
    public enum SideType
    {
        Client,
        Server,
        Both
    }

    public class ConfiguratorElement : TypeConfigElement
    {
        [ConfigElement("runAt",Default=SideType.Both)]
        public SideType RunAt { get; set; }
    }
}

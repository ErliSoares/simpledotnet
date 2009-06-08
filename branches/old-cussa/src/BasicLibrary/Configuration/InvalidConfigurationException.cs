﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace BasicLibrary.Configuration
{
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException() : base() { }
        public InvalidConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public InvalidConfigurationException(string message) : base(message) { }
    }
}

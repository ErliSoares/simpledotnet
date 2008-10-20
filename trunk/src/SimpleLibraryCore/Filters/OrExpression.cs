﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SimpleLibrary.Filters
{
    [DataContract]
    public class OrExpression : BinaryOperator
    {
        public OrExpression(Filter filter1, Filter filter2) : base(filter1, filter2) { }
    }
}

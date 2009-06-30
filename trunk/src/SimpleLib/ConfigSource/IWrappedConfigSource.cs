﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.ConfigSource
{
    public interface IWrappedConfigSource<T, A> : IConfigSource<T, IConfigSource<A>>
    {
    }

    public interface IWrappedConfigSource<T> : IWrappedConfigSource<T, T>
        { }
}

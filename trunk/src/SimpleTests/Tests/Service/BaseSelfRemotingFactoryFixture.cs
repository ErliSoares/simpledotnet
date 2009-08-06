﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Services.Remoting;
using Simple.ConfigSource;
using Simple.Services;
using NUnit.Framework;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace Simple.Tests.Service
{
    public abstract class BaseSelfRemotingFactoryFixture : BaseFactoryFixture
    {
        public abstract Uri Uri { get; }

        protected override Guid Configure()
        {
            Guid guid = Guid.NewGuid();

            RemotingSimply.Do.Configure(guid,
                XmlConfig.LoadXml<RemotingConfig>(Helper.MakeConfig(Uri)));
            Simply.Get(guid).Host(typeof(SimpleService));

            return guid;
        }

        protected override void Release(Guid guid)
        {
            Simply.Get(guid).StopServer();
            RemotingSimply.Do.ReleaseConfig(guid);
        }
    }
}

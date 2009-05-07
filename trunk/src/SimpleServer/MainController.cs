﻿using System;
using System.Collections.Generic;

using System.Text;
using System.Reflection;
using Simple.ServiceModel;
using Simple.DataAccess;
using Simple.ServiceModel;
using Simple.Logging;
using log4net;
using System.Diagnostics;

namespace Simple
{
    public class MainController
    {
        protected static ILog Logger = MainLogger.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        public static void Run(Assembly assembly)
        {
            NHibernate.ByteCode.LinFu.LazyInitializer.Equals(null, null);

            Logger.Info("Initializing server framework...");
            AssemblyLocatorHoster hoster = new AssemblyLocatorHoster();
            hoster.LocateServices(assembly);
            hoster.StartHosting();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using Simple.Reflection;


namespace Simple.Services
{
    public class DefaultInterceptor : IInterceptor
    {
        MethodCache _cache = new MethodCache();
        Func<CallHookArgs, IEnumerable<ICallHook>> Hooks { get; set; }
        bool Client { get; set; }
        IContextHandler HeaderHandler { get; set; }

        public DefaultInterceptor(Func<CallHookArgs, IEnumerable<ICallHook>> hooks, IContextHandler headerHandler, bool client)
        {
            Hooks = hooks;
            Client = client;
            HeaderHandler = headerHandler;
        }

        #region IInterceptor Members

        protected MethodBase CorrectMethod(object target, MethodBase method)
        {
            if (target == null) throw new ArgumentException("server cannot be null");
            Type targetType = target.GetType();
            Type declaringType = method.DeclaringType;
            if (targetType != declaringType && declaringType.IsInterface)
            {
                var map = targetType.GetInterfaceMap(method.DeclaringType);
                for (int i = 0; i < map.InterfaceMethods.Length; i++)
                    if (map.InterfaceMethods[i] == method) return map.TargetMethods[i];
            }
            return method;
        }

        public virtual object Intercept(object target, MethodBase method, object[] args)
        {
            if (!Client) method = CorrectMethod(target, method);
            var hookArgs = new CallHookArgs(target, method, args, Client);
            var methodHooks = Hooks(hookArgs);

            ILog logger = Simply.Do.Log(this);

            var list = new List<ICallHook>(methodHooks);

            try
            {
                foreach (var hook in Enumerable.Reverse(list)) hook.Before();

                logger.DebugFormat("Calling {0} in {1}...", method.Name, method.DeclaringType.Name);

                if (Client) HeaderHandler.InjectCallHeaders(target, method, args);
                else HeaderHandler.RecoverCallHeaders(target, method, args);

                hookArgs.Return = Invoke(target, method, args);

                if (Client) HeaderHandler.RecoverCallHeaders(target, method, args);
                else HeaderHandler.InjectCallHeaders(target, method, args);

                logger.DebugFormat("Returning from {0} in {1}...", method.Name, method.DeclaringType.Name);

                foreach (var hook in list) hook.AfterSuccess();

                return hookArgs.Return;
            }
            finally
            {
                logger.DebugFormat("Finalizing {0} in {1}...", method.Name, method.DeclaringType.Name);

                foreach (var hook in list) hook.Finally();
            }
        }

        protected object Invoke(object target, MethodBase method, object[] args)
        {
            return GetInvocationDelegate(method).Invoke(target, args);
        }

        protected InvocationDelegate GetInvocationDelegate(MethodBase method)
        {
            return _cache.GetInvoker(method);
        }

        #endregion
    }
}

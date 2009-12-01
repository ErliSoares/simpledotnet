﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Collections;

namespace Simple.Services.Remoting
{
    public class CallContextCallHeadersHandler : ICallHeadersHandler
    {
        public void InjectCallHeaders(object target, System.Reflection.MethodBase method, object[] args)
        {
            CallContext.LogicalSetData(this.GetType().GUID.ToString(), CallHeaders.Do);
        }

        public void RecoverCallHeaders(object target, System.Reflection.MethodBase method, object[] args)
        {
            object obj = CallContext.LogicalGetData(this.GetType().GUID.ToString());
            CallHeaders.Force((obj as CallHeaders) ?? new CallHeaders());
            CallContext.FreeNamedDataSlot(this.GetType().GUID.ToString());
        }

    }
}
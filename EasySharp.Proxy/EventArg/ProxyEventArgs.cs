using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace EasySharp.Proxy.EventArg
{
    //todo 继承?
    public class ProxyEventArgs
    {
        public MethodInfo MethodInfo { get; }
        public object[] Args { get; }
        public Exception Exception { get; }
        public ProxyEventArgs(MethodInfo methodInfo, object[] args)
        {
            this.Args = args;
            this.MethodInfo = methodInfo;
        }
        public ProxyEventArgs(MethodInfo methodInfo, object[] args, Exception e)
        {
            this.Args = args;
            this.MethodInfo = methodInfo;
            this.Exception = e;
        }
    }
}

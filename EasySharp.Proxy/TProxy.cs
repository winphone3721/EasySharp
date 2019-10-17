using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EasySharp.Proxy
{
    public class TProxy : DispatchProxy
    {
        // Gets or sets the Action to invoke when clients call methods on the proxy.
        public Func<MethodInfo, object[], object> CallOnInvoke { get; set; }

        // Gets the proxy itself (which is always 'this')
        public object GetProxy()
        {           
            return this;
        }

        // Implementation of DispatchProxy.Invoke() just calls back to given Action
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            //Console.WriteLine(
            //  string.Format("Interceptor does something before invoke [{0}]...", @method));

            //var retObj = @object.GetType().GetMethod(@method).Invoke(@object, parameters);

            //Console.WriteLine(
            //  string.Format("Interceptor does something after invoke [{0}]...", @method));

            //return retObj;
            return CallOnInvoke(targetMethod, args);
        }

        //Type type;
        //public static T Build<T>(Type type)
        //{
        //    Type t = typeof(T);
           
        //    ProxyFactory proxy = new ProxyFactory();
        //    try
        //    {
        //        proxy.serverBase = type.Assembly.CreateInstance(type.ToString()) as ServerBase;
        //    }
        //    catch (System.Exception)
        //    {
        //        throw;
        //    }
        //    if (proxy.serverBase == null)
        //    {
        //        throw new Exception("build 失败！");
        //    }
        //    proxy.type = type;
        //    return proxy;
        //}
    }
}

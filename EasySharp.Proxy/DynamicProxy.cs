using EasySharp.Proxy.Service.Service;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EasySharp.Proxy
{
    public class DynamicProxy
    {
        //todo 增加T,V 约束。
        public static T Proxy<T, V>() => DispatchProxy.Create<T, InvokeProxy<V>>();
        //public static T Proxy<T,V>()
        //{
        //    return DispatchProxy.Create<T, InvokeProxy<V>>();
        //}


        public class InvokeProxy<V> : DispatchProxy
        {
            //todo 测试可去掉 增加拦截器等 
            public Func<object> CallMeLeiFeng { get; set; }
            protected override object Invoke(MethodInfo targetMethod, object[] args)
            {
                var targetType = typeof(V);
                var target = targetType.Assembly.CreateInstance(targetType.ToString());
                var hello = typeof(V).Assembly.CreateInstance(targetType.ToString());
                //CallMeLeiFeng();测试代码可去掉
                if (null != CallMeLeiFeng)
                {
                    CallMeLeiFeng();
                }
           
                return targetType.InvokeMember(targetMethod.Name, BindingFlags.InvokeMethod, null, target, args);//调用指定实例instance的classmethod方法，sParams为传入参数，数量不固定，达到方法的重载

               // return targetType.GetMethod(targetMethod.Name).Invoke(target, args);
            }
        }
    }
}

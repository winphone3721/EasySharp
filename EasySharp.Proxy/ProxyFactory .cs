using System;
using System.Reflection;


namespace EasySharp.Proxy
{
    //public class DynamicProxy : DispatchProxy
    //{
    //    /// <summary>
    //    /// 真实类
    //    /// </summary>
    //    private Object realProxy;

    //    private object target;

    //    public object GetInstance(object target)
    //    {
    //        this.target = target;
    //        Type clazz = target.GetType();
    //        Object obj = Proxy.newProxyInstance(clazz.getClassLoader(), clazz.getInterfaces(), this);
    //        return obj;
    //    }
    //    protected override object Invoke(MethodInfo targetMethod, object[] args)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    public class ProxyFactory
    {
        private Object target;

        public ProxyFactory(Object target)
        {
            this.target = target;
        }


        // 为目标对象生成代理对象
        public Object GetProxyInstance<T, TProxy>() where TProxy : DispatchProxy
        {
            return DispatchProxy.Create<T, TProxy>();
        }

    }
}


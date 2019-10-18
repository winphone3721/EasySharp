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
        public static T Proxy<T, V>()
             where V : class, T
            => DispatchProxy.Create<T, InvokeProxy<V>>();
        //public static T Proxy<T, V>  ()          
        //    where V : class,T
        //{
        //    return DispatchProxy.Create<T, InvokeProxy<V>>();
        //}


        public class InvokeProxy<V> : DispatchProxy
           where V : class
        {
            private Predicate<MethodInfo> _filter;

            public event EventHandler<MethodInfo> BeforeExecute;
            public event EventHandler<MethodInfo> AfterExecute;
            public event EventHandler<MethodInfo> ErrorExecuting;

            private readonly V _decorated;
            private readonly Type _decoratedType;
            public InvokeProxy()
            {
                _decoratedType = typeof(V);
                //toString();
                _decorated = (V)_decoratedType.Assembly.CreateInstance(_decoratedType.FullName);

                Filter = m => true;
            }
            public Predicate<MethodInfo> Filter
            {
                get { return _filter; }
                set
                {
                    if (value == null)
                        _filter = m => true;
                    else
                        _filter = value;
                }
            }

            private void OnBeforeExecute(MethodInfo methodInfo)
            {
                if (BeforeExecute != null)
                {

                    if (_filter(methodInfo))
                        BeforeExecute(this, methodInfo);
                }
            }
            private void OnAfterExecute(MethodInfo methodInfo)
            {
                if (AfterExecute != null)
                {
                    if (_filter(methodInfo))
                        AfterExecute(this, methodInfo);
                }
            }
            private void OnErrorExecuting(MethodInfo methodInfo)
            {
                if (ErrorExecuting != null)
                {
                    if (_filter(methodInfo))
                        ErrorExecuting(this, methodInfo);
                }
            }
            protected override object Invoke(MethodInfo methodInfo, object[] args)
            {
                OnBeforeExecute(methodInfo);
                try
                {
                    var result = _decoratedType.InvokeMember(methodInfo.Name, BindingFlags.InvokeMethod, null, _decorated, args);
                    OnAfterExecute(methodInfo);
                    return result;
                }
                catch (Exception e)
                {
                    OnErrorExecuting(methodInfo);
                    return e.Message;
                }


            }
        }
    }


    //class DynamicProxy<T>
    //{
    //    private readonly T _decorated;
    //    private Predicate<MethodInfo> _filter;
    //    public event EventHandler<MethodInfo> BeforeExecute;
    //    public event EventHandler<MethodInfo> AfterExecute;
    //    public event EventHandler<MethodInfo> ErrorExecuting;
    //    public DynamicProxy(T decorated)
    //      : base(typeof(T))
    //    {
    //        _decorated = decorated;
    //        Filter = m => true;
    //    }
    //    public Predicate<MethodInfo> Filter
    //    {
    //        get { return _filter; }
    //        set
    //        {
    //            if (value == null)
    //                _filter = m => true;
    //            else
    //                _filter = value;
    //        }
    //    }
    //    private void OnBeforeExecute(IMethodCallMessage methodCall)
    //    {
    //        if (BeforeExecute != null)
    //        {
    //            var methodInfo = methodCall.MethodBase as MethodInfo;
    //            if (_filter(methodInfo))
    //                BeforeExecute(this, methodCall);
    //        }
    //    }
    //    private void OnAfterExecute(IMethodCallMessage methodCall)
    //    {
    //        if (AfterExecute != null)
    //        {
    //            var methodInfo = methodCall.MethodBase as MethodInfo;
    //            if (_filter(methodInfo))
    //                AfterExecute(this, methodCall);
    //        }
    //    }
    //    private void OnErrorExecuting(IMethodCallMessage methodCall)
    //    {
    //        if (ErrorExecuting != null)
    //        {
    //            var methodInfo = methodCall.MethodBase as MethodInfo;
    //            if (_filter(methodInfo))
    //                ErrorExecuting(this, methodCall);
    //        }
    //    }
    //    public override IMessage Invoke(IMessage msg)
    //    {
    //        var methodCall = msg as IMethodCallMessage;
    //        var methodInfo = methodCall.MethodBase as MethodInfo;
    //        OnBeforeExecute(methodCall);
    //        try
    //        {
    //            var result = methodInfo.Invoke(_decorated, methodCall.InArgs);
    //            OnAfterExecute(methodCall);
    //            return new ReturnMessage(
    //              result, null, 0, methodCall.LogicalCallContext, methodCall);
    //        }
    //        catch (Exception e)
    //        {
    //            OnErrorExecuting(methodCall);
    //            return new ReturnMessage(e, methodCall);
    //        }
    //    }
    //}
}

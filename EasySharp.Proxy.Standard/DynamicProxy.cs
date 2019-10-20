using System;
using System.Reflection;

namespace EasySharp.Proxy.Standard
{
    public class DynamicProxy
    {
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
}


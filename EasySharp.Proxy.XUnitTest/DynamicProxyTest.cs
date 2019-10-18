using EasySharp.Proxy.Service;
using EasySharp.Proxy.Service.Service;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;
using static EasySharp.Proxy.DynamicProxy;

namespace EasySharp.Proxy.XUnitTest
{
    public class DynamicProxyTest
    {
        [Fact]
        public void ProxyForHelloWorldChinaService()
        {
            //测试代理类
            var proxy = DynamicProxy.Proxy<IHelloWorldService, HelloWorldChinaService>();
            String result = proxy.SayHelloWorld();
            Assert.Equal(new HelloWorldChinaService().SayHelloWorld(), result);
            result = proxy.SayHelloWorld(Language.CN);
                        
            Assert.Equal(new HelloWorldChinaService().SayHelloWorld(Language.CN), result);

        }
        /// <summary>
        /// 拦截器测试
        /// </summary>
        [Fact]
        public void ProxyInterceptor()
        {
            //测试代理类
            var proxy = DynamicProxy.Proxy<IHelloWorldService, HelloWorldChinaService>();
            var invokeProxy = (InvokeProxy<HelloWorldChinaService>)proxy;
            invokeProxy.AfterExecute += InvokeProxy_AfterExecute;
            String result = proxy.SayHelloWorld();
            Assert.Equal(new HelloWorldChinaService().SayHelloWorld(), result);
            result = proxy.SayHelloWorld(Language.CN);
            Assert.Equal(new HelloWorldChinaService().SayHelloWorld(Language.CN), result);
        }

        private void InvokeProxy_AfterExecute(object sender, MethodInfo e)
        {
            var s = sender;
        }
    }
}

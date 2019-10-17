using EasySharp.Proxy.Service;
using EasySharp.Proxy.Service.Service;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

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
    }
}

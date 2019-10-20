using EasySharp.Proxy.EventArg;
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
        /// <summary>
        /// 代理测试
        /// </summary>
        [Fact]
        public void ProxyForHelloWorldChinaServiceTest()
        {
            //测试代理类
            var proxy = DynamicProxy.Proxy<IHelloWorldService, HelloWorldChinaService>();
            String result = proxy.SayHelloWorld();
            Assert.Equal(new HelloWorldChinaService().SayHelloWorld(), result);
            result = proxy.SayHelloWorld(Language.CN);
                        
            Assert.Equal(new HelloWorldChinaService().SayHelloWorld(Language.CN), result);

        }

        /// <summary>
        /// 拦截器测试-改变拦截器执行方法的值。
        /// </summary>
        [Fact]
        public void ProxyInterceptorTest()
        {
            //测试代理类
            var proxy = DynamicProxy.Proxy<IHelloWorldService, HelloWorldChinaService>();
            var invokeProxy = (InvokeProxy<HelloWorldChinaService>)proxy;
            invokeProxy.BeforeExecute += (sender, e) =>
            {
                //验证拦截成功。
                Assert.NotNull(sender);
                Assert.NotNull(e);
                var parametersValue = e.Args;
                if (parametersValue.Length > 0)
                {
                    //验证参数是否是 Language.CN
                    Assert.Equal(Language.CN,parametersValue[0]);
                }
            };
            String result = proxy.SayHelloWorld();          
            result = proxy.SayHelloWorld(Language.CN);
         
        }

        /// <summary>
        /// 拦截器测试-改变拦截器执行方法的值。
        /// </summary>
        [Fact]
        public void ProxyInterceptorChangesParamValueTest()
        {
            //测试代理类
            var proxy = DynamicProxy.Proxy<IHelloWorldService, HelloWorldChinaService>();
            var invokeProxy = (InvokeProxy<HelloWorldChinaService>)proxy;
            invokeProxy.BeforeExecute += (sender, e) =>
            {
                var s = sender.ToString();
                var parameters = e.MethodInfo.GetParameters();
                var parametersValue = e.Args;
                if (parametersValue.Length > 0)
                {
                    //测试拦截为EN
                    parametersValue[0] = Language.EN;
                }
                var ee = e.ToString();
            };
            String result = proxy.SayHelloWorld();
            Assert.Equal(new HelloWorldChinaService().SayHelloWorld(), result);
            result = proxy.SayHelloWorld(Language.CN);
            Assert.NotEqual(new HelloWorldChinaService().SayHelloWorld(Language.CN), result);
        }
        //todo 可删除
        //private void InvokeProxy_AfterExecute(object sender, ProxyEventArgs e)
        //{
        //    var s = sender.ToString();
        //    var parameters = e.MethodInfo.GetParameters();
        //    var ParametersValue = e.Args;
        //    var ee = e.ToString();
        //}
    }
}

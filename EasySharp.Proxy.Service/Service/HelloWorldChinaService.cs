using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.Proxy.Service.Service
{
    public class HelloWorldChinaService : IHelloWorldService
    {
        public string SayHelloWorld()
        {
            return "世界，你好！";
        }

        public string SayHelloWorld(Language language)
        {
            if (language.Equals(Language.CN))
            {
                return "【中文-世界，你好！】";
            }
            return "这里是中文，其它语言无效！";

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.Proxy.Service.Service
{
    public class HelloWorldEnglishService : IHelloWorldService
    {
        public string SayHelloWorld()
        {
            return "Hello,World！";
        }

        public string SayHelloWorld(Language language)
        {
            if (language.Equals(Language.EN))
            {
                return "[English-Hello,World！]";
            }
            return "这里是文英，其它语言无效！";

        }
    }
}

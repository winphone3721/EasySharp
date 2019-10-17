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
            if (language.Equals(Language.CN))
            {
                return "[English-Hello,World！]";
            }
            return string.Empty;

        }
    }
}

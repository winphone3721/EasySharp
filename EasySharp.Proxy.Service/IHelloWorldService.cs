using System;

namespace EasySharp.Proxy.Service
{
    public interface IHelloWorldService
    {
        string SayHelloWorld();
        string SayHelloWorld(Language language);
    }

    public enum Language
    {
        CN,
        EN
    }
}

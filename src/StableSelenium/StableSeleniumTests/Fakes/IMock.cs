using System.Reflection;

namespace StableSelenium.Tests.Fakes
{
    internal interface IMock
    {
         MethodBase LastMethodCalled { get; }
    }
}
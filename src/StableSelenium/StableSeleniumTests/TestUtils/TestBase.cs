using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;


namespace StableSelenium.Tests.TestUtils
{
    [TestFixture]
    public class TestBase
    {
        protected StableWebDriver Driver;
        
        [SetUp]
        public void BeforeTest()
        {
            MethodInfo currentTest = GetCurrentTest();
            CreateMockOrRealBrowser(currentTest);
        }

        [TearDown]
        public void TearDown()
        {
            Driver?.Quit();
        }


        protected void InjectHtmlWithMultipleElements(int numberOfDivs = 3)
        {
            var numbers = new [] { "First", "Second", "Third" };
            string body = "";
            for (int i = 1; i <= numberOfDivs; i++)
            {
                body += $@"<div id=""{i}"">{numbers[i-1]} div</div>";
            }
            Driver.ExecuteScript($@"document.write('{body}');");
        }

        static MethodInfo GetCurrentTest()
        {
            var typename = NUnit.Framework.TestContext.CurrentContext.Test.ClassName;
            var testMethodName = NUnit.Framework.TestContext.CurrentContext.Test.MethodName;
            var type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(a => a.FullName == typename);
            var testmethod = type.GetMethods().FirstOrDefault(a => a.Name == testMethodName);
            return testmethod;
        }

        void CreateMockOrRealBrowser(MethodInfo currentTest)
        {
            if (IsAttributeDefinedOnMethodOrItsClass(currentTest, typeof(UseMockBrowserAttribute)))
                Driver = StableWebDriverFactory.CreateMockDriver();
            else if (IsAttributeDefinedOnMethodOrItsClass(currentTest, typeof(UseRealBrowserAttribute)))
                Driver = StableWebDriverFactory.CreateRealDriver();
        }

        private bool IsAttributeDefinedOnMethodOrItsClass(MethodInfo method,Type attrType)
        {
            var definedOnMethod = Attribute.IsDefined(method, attrType);
            var definedOnType = Attribute.IsDefined(method.DeclaringType, attrType);
            return definedOnMethod || definedOnType;
        }

        public class UseMockBrowserAttribute : Attribute
        {
        }

        public class UseRealBrowserAttribute : Attribute
        {
        }
    }

    
}

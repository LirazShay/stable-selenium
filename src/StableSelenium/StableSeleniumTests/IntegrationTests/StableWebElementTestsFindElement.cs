using NUnit.Framework;
using OpenQA.Selenium;
using StableSelenium.Tests.TestUtils;
using System.Linq;

namespace StableSelenium.Tests.IntegrationTests
{
    [Category("IntegrationTests")]
    [UseRealBrowser]
    [TestFixture]
    public class StableWebElementTestsFindElement : TestBase
    {
               
        [Test]
        public void FindElement_WhenCalled_ShouldReturnStableWebElement()
        {
            var parent = Driver.FindElement(By.CssSelector("html"));
            var child = parent.FindElement(By.CssSelector("body"));
            Assert.IsTrue(child is StableWebElement);
        }

        [Test]
        public void FindElements_WhenCalled_ShouldReturnStableWebElementCollection()
        {
            var parent = Driver.FindElement(By.CssSelector("html"));
            var childs = parent.FindElements(By.XPath("//*[name()='head' or name()='body']"));
            Assert.IsTrue(childs.All(a=>a is StableWebElement));
        }

        [Test]
        public void FindElements_WhenCalledOnIWebElementObjectReference_ShouldReturnStableWebElementCollection()
        {
            var driver = Driver as IWebDriver;
            var parent = driver.FindElement(By.CssSelector("html"));
            var childs = parent.FindElements(By.XPath("//*[name()='head' or name()='body']"));
            Assert.IsTrue(childs.All(a => a is StableWebElement));
        }

    }
}
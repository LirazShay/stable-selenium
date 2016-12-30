using NUnit.Framework;
using OpenQA.Selenium;
using StableSelenium.Tests.Support;
using System.Linq;

namespace StableSelenium.Tests.WebDriverTests
{
    [UseRealBrowser]
    [TestFixture]
    public class FindElementTests  : TestBase
    {
        
        [Test]
        public void FindElement_WhenCalledFromStableWebDriverObjectReference_ShouldReturnStableWebElement()
        {
            var element = Driver.FindElement(By.CssSelector("body"));
            Assert.That(element, Is.InstanceOf<StableWebElement>());
        }

        [Test]
        public void FindElement_WhenCalledFromIWebDriverObjectReference_ShouldReturnStableWebElement()
        {
            var element = (Driver as IWebDriver).FindElement(By.CssSelector("body"));
            Assert.That(element, Is.InstanceOf<StableWebElement>());
        }


        [Test]
        public void FindElements_WhenCalledFromStableWebDriverObjectReference_ShouldReturnStableWebElements()
        {
            InjectHtmlWithMultipleElements();
            var elements = Driver.FindElements(By.CssSelector("div"));
            Assert.IsTrue(elements.All(a => a is StableWebElement));
        }

        [Test]
        public void FindElements_WhenCalledFromIWebDriverObjectReference_ShouldReturnStableWebElements()
        {
            InjectHtmlWithMultipleElements();
            var elements = (Driver as IWebDriver).FindElements(By.CssSelector("div"));
            Assert.IsTrue(elements.All(a => a is StableWebElement));
        }


    }
    
}

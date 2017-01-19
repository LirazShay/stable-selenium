using NUnit.Framework;
using OpenQA.Selenium;
using StableSelenium.Tests.TestUtils;

namespace StableSelenium.Tests.IntegrationTests
{
    [Category("IntegrationTests")]
    [UseRealBrowser]
    [TestFixture]
    public class StableWebElementTests : TestBase
    {

        [Test]
        public void By_WhenCallledOnSingleElement_ShouldReturnTheLocatorOfTheCurrentElement()
        {
            var element = Driver.FindElement(By.CssSelector("body"));
            Assert.AreEqual("By.CssSelector: body", element.By.ToString());
        }

        [Test]
        public void By_WhenCalledOnWebElementFromList_ShouldReturnTheLocatorOfTheCurrentElement()
        {
            InjectHtmlWithMultipleElements();
            var elements = Driver.FindElements(By.CssSelector("div"));
            Assert.AreEqual("By.CssSelector: div", elements[1].By.ToString());
        }

        [Test]
        public void ElementIndex_WhenCalledOnWebElementFromList_ShouldReturnTheCorrectIndex()
        {
            InjectHtmlWithMultipleElements();
            var elements = Driver.FindElements(By.CssSelector("div"));
            Assert.AreEqual(1, elements[1].ElementIndex);
        }

        [Test]
        public void ElementIndex_WhenCalledOnSingleWebElement_ShouldReturnZero()
        {
            var element = Driver.FindElement(By.CssSelector("body"));
            Assert.AreEqual(0, element.ElementIndex);
        }

       
    }
}
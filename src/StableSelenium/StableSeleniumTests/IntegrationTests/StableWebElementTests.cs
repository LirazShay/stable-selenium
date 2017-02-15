using NUnit.Framework;
using OpenQA.Selenium;
using StableSelenium.Tests.TestUtils;

namespace StableSelenium.Tests.IntegrationTests
{
    [Category("IntegrationTests")]
    [TestFixture]
    public class StableWebElementTests : TestBase
    {

        [UseMockBrowser]
        [Test]
        public void By_SingleElement_ReturnCorrectLocator()
        {
            var element = Driver.FindElement(By.CssSelector("body"));
            Assert.AreEqual("By.CssSelector: body", element.By.ToString());
        }

        [UseMockBrowser]
        [Test]
        public void By_MultipleElements_ReturnCorrectLocator()
        {
            var elements = Driver.FindElements(By.CssSelector("div"));
            Assert.AreEqual("By.CssSelector: div", elements[0].By.ToString());
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
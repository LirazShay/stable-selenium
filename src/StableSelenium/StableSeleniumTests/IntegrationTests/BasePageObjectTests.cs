using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using StableSelenium.Tests.TestUtils;


namespace StableSelenium.Tests.IntegrationTests
{
    [Category("IntegrationTests")]
    [TestFixture]
    public class PageObjectTests : TestBase
    {
        [Test,UseRealBrowser]
        public void PageObjectTest()
        {
            var page = new DemoPageObject(Driver);
            var elementType = page.Body.GetType();
            
            Assert.IsTrue(elementType is StableWebElement);
            //search google how to get the real type of transparentproxy C#
        }
    }

    public class DemoPageObject : BasePageObject
    {
        public DemoPageObject(StableWebDriver driver) : base(driver)
        {
            Body.Click();
        }
        
        [FindsBy(How = How.CssSelector, Using = "body")]
        public IWebElement Body;

    }
}
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using StableSelenium.Tests.Support;
using System;

namespace StableSelenium.Tests.PageObjectTests
{
    [TestFixture]
    public class PageObjectTests : TestBase
    {
        [Test]
        public void PageObjectTest()
        {
            var page = new DemoPageObject(Driver);
            throw new NotImplementedException();
        }
    }

    public class DemoPageObject : BasePageObject
    {
        public DemoPageObject(StableWebDriver driver) : base(driver)
        {
        }
        
        [FindsBy(How = How.CssSelector, Using = "body")]
        public IWebElement Body;

    }
}
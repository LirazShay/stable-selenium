using NUnit.Framework;
using OpenQA.Selenium;
using StableSelenium.Tests.TestUtils;
using System;

namespace StableSelenium.Tests.IntegrationTests
{
    /// <summary>
    /// In Selenium, when calling webElement.Displayed
    /// if the element exists in the HTML - it will return true or false (according to if displayed or not)
    /// but if the element doesn't exist in the HTML - then we cannot check if displayed 
    /// because the method FindElement will throw NoSuchElementException
    /// So, in this framework - the behavior was fixed to be reasonable:
    /// Meaning: if element is not displayed and not exist in the HTML 
    /// then the result for Displayed will be false instead of exception
    /// the main change is in the framework, the FindElement method will not
    /// throw NoSuchElement, but when calling IWebElement method and the element
    /// does not exist - that method will throw it (for example -click/text)
    /// </summary>
    [TestFixture,UseRealBrowser,Category("IntegrationTests")]
    public class StableWebElementTestsDisplayed  : TestBase
    {
        [Test]
        public void Displayed_WhenElementIsDisplayed_ShouldReturnTrue()
        {
            var element = Driver.FindElement(By.CssSelector("body"));
            Assert.IsTrue(element.Displayed);
        }


        [Test]
        public void Displayed_WhenElementIsNotDisplayedButExists_ShouldReturnFalse()
        {
            string createHiddenDiv =
            @"var element = document.createElement('div');
            element.id = 'hidden';
            element.style.display = 'none';
            document.body.appendChild(element);";
            Driver.ExecuteScript(createHiddenDiv);

            var element = Driver.FindElement(By.Id("hidden"));
            Assert.IsFalse(element.Displayed);
        }


        [Test]
        public void Displayed_WhenElementIsNotDisplayedAndNotExistsInTheHtml_ShouldReturnFalseInsteadOfNoSuchElementException()
        {
            var element = Driver.FindElement(By.Id("hidden"));
            Assert.IsFalse(element.Displayed);
        }

        [Test]
        public void ByConfigurationIfFindelementWillThrowNoSuch()
        {
            throw new NotImplementedException();
        }

    }
}

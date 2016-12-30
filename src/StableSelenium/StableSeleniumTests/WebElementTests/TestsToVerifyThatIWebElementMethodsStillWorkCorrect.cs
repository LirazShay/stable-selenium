using NUnit.Framework;
using OpenQA.Selenium;
using StableSelenium.Tests.Support;
using System;

namespace StableSelenium.Tests.WebElementTests
{
    [TestFixture, UseMockBrowser]
    public class TestsToVerifyThatIWebElementMethodsStillWorkCorrect : TestBase
    {
        [Test]
        public void Clear_WhenCalled_ShouldCallWrappedElementClear()
        {
            var stableWebElement = Driver.FindElement(By.CssSelector("input[type='text']"));
            var realElement = stableWebElement.WrappedElement as FakeWebElement;

            stableWebElement.Clear();

            Assert.AreEqual("Clear", realElement.LastMethodCalled.Name);
        }

        [Test]
        public void Click_WhenCalled_ShouldCallWrappedElementClick()
        {
            var stableWebElement = Driver.FindElement(By.TagName("button"));
            var realElement = stableWebElement.WrappedElement as FakeWebElement;
            realElement.BooleanReturnValue = true; //to return that element is enabled and visible

            stableWebElement.Click();

            Assert.AreEqual("Click", realElement.LastMethodCalled.Name);
        }

        [Test]
        public void GetAttribute_WhenCalled_ShouldCallWrappedElementGetAttribute()
        {
            var stableWebElement = Driver.FindElement(By.TagName("input"));
            var realElement = stableWebElement.WrappedElement as FakeWebElement;
            var attributeNameArgument = "class";
            var fakeAttributeValue = "myclass";
            realElement.StringReturnValue = fakeAttributeValue;

            var actualAttributeValue = stableWebElement.GetAttribute(attributeNameArgument);
            
            Assert.AreEqual(fakeAttributeValue, actualAttributeValue);
            Assert.AreEqual(attributeNameArgument, realElement.LastParameterArgument);
        }

        [Test]
        public void GetCssValue_WhenCalled_ShouldCallWrappedElementGetCssValue()
        {
            var stableWebElement = Driver.FindElement(By.TagName("input"));
            var realElement = stableWebElement.WrappedElement as FakeWebElement;
            var propertyNameArgument = "display";
            var fakeCssValue = "none";
            realElement.StringReturnValue = fakeCssValue;

            var actualCssValue = stableWebElement.GetCssValue(propertyNameArgument);

            Assert.AreEqual(fakeCssValue, actualCssValue);
            Assert.AreEqual(propertyNameArgument, realElement.LastParameterArgument);
        }

        [Test]
        public void SendKeys_WhenCalled_()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Submit_WhenCalled_()
        {
            throw new NotImplementedException();
        }
    }
}
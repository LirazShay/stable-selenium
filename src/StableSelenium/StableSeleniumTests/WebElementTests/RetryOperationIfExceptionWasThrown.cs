using System;
using OpenQA.Selenium;
using NUnit.Framework;
using StableSelenium.Tests.Support;

namespace StableSelenium.Tests.WebElementTests
{
    [UseRealBrowser]
    [TestFixture]
    public class RetryOperationIfExceptionWasThrown : TestBase
    {
        /// <summary>
        /// This test verifies that the exception Element Not Clickable at point 
        /// this is usually thrown by Selenium if the clicked element is displayed by CSS but visually hidden under
        /// another element (for example the top element has position absolute)
        /// this framework will try again to click on the element until timeout exceeded (default is 20 sec)
        /// this will solve the scenarios when the element is hidden but the DOM is rendered / while animation
        /// </summary>
        [Test]
        public void Click_WhenElementClickableOnlyAfter3Sec_ShouldWork()
        {
            CreateButtonNotClickableFor3Seconds();
            
            //Without the framework it should throw exception
            var ex = Assert.Throws<InvalidOperationException>(() => Driver.FindElement(By.CssSelector("button")).WrappedElement.Click());
            Assert.IsTrue(ex.Message.Contains("Element is not clickable at point"));
            //The framework should not throw exception
            Driver.FindElement(By.CssSelector("button")).Click();

            Assert.IsTrue(Driver.FindElement(By.CssSelector("body")).Text.Contains("button clicked"));
        }

        /// <summary>
        /// This test verifies that when element does not exist
        /// when calling FindElement(), it will retry finding the element.
        /// the solution is made by calling :
        /// driver.Manage().Timeouts().ImplicitlyWait(config.ImplicitlyWaitTimeout);
        /// in StableWebDriver constructor
        /// </summary>
        [Test]
        public void Click_WhenElementExistOnlyAfter3Sec_ShouldWork()
        {
            CreateButtonAfter3Seconds();
            
            //The framework should not throw exception
            Driver.FindElement(By.CssSelector("button")).Click();

            Assert.IsTrue(Driver.FindElement(By.CssSelector("body")).Text.Contains("button clicked"));
        }

        [Test]
        public void Click_WhenElementVisibleOnlyAfter3Sec_ShouldWork()
        {
            CreateButtonVisibleAfter3Sec();

            //Without the framework it should throw exception
            Assert.Throws<ElementNotVisibleException>(
                () => Driver.FindElement(By.CssSelector("button")).WrappedElement.Click());
            //The framework should not throw exception
            Driver.FindElement(By.CssSelector("button")).Click();

            Assert.IsTrue(Driver.FindElement(By.CssSelector("body")).Text.Contains("button clicked"));
        }

        [Test]
        public void Click_WhenElementDisabledFor3Sec_WillWaitUntilEnabledAndThenClick()
        {
            CreateButtonDisabledFor3Sec();
            Driver.FindElement(By.CssSelector("button")).Click();
            Assert.IsTrue(Driver.FindElement(By.CssSelector("body")).Text.Contains("button clicked"));
        }

        [Test]
        public void Click_WhenElementDisabled_WillWaitUntilTimeoutAndThenClickInsteadOfThrowingException()
        {
            CreateDisabledButton();
            Driver.FindElement(By.CssSelector("button")).Click();
            Assert.IsFalse(Driver.FindElement(By.CssSelector("body")).Text.Contains("button clicked"));
        }

        /// <summary>
        /// Element not clickable is exception that is thrown if an element is hiding
        /// the element that you tried to click on, so the element will not receive the click
        /// just want to check here that the real exception is thrown in such case 
        /// and not time out exception
        /// </summary>
        [Test]
        public void Click_WhenElementNotClickable_ShouldThrowNotClickableException()
        {
            CreateButtonNotClickableAtPoint();
            Driver.DriverConfiguration.TimeoutToRetryClickIfFailed = TimeSpan.FromSeconds(2);

            var ex = Assert.Throws<InvalidOperationException>(
                () => Driver.FindElement(By.CssSelector("button")).Click());

            Assert.IsTrue(ex.Message.Contains("Element is not clickable at point"), ex.Message);
            Assert.IsTrue(ex.Message.Contains("Other element would receive the click"), ex.Message);
        }

        [Test]
        public void Text_WhenElementNotVisible_ShouldThrowNotVisibleException()
        {
            Driver.DriverConfiguration.TimeToWaitUntilElementClickable = TimeSpan.FromSeconds(2);
            Driver.DriverConfiguration.TimeoutToRetryClickIfFailed = TimeSpan.FromSeconds(2);

            CreateButtonNotVisible();

            var ex = Assert.Throws<ElementNotVisibleException>(
                () => Driver.FindElement(By.CssSelector("button")).Click());
        }

        [Test]
        public void WrappedElement_WhenNotExistSuchElement_ShouldThrowNoSuchElementException()
        {
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
            Driver.DriverConfiguration.TimeToWaitUntilElementClickable = TimeSpan.FromSeconds(1);
            Driver.DriverConfiguration.TimeoutToRetryClickIfFailed = TimeSpan.FromSeconds(1);

            var ex = Assert.Throws<NoSuchElementException>(
                () => {
                    var e =
                    Driver.FindElement(By.CssSelector("button")).WrappedElement;
                    });
        }

        private void CreateButtonAfter3Seconds()
        {
            string script =
                @"setTimeout(createButton, 3000);
                function createButton() {
                    document.body.innerHTML = '';
                    var btn = document.createElement('button');
                    btn.textContent = 'Click me';
                    btn.onclick = function () {
                        document.body.append('button clicked');
                    }
                    document.body.appendChild(btn);
                }";
            Driver.ExecuteScript(script);
        }
        private void CreateDisabledButton()
        {
            var script = @"createDisabledButton();
                            function createDisabledButton() {
                                document.body.innerHTML = '';
                                var btn = document.createElement('button');
                                btn.textContent = 'Click me';
                                btn.onclick = function () {
                                    document.body.append('button clicked');
                                }
                                btn.disabled = true;
                                document.body.appendChild(btn);
                            }";
            Driver.ExecuteScript(script);
        }

        private void CreateButtonDisabledFor3Sec()
        {
            var script = @"createDisabledButton();
                            setTimeout(makeButtonEnabled, 3000);
                            function createDisabledButton() {
                                document.body.innerHTML = '';
                                var btn = document.createElement('button');
                                btn.textContent = 'Click me';
                                btn.onclick = function () {
                                    document.body.append('button clicked');
                                }
                                btn.disabled = true;
                                document.body.appendChild(btn);
                            }
                            function makeButtonEnabled() {
                                var btn = document.querySelector('button');
                                btn.disabled = false;
                            }";
            Driver.ExecuteScript(script);
        }

        private void CreateButtonVisibleAfter3Sec()
        {
            string script = @"createNotVisibleButton();
                                    setTimeout(makeButtonVisible, 3000);
                                    function createNotVisibleButton() {
                                        document.body.innerHTML = '';
                                        var btn = document.createElement('button');
                                        btn.textContent = 'Click me';
                                        btn.onclick = function () {
                                            document.body.append('button clicked');
                                        }
                                        btn.style.display = 'none';
                                        document.body.appendChild(btn);
                                    }
                                    function makeButtonVisible() {
                                        var btn = document.querySelector('button');
                                        btn.style.display = 'block';
                                    }";
            Driver.ExecuteScript(script);
        }

        private void CreateButtonNotVisible()
        {
            string script = @"createNotVisibleButton();
                                    function createNotVisibleButton() {
                                        document.body.innerHTML = '';
                                        var btn = document.createElement('button');
                                        btn.textContent = 'Click me';
                                        btn.onclick = function () {
                                            document.body.append('button clicked');
                                        }
                                        btn.style.display = 'none';
                                        document.body.appendChild(btn);
                                    }";
            Driver.ExecuteScript(script);
        }


        private void CreateButtonNotClickableFor3Seconds()
        {
            string script = @"createButtonHiddenUnderAnotherDiv();
                            setTimeout(removeTheDiv, 3000);
                            function createButtonHiddenUnderAnotherDiv() {
                                document.body.innerHTML = '';
                                var btn = document.createElement('button');
                                btn.textContent = 'Click me';
                                btn.style.position = 'absolute';
                                btn.style.top = '0';
                                btn.style.left = '0';
                                btn.onclick = function () {
                                    document.body.append('button clicked');
                                    document.body.appendChild(document.createElement('br'));
                                }
                                document.body.appendChild(btn);
                                var div = document.createElement('div');
                                div.id = 'hideBtnDiv';
                                div.textContent = 'Hiding the button for 3 seconds';
                                div.style.width = '100px';
                                div.style.backgroundColor = 'red';
                                div.style.position = 'absolute';
                                div.style.top = '0';
                                div.style.left = '0';
                                document.body.appendChild(div);
                            }
                            function removeTheDiv() {
                                document.getElementById('hideBtnDiv').remove();
                            }";
            Driver.ExecuteScript(script);
        }

        private void CreateButtonNotClickableAtPoint()
        {
            string script = @"
                            createButtonHiddenUnderAnotherDiv();
                            function createButtonHiddenUnderAnotherDiv() {
                                document.body.innerHTML = '';
                                var btn = document.createElement('button');
                                btn.textContent = 'Click me';
                                btn.style.position = 'absolute';
                                btn.style.top = '0';
                                btn.style.left = '0';
                                btn.onclick = function () {
                                    document.body.append('button clicked');
                                    document.body.appendChild(document.createElement('br'));
                                }
                                document.body.appendChild(btn);
                                var div = document.createElement('div');
                                div.id = 'hideBtnDiv';
                                div.textContent = 'I am hiding the button';
                                div.style.width = '100px';
                                div.style.backgroundColor = 'red';
                                div.style.position = 'absolute';
                                div.style.top = '0';
                                div.style.left = '0';
                                document.body.appendChild(div);
                            }";
            Driver.ExecuteScript(script);
        }

    }
}
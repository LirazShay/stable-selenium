using NUnit.Framework;
using OpenQA.Selenium;
using StableSelenium.Tests.Support;
using System;
using System.Collections.Generic;

namespace StableSelenium.Tests.WebElementTests
{
    [UseRealBrowser]
    [TestFixture]
    public class StaleElementReferenceExceptionHandlingTests : TestBase
    {

        string createChildButton = @"
                var childBtn = document.createElement('button');
                childBtn.id = 'childBtn';
                childBtn.innerText = 'click me';
                childBtn.onclick = function() { document.body.append('clicked'); }
                document.getElementById('parentDiv').appendChild(childBtn);";

        string createParentDiv =
                @"var parent = document.createElement('div');
                parent.id = 'parentDiv';
                document.body.appendChild(parent);";

        string removeTheChildScript = @"document.getElementById('childBtn').remove();";

        [Test]
        public void WrappedElement_WhenSingleElementAndElementReferenceWasStaled_ShouldFindAgainTheElementAndReturnItInsteadOfThrowingStaleElementReferenceException()
        {
            //Prepare
            var stableWebElement = Driver.FindElement(By.CssSelector("body"));
            var realElement = stableWebElement.WrappedElement;
            //Act
            Driver.Navigate().Refresh();

            //Assert
            Assert.Throws<StaleElementReferenceException>(
                () =>
                {
                    var displayed = realElement.Displayed;
                });
            Assert.DoesNotThrow(() =>
            {
                var displayed = stableWebElement.WrappedElement.Displayed;
            });
        }
        


        [Test]
        public void WrappedElement_WhenWebElementIsChildOfAnotherStableWebElementAndTheElementReferenceOfTheChildWasStaled_ShouldFindAgainTheChildElementAndReturnItInsteadOfThrowingStaleElementReferenceException()
        {
            //Arrange
            StableWebElement parentDiv;
            StableWebElement childBtn;

            CreateParentDivAndChildBtn();
            FindParentDivAndChildBtn(out parentDiv, out childBtn);
            IWebElement childButtonOriginalElement = childBtn.WrappedElement;


            //Remove the child and recreate it.
            Driver.ExecuteScript(removeTheChildScript);
            Driver.ExecuteScript(createChildButton);
           
            
            //Assert
            //Without the framework - an exception will be thrown
            Assert.Throws<StaleElementReferenceException>(
                () => childButtonOriginalElement.Click());

            Assert.DoesNotThrow(() =>
            {
                childBtn.Click();
            });

            Assert.IsTrue(Driver.PageSource.Contains("clicked"));
        }

        [Test]
        public void WrappedElement_WhenWebElementIsChildOfAnotherStableWebElementAndTheElementReferenceOfTheParentWasStaled_ShouldFindAgainTheParentElementAndTheChildReturnItInsteadOfThrowingStaleElementReferenceException()
        {
            //Arrange
            StableWebElement parentDiv;
            StableWebElement childButton;

            CreateParentDivAndChildBtn();
            FindParentDivAndChildBtn(out parentDiv, out childButton);
            IWebElement childButtonWrappedElement = childButton.WrappedElement;

            //Act
            Driver.Navigate().Refresh();
            CreateParentDivAndChildBtn();

            //Assert
            //Without the framework - an exception will be thrown
            Assert.Throws<StaleElementReferenceException>(
                () => childButtonWrappedElement.Click());

            Assert.DoesNotThrow(() =>
            {
                childButton.Click();
            });

            Assert.IsTrue(Driver.PageSource.Contains("clicked"));
        }

        [Test]
        public void WrappedElement_WhenListOfElementsAreChildsOfStableWebElementAndTheReferenceOfTheParentWasStaled_ShouldFindAgainTheParentElementAndTheChildReturnItInsteadOfThrowingStaleElementReferenceException()
        {
            
            //Arrange
            CreateParentDivAndInside2Buttons();
            StableWebElement parentDiv = Driver.FindElement(By.TagName("div"));
            IList<StableWebElement> childButtons = parentDiv.FindElements(By.TagName("button"));
            IWebElement childButtonWrappedElement = childButtons[1].WrappedElement;

            //Act
            Driver.Navigate().Refresh();
            CreateParentDivAndInside2Buttons();

            //Assert
            //Without the framework - an exception will be thrown
            Assert.Throws<StaleElementReferenceException>(
                () => childButtonWrappedElement.Click());

            Assert.DoesNotThrow(() =>
            {
                childButtons[1].Click();
            });

            Assert.IsTrue(Driver.PageSource.Contains("button2 clicked"));
            throw new NotImplementedException("Need to check how this passes");
        }

        private void CreateParentDivAndInside2Buttons()
        {
            var script = @"var parent = createParentDivWith2ButtonsInside();
                            document.body.innerHTML = '';
                            document.body.appendChild(parent);

                            function createParent() {
                                var parent = document.createElement('div');
                                parent.id = 'parentDiv';
                                return parent;
                            }
                            function createButton(btnId) {
                                var btn = document.createElement('button');
                                btn.textContent = btnId;
                                btn.id = btnId;
                                btn.onclick = function () {
                                    document.body.append(btnId + ' clicked');
                                    document.body.appendChild(document.createElement('br'));
                                }
                                return btn;
                            }
                            function createParentDivWith2ButtonsInside() {
                                var parent = createParent();
                                var child1 = createButton('button1');
                                var child2 = createButton('button2');
                                parent.appendChild(child1);
                                parent.appendChild(child2);
                                return parent;
                            }";
            Driver.ExecuteScript(script);
        }

        [Test]
        public void WrappedElement_WhenSingleElementAndElementReferenceWasStaledAndElementRemovedFromTheDom_ShouldThrowNoSuchElementExceptionInsteadOfThrowingStaleElementReferenceException()
        {
            //Prepare
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));//in order to not wait more than 2 seconds
            Driver.ExecuteScript("document.write('<h1>Header</h1>');");
            var stableWebElement = Driver.FindElement(By.CssSelector("h1"));
            var realElement = stableWebElement.WrappedElement;

            //Act
            Driver.Navigate().Refresh(); // the element reference will not be valid and the element will disappear

            //Assert
            Assert.Throws<StaleElementReferenceException>(
                () =>
                {
                    var displayed = realElement.Displayed;
                });

            Assert.Throws<NoSuchElementException>(
                () =>
                {
                    var displayed = stableWebElement.WrappedElement.Displayed;
                });
        }

        [Test]
        public void WrappedElement_WhenElementFromListAndElementReferenceWasStaled_ShouldFindAgainTheElementInTheCorrectIndexAndReturnItInsteadOfThrowingStaleElementReferenceException()
        {
            //Prepare
            InjectHtmlWithMultipleElements();
            int elementIndex = 1;
            var stableWebElement = Driver.FindElements(By.CssSelector("div"))[elementIndex];
            var realElement = stableWebElement.WrappedElement;

            //Act
            Driver.Navigate().Refresh();
            InjectHtmlWithMultipleElements();

            //Assert
            bool isDisplayed = false;
            Assert.Throws<StaleElementReferenceException>(() =>
                { isDisplayed = realElement.Displayed; });
            Assert.DoesNotThrow(() =>
                { isDisplayed = stableWebElement.Displayed; });
            Assert.IsTrue(isDisplayed);
            Assert.AreEqual(elementIndex, stableWebElement.ElementIndex);
        }

        [Test]
        public void WrappedElement_WhenElementFromListAndStaleElementReferenceAndTheNewListOfElementsDoesNotHaveTheIndexOfTheElement_ShouldThrowException()
        {
            //Prepare
            InjectHtmlWithMultipleElements(3);
            int elementIndex = 2;
            var stableWebElement = Driver.FindElements(By.CssSelector("div"))[elementIndex];
            var realElement = stableWebElement.WrappedElement;

            //Act
            Driver.Navigate().Refresh();
            InjectHtmlWithMultipleElements(2);

            //Assert
            try
            {
                var element = stableWebElement.WrappedElement;
            }
            catch (NoSuchElementException e)
            {
                Assert.AreEqual("Can't find element: By.CssSelector: div at index:2 .there are only: 2 elements",
                    e.Message);
                return;
            }
            Assert.Fail();// If NoSuchElementException was thrown, there is a return before this row.
        }


        private void CreateParentDivAndChildBtn()
        {
            Driver.ExecuteScript(createParentDiv);
            Driver.ExecuteScript(createChildButton);
        }

        private void FindParentDivAndChildBtn(out StableWebElement parentDiv, out StableWebElement childBtn)
        {
            parentDiv = Driver.FindElement(By.Id("parentDiv"));
            childBtn = parentDiv.FindElement(By.CssSelector("button")) as StableWebElement;
        }
    }
}
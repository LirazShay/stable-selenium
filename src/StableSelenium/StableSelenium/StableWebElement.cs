using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace StableSelenium
{
    public class StableWebElement : IWebElement,IWrapsElement,IWrapsDriver
    {

        /// <summary>
        /// The locator to find the element
        /// </summary>
        private By by;
        /// <summary>
        /// The StableWebDriver instance that created this instance
        /// </summary>
        private StableWebDriver driver;
        /// <summary>
        /// Original element, the reference can be stoled (StaleElementReference)
        /// </summary>
        private IWebElement wrappedElement;
        /// <summary>
        /// If object was created from Driver.FindElements
        /// then the element index is in use.
        /// </summary>
        private int elementIndex = 0;

        /// <summary>
        /// Indicates if the current element was found by driver.FindElements method
        /// </summary>
        private bool isElementFromList = false;

        /// <summary>
        /// When this element was created by StableWebElement.FindElement(by),
        /// then we save the parent element, that in case of stale element reference
        /// we will find again the parent and then the child (this)
        /// </summary>
        private StableWebElement parentElement = null;

        /// <summary>
        /// The constructor in used when calling driver.FindElement
        /// </summary>
        /// <param name="element">Original IWebElement object</param>
        /// <param name="by">The locator to find the element</param>
        /// <param name="driver">The StableWebDriver instance that created this instance</param>
        public StableWebElement(IWebElement element, By by, StableWebDriver driver)
        {
            wrappedElement = element;
            this.by = by;
            this.driver = driver;
        }

        /// <summary>
        /// The constructor in used when calling driver.FindElements
        /// then need to pass the element index together with the element locator
        /// </summary>
        /// <param name="element">Original IWebElement object</param>
        /// <param name="by">The locator to find the element</param>
        /// <param name="elementIndex">the index of the element in the list</param>
        /// <param name="driver">The StableWebDriver instance that created this instance</param>
        public StableWebElement(IWebElement element, By by, int elementIndex, StableWebDriver driver) : this(element,by,driver)
        {
            this.elementIndex = elementIndex;
            isElementFromList = true;
        }

        /// <summary>
        /// When this element was created by StableWebElement.FindElement(by),
        /// then we need the parent element, that in case of stale element reference
        /// we will find again the parent and then the child (this)
        /// </summary>
        /// <param name="element">Original IWebElement object</param>
        /// <param name="by">The locator to find the element</param>
        /// <param name="parentElement">The parent of the current instance of StableWebElement</param>
        /// <param name="driver">The StableWebDriver instance that created this instance</param>
        public StableWebElement(IWebElement element, By by, StableWebElement parentElement, StableWebDriver driver) : this(element, by, driver)
        {
            this.parentElement = parentElement;
        }

        /// <summary>
        /// The locator that found the element
        /// </summary>
        public By By => by;


        /// <summary>
        /// If object was created from Driver.FindElements
        /// then the element index is in use.
        /// </summary>
        public int ElementIndex => elementIndex;

        /// <summary>
        /// There difference between this method and the original 'Displayed' method is that
        /// in the original method when calling this method and the element does not exist in the html
        /// it will throw an exception - NoSuchElementException
        /// BUT in this framework we will return false.
        /// </summary>
        public bool Displayed
        {
            get
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                bool elementPresent = false;
                try
                {
                    elementPresent = WrappedElement.Displayed;
                }
                catch (Exception) { }
                driver.Manage().Timeouts().ImplicitlyWait(driver.DriverConfiguration.ImplicitlyWaitTimeout);
                return elementPresent;
            }
        }

        public bool Enabled
        {
            get
            {
                return RetryFuncIfFailed(() => WrappedElement.Enabled, driver.DriverConfiguration.TimeoutToRetryClickIfFailed);
            }
        }

        public System.Drawing.Point Location
        {
            get
            {
                return RetryFuncIfFailed(() => WrappedElement.Location, driver.DriverConfiguration.TimeoutToRetryClickIfFailed);
            }
        }

        public bool Selected
        {
            get
            {
                return RetryFuncIfFailed(() => WrappedElement.Selected, driver.DriverConfiguration.TimeoutToRetryClickIfFailed);
            }
        }

        public System.Drawing.Size Size
        {
            get
            {
                return RetryFuncIfFailed(() => WrappedElement.Size, driver.DriverConfiguration.TimeoutToRetryClickIfFailed);
            }
        }

        public string TagName
        {
            get
            {
                return RetryFuncIfFailed(() => WrappedElement.TagName, driver.DriverConfiguration.TimeoutToRetryClickIfFailed);
            }
        }

        public string Text
        {
            get
            {
               return RetryFuncIfFailed(() => WrappedElement.Text, driver.DriverConfiguration.TimeoutToRetryClickIfFailed);
            }
        }

        public IWebElement WrappedElement
        {
            get
            {
                wrappedElement = wrappedElement == null || IsStaleElementReference() ? FindTheElementAgain() : wrappedElement;
                return wrappedElement;
            }
        }

        public IWebDriver WrappedDriver => driver;

        public void Clear()
        {
            RetryActionIfFailed(() => WrappedElement.Clear(), driver.DriverConfiguration.TimeoutToRetryClickIfFailed);
        }

        public void Click()
        {
            var wait = new WebDriverWait(driver, driver.DriverConfiguration.TimeToWaitUntilElementClickable);
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(WrappedElement));
            }
            catch (Exception)
            {
                // Ignored, because if element is not enabled, we can still click on it
                // if element is not visible, an exception will be thrown from the next method (actual click)
            }
            RetryActionIfFailed(() => WrappedElement.Click(), driver.DriverConfiguration.TimeoutToRetryClickIfFailed);
        }

        public T RetryFuncIfFailed<T>(Func<T> func, TimeSpan timeout)
        {
            var timeoutDateTime = DateTime.Now.Add(timeout);
            Exception exception = null;
            T returnVal = default(T);
            do
            {
                try
                {
                    returnVal = func();
                    exception = null;
                }
                catch (Exception e) { exception = e; }
            } while (exception != null && DateTime.Now <= timeoutDateTime);
            if (exception != null)
                throw exception;
            return returnVal;
        }

        public IWebElement FindElement(By by)
        {
            var element = WrappedElement.FindElement(by);
            return new StableWebElement(element, by, this, driver);
        }

        ReadOnlyCollection<IWebElement> ISearchContext.FindElements(By by)
        {
            return new ReadOnlyCollection<IWebElement>(FindElements(by).Cast<IWebElement>().ToList());
        }

        public ReadOnlyCollection<StableWebElement> FindElements(By by)
        {
            var elements = WrappedElement.FindElements(by);
            return elements.ToStableWebElementCollection(by, driver);
        }

        public string GetAttribute(string attributeName)
        {
            return RetryFuncIfFailed(() => WrappedElement.GetAttribute(attributeName), driver.DriverConfiguration.TimeoutToRetryClickIfFailed);
        }

        public string GetCssValue(string propertyName)
        {
            return RetryFuncIfFailed(() => WrappedElement.GetCssValue(propertyName), driver.DriverConfiguration.TimeoutToRetryClickIfFailed);
        }

        public void SendKeys(string text)
        {
            RetryActionIfFailed(() => WrappedElement.SendKeys(text), driver.DriverConfiguration.TimeoutToRetryClickIfFailed);
        }

        public void Submit()
        {
            RetryActionIfFailed(() => WrappedElement.Submit(), driver.DriverConfiguration.TimeoutToRetryClickIfFailed);
        }

        private IWebElement FindTheElementAgain()
        {
            if(isElementFromList)
            {
                var elements = driver.WrappedDriver.FindElements(By);
                if (elements.Count <= ElementIndex)
                    throw new NoSuchElementException("Can't find element: " + By + " at index:" + ElementIndex + " .there are only: " + elements.Count + " elements");
                return elements[ElementIndex];
            }
            if(parentElement != null)
            {
                return parentElement.FindElement(by);
            }
            return driver.WrappedDriver.FindElement(By);
        }

        private bool IsStaleElementReference()
        {
            try
            {
                bool d = wrappedElement.Displayed;
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return true;
            }
        }
        private void RetryActionIfFailed(Action action,TimeSpan timeout)
        {
            var timeoutDateTime = DateTime.Now.Add(timeout);
            Exception exception = null;
            do
            {
                try
                {
                    action();
                    exception = null;
                }
                catch (Exception e) { exception = e; }
            } while (exception != null && DateTime.Now <= timeoutDateTime);
            if (exception != null)
                throw exception;
        }
    }
}
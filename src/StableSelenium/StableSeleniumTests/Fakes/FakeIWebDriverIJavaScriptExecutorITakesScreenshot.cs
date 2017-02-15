using OpenQA.Selenium;
using StableSelenium.Tests.WebElementTests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace StableSelenium.Tests.Fakes
{
    public class FakeIWebDriverIJavaScriptExecutorITakesScreenshot : IWebDriver, IJavaScriptExecutor, ITakesScreenshot,IMock
    {
        public MethodBase LastMethodCalled { get; private set; }

        public string CurrentWindowHandle
        {
            get
            {
                LastMethodCalled = MethodBase.GetCurrentMethod();
                return "";
            }
        }

        public string PageSource
        {
            get
            {
                LastMethodCalled = MethodBase.GetCurrentMethod();
                return "";
            }
        }

        public string Title
        {
            get
            {
                LastMethodCalled = MethodBase.GetCurrentMethod();
                return "";
            }
        }

        public string Url
        {
            get
            {
                LastMethodCalled = MethodBase.GetCurrentMethod();
                return "";
            }

            set
            {
                LastMethodCalled = MethodBase.GetCurrentMethod();
                var d = value;
            }
        }


        public ReadOnlyCollection<string> WindowHandles
        {
            get
            {
                LastMethodCalled = MethodBase.GetCurrentMethod();
                return null;
            }
        }

        ReadOnlyCollection<string> IWebDriver.WindowHandles
        {
            get
            {
                LastMethodCalled = MethodBase.GetCurrentMethod();
                return null;
            }
        }

        public void Close()
        {
            LastMethodCalled = MethodBase.GetCurrentMethod();
        }

        public void Dispose()
        {
            LastMethodCalled = MethodBase.GetCurrentMethod();
        }

        public object ExecuteAsyncScript(string script, params object[] args)
        {
            LastMethodCalled = MethodBase.GetCurrentMethod();
            return null;
        }

        public object ExecuteScript(string script, params object[] args)
        {
            LastMethodCalled = MethodBase.GetCurrentMethod();
            return null;
        }

        public IWebElement FindElement(By by)
        {
            LastMethodCalled = MethodBase.GetCurrentMethod();
            return new FakeWebElement();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            LastMethodCalled = MethodBase.GetCurrentMethod();
            return new ReadOnlyCollection<IWebElement>(
                new List<IWebElement> { new FakeWebElement() });
        }

        public Screenshot GetScreenshot()
        {
            LastMethodCalled = MethodBase.GetCurrentMethod();
            return null;
        }

        public IOptions Manage()
        {
            LastMethodCalled = MethodBase.GetCurrentMethod();
            return new FakeOptions(this);
        }

        public INavigation Navigate()
        {
            LastMethodCalled = MethodBase.GetCurrentMethod();
            return null;
        }

        public void Quit()
        {
            LastMethodCalled = MethodBase.GetCurrentMethod();
        }

        public ITargetLocator SwitchTo()
        {
            LastMethodCalled = MethodBase.GetCurrentMethod();
            return null;
        }

        ReadOnlyCollection<IWebElement> ISearchContext.FindElements(By by)
        {
            return FindElements(by);
        }
    }

        internal class FakeOptions : IOptions
    {
        private IWebDriver driver;

        public FakeOptions(IWebDriver driver)
        {
            this.driver = driver;
        }

        public ICookieJar Cookies
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ILogs Logs
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IWindow Window
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ITimeouts Timeouts()
        {
            return new FakeITimeouts(driver);
        }
    }

    internal class FakeITimeouts : ITimeouts
    {
        private IWebDriver mockWebDriver;
        public TimeSpan TimeToWaitForImplicitlyWait;

        public FakeITimeouts(IWebDriver mockWebDriver)
        {
            this.mockWebDriver = mockWebDriver;
        }

        public ITimeouts ImplicitlyWait(TimeSpan timeToWait)
        {
            TimeToWaitForImplicitlyWait = timeToWait;
            return this;
        }

        public ITimeouts SetPageLoadTimeout(TimeSpan timeToWait)
        {
            throw new NotImplementedException();
        }

        public ITimeouts SetScriptTimeout(TimeSpan timeToWait)
        {
            throw new NotImplementedException();
        }
    }

    
}

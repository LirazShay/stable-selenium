using OpenQA.Selenium;
using OpenQA.Selenium.Html5;
using System.Collections.ObjectModel;
using System.Reflection;

namespace StableSelenium.Tests.Fakes
{
    internal class FakeIWebDriverIJavaScriptExecutorITakesScreenshotIHasWebStorage : IWebDriver, IJavaScriptExecutor, ITakesScreenshot, IHasWebStorage,IMock
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

        public bool HasWebStorage
        {
            get
            {
                LastMethodCalled = MethodBase.GetCurrentMethod();
                return true;
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

        public IWebStorage WebStorage
        {
            get
            {
                LastMethodCalled = MethodBase.GetCurrentMethod();
                return null;
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
            return null;
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            LastMethodCalled = MethodBase.GetCurrentMethod();
            return null;
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
            LastMethodCalled = MethodBase.GetCurrentMethod();
            return null;
        }
    }
}

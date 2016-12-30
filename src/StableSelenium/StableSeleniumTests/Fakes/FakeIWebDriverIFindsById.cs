using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.ObjectModel;

namespace StableSelenium.Tests.Fakes
{
    class FakeIWebDriverIFindsById : IWebDriver,IFindsById,IJavaScriptExecutor,ITakesScreenshot
    {
        public string CurrentWindowHandle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string PageSource
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Title
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Url
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object ExecuteAsyncScript(string script, params object[] args)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScript(string script, params object[] args)
        {
            throw new NotImplementedException();
        }

        public IWebElement FindElement(By by)
        {
            throw new NotImplementedException();
        }

        public IWebElement FindElementById(string id)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<IWebElement> FindElementsById(string id)
        {
            throw new NotImplementedException();
        }

        public Screenshot GetScreenshot()
        {
            throw new NotImplementedException();
        }

        public IOptions Manage()
        {
            return new FakeOptions(this);
        }

        public INavigation Navigate()
        {
            throw new NotImplementedException();
        }

        public void Quit()
        {
            
        }

        public ITargetLocator SwitchTo()
        {
            throw new NotImplementedException();
        }
    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Html5;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace StableSelenium
{
    /// <summary>
    /// This class is a wrapper for any instance of IWebDriver,
    /// Under the hood, many of the exceptions that were usually thrown by Selenium will be handled.
    /// As a result, only exceptions that cannot be handled by code will be thrown (like wrong element locator - will throw NoSuchElementException)  
    /// this framework solves those issues by automatically waiting and retrying if an action was failed 
    /// </summary>
    public class StableWebDriver : IWebDriver, IWrapsDriver, IJavaScriptExecutor,ITakesScreenshot, IHasWebStorage
    {
        readonly IWebDriver driver;
        readonly IJavaScriptExecutor javaScriptExecutor;
        readonly ITakesScreenshot takesScreenshot;

        public StableWebDriver(IWebDriver driver) : this(driver,DriverConfiguration.CreateDefaultConfiguration())
        {
        }

        public DriverConfiguration DriverConfiguration { get; set; }

        public StableWebDriver(IWebDriver driver, DriverConfiguration config)
        {
            DriverConfiguration = config;
            this.driver = driver;
            javaScriptExecutor = DriverAs<IJavaScriptExecutor>();
            if (javaScriptExecutor == null)
                throw new InvalidOperationException(nameof(driver) + " or its wrapped driver must implement interface IJavaScriptExecutor");
            takesScreenshot = DriverAs<ITakesScreenshot>();
            if (takesScreenshot == null)
                throw new InvalidOperationException(nameof(driver) + " or its wrapped driver must implement interface ITakesScreenshot");

            driver.Manage().Timeouts().ImplicitlyWait(config.ImplicitlyWaitTimeout);
        }

        public string CurrentWindowHandle => driver.CurrentWindowHandle;

        public bool HasWebStorage
        {
            get
            {
                var ws = DriverAs<IHasWebStorage>();
                return ws != null && ws.HasWebStorage;
            }
        }

        public string PageSource => driver.PageSource;

        public string Title => driver.Title;

        public string Url
        {
            get
            {
                return driver.Url;
            }

            set
            {
                driver.Url = value;
            }
        }

        public IWebStorage WebStorage
        {
            get
            {
                var iHasWebStorage = DriverAs<IHasWebStorage>();
                if (iHasWebStorage == null)
                    throw new InvalidOperationException(nameof(driver) + " or its wrapped driver must implement interface IWebStorage");

                return iHasWebStorage.WebStorage;
            }
        }

        public ReadOnlyCollection<string> WindowHandles => driver.WindowHandles;

        public IWebDriver WrappedDriver => driver;

        /// <summary>
        /// Cast the IWebDriver instance to T, 
        /// if the instance is not T but is a wrapper for driver (IWrapsDriver)
        /// then check if the WrappedDriver is T and so on - until the original driver
        /// if any of the drivers are T - the result will be null
        /// </summary>
        /// <typeparam name="T">Cast the driver as T</typeparam>
        /// <returns></returns>
        public T DriverAs<T>() where T : class
        { 
            T t = driver as T;
            var driverWrapper = driver as IWrapsDriver;
            while (t == null && driverWrapper != null)
            {
                t = driverWrapper.WrappedDriver as T;
                driverWrapper = driverWrapper.WrappedDriver as IWrapsDriver;
            }
            return t;
        }

        public void Close() => driver.Close();

        public void Dispose() => driver.Dispose();

        public object ExecuteAsyncScript(string script, params object[] args)
            => javaScriptExecutor.ExecuteAsyncScript(script, args);

        public object ExecuteScript(string script, params object[] args)
            => javaScriptExecutor.ExecuteScript(script, args);

        IWebElement ISearchContext.FindElement(By by)
        {
            return FindElement(by);
        }

        public StableWebElement FindElement(By by)
        {
            return new StableWebElement(null, by, this);
        }

        public ReadOnlyCollection<StableWebElement> FindElements(By by)
        {
            var elements = driver.FindElements(by);
            return elements.ToStableWebElementCollection(by,this);
        }

        public Screenshot GetScreenshot() => takesScreenshot.GetScreenshot();

        public IOptions Manage() => driver.Manage();

        public INavigation Navigate() => driver.Navigate();

        public void Quit() => driver.Quit();

        public ITargetLocator SwitchTo() => driver.SwitchTo();

        ReadOnlyCollection<IWebElement> ISearchContext.FindElements(By by)
        {
            return new ReadOnlyCollection<IWebElement>(FindElements(by).Cast<IWebElement>().ToList());
        }
    }
}

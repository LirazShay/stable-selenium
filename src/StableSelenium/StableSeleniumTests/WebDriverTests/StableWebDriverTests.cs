using NUnit.Framework;
using OpenQA.Selenium.Internal;
using StableSelenium.Tests.Support;
using StableSelenium.Tests.Fakes;
using System;

namespace StableSelenium.Tests.WebDriverTests
{
    [TestFixture]
    public class StableWebDriverTests  : TestBase
    {
        [Test]
        public void Ctor_WhenCalledWithoutConfiguration_ShouldCreateDefaultConfiguration()
        {
            var driver = new StableWebDriver(new FakeIWebDriverIJavaScriptExecutorITakesScreenshot());
            var defaultConfig = DriverConfiguration.CreateDefaultConfiguration();
            Assert.AreEqual(defaultConfig.ImplicitlyWaitTimeout, driver.DriverConfiguration.ImplicitlyWaitTimeout);
            Assert.AreEqual(defaultConfig.TimeoutToRetryClickIfFailed, driver.DriverConfiguration.TimeoutToRetryClickIfFailed);
            Assert.AreEqual(defaultConfig.TimeToWaitUntilElementClickable, driver.DriverConfiguration.TimeToWaitUntilElementClickable);
        }

        [Test]
        public void Ctor_WhenCalledWithConfiguration_ShouldBeWithCorrectConfiguration()
        {
            var config = DriverConfiguration.CreateDefaultConfiguration();
            config.ImplicitlyWaitTimeout = TimeSpan.FromDays(2);
            var driver = new StableWebDriver(new FakeIWebDriverIJavaScriptExecutorITakesScreenshot(), config);
            Assert.AreEqual(config.ImplicitlyWaitTimeout, driver.DriverConfiguration.ImplicitlyWaitTimeout);
        }

        [Test]
        public void Ctor_WhenPassingDriverThatDoesNotImplementIJavaScriptExecutor_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(
                () =>
                {
                    var driver = new FakeIWebDriverOnly();
                    var stdriver = new StableWebDriver(driver);
                });
        }


        [Test]
        public void Ctor_WhenPassingDriverThatDoesNotImplementITakesScreenshot_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(
                () =>
                {
                    var driver = new FakeIWebDriverNotITakesScreenshot();
                    var stdriver = new StableWebDriver(driver);
                });
        }


        [Test]
        public void HasWebStorage_WhenWrappedDriverWithoutWebStorage_ShouldReturnFalse()
        {
            var driver = new StableWebDriver(new FakeIWebDriverIJavaScriptExecutorITakesScreenshot());
            Assert.False(Driver.HasWebStorage);
        }

        [Test]
        public void HasWebStorage_WhenWrappedDriverWithWebStorage_ShouldReturnTrue()
        {
            var dr = new FakeIWebDriverIJavaScriptExecutorITakesScreenshotIHasWebStorage();
            var driver = new StableWebDriver(dr);
            Assert.True(driver.HasWebStorage);
        }


        [Test]
        public void DriverAs_WhenWrappedDriverCanBeCastedToAnotherType_ShouldReturnCastedObject()
        {
            var driver = new FakeIWebDriverIFindsById();
            var stableWD = new StableWebDriver(driver);
            Assert.IsNull(stableWD as IFindsById);
            Assert.IsNotNull(stableWD.DriverAs<IFindsById>());
        }


        [Test,UseMockBrowser]
        public void Manage_WhenCalled_ShouldCallWrappedDriverManage()
        {
            var driver = Driver.WrappedDriver as IMock;
            Driver.Manage();
            Assert.AreEqual("Manage", driver.LastMethodCalled.Name);
        }

        [Test, UseMockBrowser]
        public void Navigate_WhenCalled_ShouldCallWrappedDriverNavigate()
        {
            var driver = Driver.WrappedDriver as IMock;
            Driver.Navigate();
            Assert.AreEqual("Navigate", driver.LastMethodCalled.Name);
        }

        [Test, UseMockBrowser]
        public void SwitchTo_WhenCalled_ShouldCallWrappedDriverSwitchTo()
        {
            var driver = Driver.WrappedDriver as IMock;
            Driver.SwitchTo();
            Assert.AreEqual("SwitchTo", driver.LastMethodCalled.Name);
        }

        [Test, UseMockBrowser]
        public void GetScreenshot_WhenCalled_ShouldCallWrappedDriverGetScreenshot()
        {
            var driver = Driver.WrappedDriver as IMock;
            Driver.GetScreenshot();
            Assert.AreEqual("GetScreenshot", driver.LastMethodCalled.Name);
        }

        [Test, UseMockBrowser]
        public void Close_WhenCalled_ShouldCallWrappedDriverClose()
        {
            var driver = Driver.WrappedDriver as IMock;
            Driver.Close();
            Assert.AreEqual("Close", driver.LastMethodCalled.Name);
        }


        [Test, UseMockBrowser]
        public void Dispose_WhenCalled_ShouldCallWrappedDriverDispose()
        {
            var driver = Driver.WrappedDriver as IMock;
            Driver.Dispose();
            Assert.AreEqual("Dispose", driver.LastMethodCalled.Name);
        }

        [Test, UseMockBrowser]
        public void ExecuteAsyncScript_WhenCalled_ShouldCallWrappedDriverExecuteAsyncScript()
        {
            var driver = Driver.WrappedDriver as IMock;
            Driver.ExecuteAsyncScript("");
            Assert.AreEqual("ExecuteAsyncScript", driver.LastMethodCalled.Name);
        }

        [Test, UseMockBrowser]
        public void ExecuteScript_WhenCalled_ShouldCallWrappedDriverExecuteScript()
        {
            var driver = Driver.WrappedDriver as IMock;
            Driver.ExecuteScript("");
            Assert.AreEqual("ExecuteScript", driver.LastMethodCalled.Name);
        }
        

        [Test, UseMockBrowser]
        public void Quit_WhenCalled_ShouldCallWrappedDriverQuit()
        {
            var driver = Driver.WrappedDriver as IMock;
            Driver.Quit();
            Assert.AreEqual("Quit", driver.LastMethodCalled.Name);
        }
       
    }

    
}
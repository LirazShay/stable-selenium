using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using StableSelenium.Tests.Fakes;
using StableSelenium.Tests.TestUtils;
using NSubstitute;
using System;
using OpenQA.Selenium.Html5;

namespace StableSelenium.Tests.UnitTests
{
    [TestFixture, Category("UnitTests")]
    public class StableWebDriverTests  : TestBase
    {
        private IWebDriver CreateDefaultFakeDriver()
        {
            return Substitute.For<IWebDriver, IJavaScriptExecutor, ITakesScreenshot>();
        }

        [Test]
        public void Ctor_WhenCalledWithoutConfiguration_ShouldCreateDefaultConfiguration()
        {
            var fakeDriver = CreateDefaultFakeDriver();
            var driver = new StableWebDriver(fakeDriver);
            var defaultConfig = new DriverConfiguration();
            Assert.AreEqual(defaultConfig.ImplicitlyWaitTimeout, driver.DriverConfiguration.ImplicitlyWaitTimeout);
            Assert.AreEqual(defaultConfig.TimeoutToRetryClickIfFailed, driver.DriverConfiguration.TimeoutToRetryClickIfFailed);
            Assert.AreEqual(defaultConfig.TimeToWaitUntilElementClickable, driver.DriverConfiguration.TimeToWaitUntilElementClickable);
        }

        [Test]
        public void Ctor_WhenCalledWithConfiguration_ShouldBeWithCorrectConfiguration()
        {
            var config = new DriverConfiguration();
            config.ImplicitlyWaitTimeout = TimeSpan.FromDays(2);
            var driver = new StableWebDriver(CreateDefaultFakeDriver(), config);
            Assert.AreEqual(config.ImplicitlyWaitTimeout, driver.DriverConfiguration.ImplicitlyWaitTimeout);
        }

        [Test]
        public void Ctor_WhenPassingDriverThatDoesNotImplementIJavaScriptExecutor_ShouldThrowException()
        {
            IWebDriver fakeDriver = Substitute.For<IWebDriver>();
               
            var ex = Assert.Throws<InvalidOperationException>(
                () =>
                {
                    var stdriver = new StableWebDriver(fakeDriver);
                });

            Assert.IsTrue(ex.Message.Contains("must implement interface IJavaScriptExecutor"));
        }


        [Test]
        public void Ctor_WhenPassingDriverThatDoesNotImplementITakesScreenshot_ShouldThrowException()
        {
            var fakeDriver = Substitute.For<IWebDriver, IJavaScriptExecutor>();

            Assert.Throws<InvalidOperationException>(
                () =>
                {
                    var stdriver = new StableWebDriver(fakeDriver);
                });
        }


        [Test]
        public void HasWebStorage_WhenWrappedDriverWithoutWebStorage_ShouldReturnFalse()
        {
            var fakeDriver = CreateDefaultFakeDriver();
            var driver = new StableWebDriver(fakeDriver);
            Assert.False(Driver.HasWebStorage);
        }

        [Test]
        public void HasWebStorage_WhenWrappedDriverWithWebStorage_ShouldReturnTrue()
        {
            var fakeDriverWithWebStorage = Substitute.For(new[] { typeof(IWebDriver), typeof(ITakesScreenshot), typeof(IJavaScriptExecutor), typeof(IHasWebStorage) },new object[] {});
            (fakeDriverWithWebStorage as IHasWebStorage).HasWebStorage.Returns(true);
            var driver = new StableWebDriver(fakeDriverWithWebStorage as IWebDriver);

            var hasWebStorage = driver.HasWebStorage;

            Assert.True(hasWebStorage);
        }



        [Test]
        public void DriverAs_WhenWrappedDriverCanBeCastedToAnotherType_ShouldReturnCastedObject()
        {
            var driver = Substitute.For(new Type[] { typeof(IFindsById), typeof(IWebDriver), typeof(IJavaScriptExecutor), typeof(ITakesScreenshot) },null);
            var stableWD = new StableWebDriver(driver as IWebDriver);

            IFindsById findsById = stableWD.DriverAs<IFindsById>();
            
            Assert.IsNotNull(findsById);
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
using OpenQA.Selenium.Chrome;
using StableSelenium.Tests.Fakes;


namespace StableSelenium.Tests.TestUtils
{
    public class StableWebDriverFactory
    {

        public static StableWebDriver CreateMockDriver()
        {
            var dr = new FakeIWebDriverIJavaScriptExecutorITakesScreenshot();
            var driver = new StableWebDriver(dr);
            return driver;
        }


        public static StableWebDriver CreateRealDriver()
        {
            var dr = new ChromeDriver();
            return new StableWebDriver(dr);
        }


    }
}

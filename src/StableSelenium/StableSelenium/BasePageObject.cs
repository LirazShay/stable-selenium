using OpenQA.Selenium.Support.PageObjects;

namespace StableSelenium
{
    /// <summary>
    /// This class is the base class for page objects,
    /// when you create a page object class, make it inherit from this class
    /// this class responsible for initializing the elements in the Page Object,
    /// so you don't need to call PageFactory.InitElements in any other place, it is called here.
    /// </summary>
    public abstract class BasePageObject
    {
        protected StableWebDriver Driver { get; set; }

        public BasePageObject(StableWebDriver driver)
        {
            Driver = driver;
            PageFactory.InitElements(Driver,this);
        }
    }
}

/*
 * https://github.com/SeleniumHQ/selenium/blob/master/dotnet/src/support/PageObjects/PageFactory.cs
 * https://github.com/SeleniumHQ/selenium/blob/master/dotnet/src/support/PageObjects/DefaultElementLocator.cs
 * https://github.com/SeleniumHQ/selenium/blob/ceaf3da79542024becdda5953059dfbb96fb3a90/dotnet/src/support/PageObjects/DefaultPageObjectMemberDecorator.cs
 * https://github.com/SeleniumHQ/selenium/blob/master/dotnet/src/support/PageObjects/FindsByAttribute.cs
 * https://github.com/SeleniumHQ/selenium/blob/ceaf3da79542024becdda5953059dfbb96fb3a90/dotnet/src/support/PageObjects/ByFactory.cs
 * https://github.com/SeleniumHQ/selenium/blob/ceaf3da79542024becdda5953059dfbb96fb3a90/dotnet/src/support/PageObjects/WebElementListProxy.cs
 * https://github.com/SeleniumHQ/selenium/blob/ceaf3da79542024becdda5953059dfbb96fb3a90/dotnet/src/support/PageObjects/WebDriverObjectProxy.cs
 * https://github.com/SeleniumHQ/selenium/tree/master/dotnet/src/support/PageObjects
 */

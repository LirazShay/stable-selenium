using OpenQA.Selenium;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StableSelenium
{
    public static class IWebElementExtensions
    {
        public static StableWebElement ToStableWebElement(this IWebElement element,By by,StableWebDriver driver)
        {
            if (element is StableWebElement)
                return element as StableWebElement;
            return new StableWebElement(element, by, driver);
        }

        public static ReadOnlyCollection<StableWebElement> ToStableWebElementCollection(this ReadOnlyCollection<IWebElement> elements, By by, StableWebDriver driver)
        {
            var stableWebElements = new List<StableWebElement>();
            for (int i = 0; i < elements.Count; i++)
            {
                stableWebElements.Add(
                    new StableWebElement(elements[i], by, i, driver));
            }
            return new ReadOnlyCollection<StableWebElement>(stableWebElements);
        }
    }
}

using OpenQA.Selenium;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StableSelenium
{
    public static class IWebElementExtensions
    {
        public static StableWebElement ToStableWebElement(this IWebElement element,By by,StableWebDriver driver)
        {
            var stbElem = element as StableWebElement;
            if (stbElem == null)
                stbElem = new StableWebElement(element, by, driver);
            return stbElem;
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

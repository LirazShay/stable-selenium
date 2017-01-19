using OpenQA.Selenium;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using StableSelenium.Tests.Fakes;
using System.Reflection;

namespace StableSelenium.Tests.WebElementTests
{
    public class FakeWebElement : IWebElement,IMock
    {
        public bool BooleanReturnValue;
        public string StringReturnValue;
        public object LastParameterArgument;
        public Point PointReturnValue;
        public Size SizeReturnValue;

        public bool Displayed
        {
            get
            {
                LastMethodCalled = MethodBase.GetCurrentMethod();
                return BooleanReturnValue;
            }
        }

        public bool Enabled
        {
            get
            {
                LastMethodCalled = MethodBase.GetCurrentMethod();
                return BooleanReturnValue;
            }
        }

        public Point Location
        {
            get
            {
                LastMethodCalled = MethodBase.GetCurrentMethod();
                return PointReturnValue;
            }
        }

        public bool Selected
        {
            get
            {
                LastMethodCalled = MethodBase.GetCurrentMethod();
                return BooleanReturnValue;
            }
        }

        public Size Size
        {
            get
            {
                LastMethodCalled = MethodBase.GetCurrentMethod();
                return SizeReturnValue;
            }
        }

        public string TagName
        {
            get
            {
                LastMethodCalled = MethodBase.GetCurrentMethod();
                return StringReturnValue;
            }
        }

        public string Text
        {
            get
            {
                LastMethodCalled = MethodBase.GetCurrentMethod();
                return StringReturnValue;
            }
        }

        public MethodBase LastMethodCalled
        {
            get; private set;
        }

        public void Clear()
        {
            LastMethodCalled = MethodBase.GetCurrentMethod();
        }

        public void Click()
        {
            LastMethodCalled = MethodBase.GetCurrentMethod();
        }

        public IWebElement FindElement(By by)
        {
            LastMethodCalled = MethodBase.GetCurrentMethod();
            return this;
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            LastParameterArgument = by;
            LastMethodCalled = MethodBase.GetCurrentMethod();
            return new ReadOnlyCollection<IWebElement>( new List<IWebElement> { this });
        }

        public string GetAttribute(string attributeName)
        {
            LastParameterArgument = attributeName;
            LastMethodCalled = MethodBase.GetCurrentMethod();
            return StringReturnValue;
        }

        public string GetCssValue(string propertyName)
        {
            LastParameterArgument = propertyName;
            LastMethodCalled = MethodBase.GetCurrentMethod();
            return StringReturnValue;
        }

        public void SendKeys(string text)
        {
            LastParameterArgument = text;
            LastMethodCalled = MethodBase.GetCurrentMethod();
        }

        public void Submit()
        {
            LastMethodCalled = MethodBase.GetCurrentMethod();
        }
    }
}

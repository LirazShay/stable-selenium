using System;

namespace StableSelenium
{
    public class DriverConfiguration
    {
        /// <summary>
        /// How much time to wait until element is clickable: enabled & visible
        /// before clicking on it.
        /// </summary>
        public TimeSpan TimeToWaitUntilElementClickable { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Timeout when stop trying to click an element and exception is thrown
        /// </summary>
        public TimeSpan TimeoutToRetryClickIfFailed { get; set; } = TimeSpan.FromSeconds(10);

        /// <summary>
        /// Specifies the amount of time the driver should wait when searching for an element
        /// if it is not immediately present, will be used for driver.Manage().Timeouts().ImplicitlyWait(timeToWait);
        /// </summary>
        public TimeSpan ImplicitlyWaitTimeout { get; set; } = TimeSpan.FromSeconds(20);
    }
}

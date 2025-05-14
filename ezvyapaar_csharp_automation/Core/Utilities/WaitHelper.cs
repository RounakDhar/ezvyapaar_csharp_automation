// 7. Wait Helper
namespace ezvyapaar_csharp_automation.core.Utilities
{
    using ezvyapaar_csharp_automation.core.Browser;
    using ezvyapaar_csharp_automation.core.Configuration;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;

    public static class WaitHelper
    {
        private static readonly ConfigManager _configManager = ConfigManager.Instance;

        public static IWebElement WaitForElement(By locator, int timeoutInSeconds = 0)
        {
            if (timeoutInSeconds <= 0)
            {
                timeoutInSeconds = _configManager.ExplicitWaitTimeInSeconds;
            }

            var wait = new WebDriverWait(BrowserFactory.Driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            return wait.Until(driver =>
            {
                var element = driver.FindElement(locator);
                return element.Displayed ? element : null;
            });
        }

        public static IWebElement WaitForElementToBeClickable(By locator, int timeoutInSeconds = 0)
        {
            if (timeoutInSeconds <= 0)
            {
                timeoutInSeconds = _configManager.ExplicitWaitTimeInSeconds;
            }

            var wait = new WebDriverWait(BrowserFactory.Driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        public static bool WaitForElementToBeInvisible(By locator, int timeoutInSeconds = 0)
        {
            if (timeoutInSeconds <= 0)
            {
                timeoutInSeconds = _configManager.ExplicitWaitTimeInSeconds;
            }

            var wait = new WebDriverWait(BrowserFactory.Driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public static void WaitForPageToLoad(int timeoutInSeconds = 0)
        {
            if (timeoutInSeconds <= 0)
            {
                timeoutInSeconds = _configManager.ExplicitWaitTimeInSeconds;
            }

            var wait = new WebDriverWait(BrowserFactory.Driver, TimeSpan.FromSeconds(timeoutInSeconds));

            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public static void WaitForJQueryToLoad(int timeoutInSeconds = 0)
        {
            if (timeoutInSeconds <= 0)
            {
                timeoutInSeconds = _configManager.ExplicitWaitTimeInSeconds;
            }

            var wait = new WebDriverWait(BrowserFactory.Driver, TimeSpan.FromSeconds(timeoutInSeconds));

            wait.Until(driver =>
            {
                var jQueryUndefined = (bool)((IJavaScriptExecutor)driver).ExecuteScript("return typeof jQuery == 'undefined'");
                if (jQueryUndefined)
                    return true;

                return (bool)((IJavaScriptExecutor)driver).ExecuteScript("return jQuery.active == 0");
            });
        }
    }
}

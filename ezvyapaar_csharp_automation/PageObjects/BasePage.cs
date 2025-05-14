// 1. Base Page
namespace ezvyapaar_csharp_automation.core.PageObjects
{
    using ezvyapaar_csharp_automation.core.Browser;
    using ezvyapaar_csharp_automation.core.Utilities;
    using OpenQA.Selenium;
    using System;

    public abstract class BasePage
    {
        protected IWebDriver Driver => BrowserFactory.Driver;

        public BasePage()
        {
            WaitHelper.WaitForPageToLoad();
        }

        protected virtual bool IsPageLoaded(By pageIdentifier)
        {
            try
            {
                WaitHelper.WaitForElement(pageIdentifier);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Page not loaded properly. Error: {ex.Message}");
                return false;
            }
        }

        protected virtual void Click(By locator)
        {
            try
            {
                var element = WaitHelper.WaitForElementToBeClickable(locator);
                element.Click();
                Logger.Debug($"Clicked on element: {locator}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to click on element {locator}. Error: {ex.Message}");
                throw;
            }
        }

        protected virtual void EnterText(By locator, string text)
        {
            try
            {
                var element = WaitHelper.WaitForElement(locator);
                element.Clear();
                element.SendKeys(text);
                Logger.Debug($"Entered text '{text}' in element: {locator}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to enter text in element {locator}. Error: {ex.Message}");
                throw;
            }
        }

        protected virtual string GetText(By locator)
        {
            try
            {
                var element = WaitHelper.WaitForElement(locator);
                var text = element.Text;
                Logger.Debug($"Retrieved text '{text}' from element: {locator}");
                return text;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to get text from element {locator}. Error: {ex.Message}");
                throw;
            }
        }

        protected virtual bool IsElementDisplayed(By locator, int timeoutInSeconds = 5)
        {
            try
            {
                WaitHelper.WaitForElement(locator, timeoutInSeconds);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected virtual void SelectDropdownByText(By locator, string text)
        {
            try
            {
                var element = WaitHelper.WaitForElement(locator);
                var selectElement = new OpenQA.Selenium.Support.UI.SelectElement(element);
                selectElement.SelectByText(text);
                Logger.Debug($"Selected '{text}' from dropdown: {locator}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to select from dropdown {locator}. Error: {ex.Message}");
                throw;
            }
        }

        protected virtual void ScrollToElement(By locator)
        {
            try
            {
                var element = WaitHelper.WaitForElement(locator);
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
                Logger.Debug($"Scrolled to element: {locator}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to scroll to element {locator}. Error: {ex.Message}");
                throw;
            }
        }
    }
}

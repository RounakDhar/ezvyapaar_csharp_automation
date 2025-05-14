using OpenQA.Selenium;
using System;
using System.IO;

namespace ezvyapaar_csharp_automation.core.Utilities
{
    public static class ScreenshotHelper
    {
        public static void TakeScreenshot(IWebDriver driver, string scenarioName)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            var fileName = $"{scenarioName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            var path = Path.Combine("Reports", fileName);
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
        }
    }
}

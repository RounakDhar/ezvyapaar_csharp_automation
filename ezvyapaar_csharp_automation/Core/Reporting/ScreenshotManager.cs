// 4. Screenshot Manager
namespace ezvyapaar_csharp_automation.core.Reporting
{
    using ezvyapaar_csharp_automation.core.Browser;
    using ezvyapaar_csharp_automation.core.Configuration;
    using OpenQA.Selenium;
    using System;
    using System.IO;

    public class ScreenshotManager
    {
        private static readonly ConfigManager _configManager = ConfigManager.Instance;

        public static string CaptureScreenshot(string testName)
        {
            try
            {
                IWebDriver driver = BrowserFactory.Driver;
                if (driver == null)
                {
                    return string.Empty;
                }

                string screenshotDir = _configManager.ScreenshotFolder;
                Directory.CreateDirectory(screenshotDir);

                string fileName = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                string filePath = Path.Combine(screenshotDir, fileName);

                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);

                return filePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to capture screenshot: {ex.Message}");
                return string.Empty;
            }
        }
    }
}

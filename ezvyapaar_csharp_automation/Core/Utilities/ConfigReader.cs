using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ezvyapaar_csharp_automation.core.Utilities
{
    public static class ConfigReader
    {
        private static IConfiguration _configuration;

        static ConfigReader()
        {
            // Initialize configuration from appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public static string GetBrowser()
        {
            return _configuration["BrowserSettings:BrowserType"] ?? "chrome";
        }

        public static string GetBaseUrl()
        {
            return _configuration["AppSettings:BaseUrl"] ?? "https://www.ezvyapaar.com/";
        }

        public static int GetImplicitWaitTime()
        {
            if (int.TryParse(_configuration["BrowserSettings:ImplicitWaitTimeInSeconds"], out int waitTime))
            {
                return waitTime;
            }
            return 30; // Default wait time
        }

        public static string GetTestDataPath(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "TestData", fileName);
        }

        public static string GetReportPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Reports");
        }

        public static string GetScreenshotPath()
        {
            string path = Path.Combine(GetReportPath(), "Screenshots");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public static string GetValue(string key)
        {
            return _configuration[key];
        }
    }
}

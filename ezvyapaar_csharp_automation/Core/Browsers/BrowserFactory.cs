// Core Framework Components:

// 1. Browser Factory
using ezvyapaar_csharp_automation.core.Configuration;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace ezvyapaar_csharp_automation.core.Browser
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Edge;
    using OpenQA.Selenium.Firefox;
    using System;
    using OpenQA.Selenium.Support.UI;
    using ezvyapaar_csharp_automation.core.Configuration;
  

    public enum BrowserType
    {
        Chrome,
        Firefox,
        Edge
    }

    public class BrowserFactory
    {
        private static readonly object _lock = new object();
        private static IDictionary<string, IWebDriver> _drivers = new Dictionary<string, IWebDriver>();
        private static readonly ConfigManager _configManager = ConfigManager.Instance;

        public static IWebDriver Driver
        {
            get
            {
                string threadId = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
                if (_drivers.ContainsKey(threadId))
                {
                    return _drivers[threadId];
                }
                return null;
            }
        }

        public static IWebDriver InitBrowser(BrowserType browserType = BrowserType.Chrome)
        {
            string threadId = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
            IWebDriver driver;

            if (_drivers.ContainsKey(threadId))
            {
                return _drivers[threadId];
            }

            switch (browserType)
            {
                case BrowserType.Chrome:
                    var chromeOptions = new ChromeOptions();
                    if (_configManager.HeadlessMode)
                    {
                        chromeOptions.AddArgument("--headless");
                    }
                    chromeOptions.AddArgument("--start-maximized");
                    driver = new ChromeDriver(chromeOptions);
                    break;
                case BrowserType.Firefox:
                    var firefoxOptions = new FirefoxOptions();
                    if (_configManager.HeadlessMode)
                    {
                        firefoxOptions.AddArgument("--headless");
                    }
                    driver = new FirefoxDriver(firefoxOptions);
                    break;
                case BrowserType.Edge:
                    var edgeOptions = new EdgeOptions();
                    if (_configManager.HeadlessMode)
                    {
                        edgeOptions.AddArgument("--headless");
                    }
                    edgeOptions.AddArgument("--start-maximized");
                    driver = new EdgeDriver(edgeOptions);
                    break;
                default:
                    throw new ArgumentException($"Browser type {browserType} is not supported.");
            }

            lock (_lock)
            {
                _drivers.Add(threadId, driver);
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_configManager.ImplicitWaitTimeInSeconds);
            driver.Manage().Window.Maximize();
            return driver;
        }

        public static void CloseAllDrivers()
        {
            foreach (var key in _drivers.Keys)
            {
                _drivers[key].Quit();
            }

            _drivers.Clear();
        }

        public static void CloseCurrentDriver()
        {
            string threadId = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();

            if (_drivers.ContainsKey(threadId))
            {
                _drivers[threadId].Quit();
                _drivers.Remove(threadId);
            }
        }
    }
}


// File: Utilities/BrowserFactory.cs
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Firefox;

//public class BrowserFactory
//{
//    public static IWebDriver InitBrowser(ConfigManager config)
//    {
//        IWebDriver driver;

//        if (config.Browser.ToLower() == "chrome")
//        {
//            var options = new ChromeOptions();
//            if (config.Headless) options.AddArgument("--headless");
//            driver = new ChromeDriver(options);
//        }
//        else if (config.Browser.ToLower() == "firefox")
//        {
//            var options = new FirefoxOptions();
//            if (config.Headless) options.AddArgument("--headless");
//            driver = new FirefoxDriver(options);
//        }
//        else
//        {
//            throw new ArgumentException("Unsupported browser specified.");
//        }

//        driver.Manage().Window.Maximize();
//        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(config.ImplicitWait);
//        return driver;
//    }
//}

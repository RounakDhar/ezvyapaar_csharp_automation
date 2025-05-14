// 2. Configuration Manager
using Newtonsoft.Json;

namespace ezvyapaar_csharp_automation.core.Configuration
{
    using System;
    using System.Configuration;

    public class ConfigManager
    {
        private static readonly Lazy<ConfigManager> _instance = new Lazy<ConfigManager>(() => new ConfigManager());

        public static ConfigManager Instance => _instance.Value;

        public string BaseUrl => GetAppSetting("BaseUrl", "https://www.ezvyapaar.com/");
        public BrowserType Browser => Enum.Parse<BrowserType>(GetAppSetting("Browser", "Chrome"));
        public bool HeadlessMode => bool.Parse(GetAppSetting("HeadlessMode", "false"));
        public int ImplicitWaitTimeInSeconds => int.Parse(GetAppSetting("ImplicitWaitTime", "10"));
        public int ExplicitWaitTimeInSeconds => int.Parse(GetAppSetting("ExplicitWaitTime", "30"));
        public string TestDataFolder => GetAppSetting("TestDataFolder", "TestData");
        public string ReportFolder => GetAppSetting("ReportFolder", "Reports");
        public string ScreenshotFolder => GetAppSetting("ScreenshotFolder", "Screenshots");
        public bool RunTestsInParallel => bool.Parse(GetAppSetting("RunTestsInParallel", "true"));
        public int MaxParallelThreads => int.Parse(GetAppSetting("MaxParallelThreads", "3"));
        public string LogLevel => GetAppSetting("LogLevel", "Info");

        private string GetAppSetting(string key, string defaultValue)
        {
            return ConfigurationManager.AppSettings[key] ?? defaultValue;
        }
    }
}


// File: Utilities/ConfigManager.cs
//using newtonsoft.json;

//public class configmanager
//{
//    public string baseurl { get; set; }
//    public string browser { get; set; }
//    public bool headless { get; set; }
//    public int implicitwait { get; set; }
//    public string screenshotpath { get; set; }
//    public string reportpath { get; set; }

//    public static configmanager load()
//    {
//        var json = file.readalltext("config/appsettings.json");
//        return jsonconvert.deserializeobject<configmanager>(json);
//    }
//}

// 3. Reporting - Extent Report Manager
namespace ezvyapaar_csharp_automation.core.Reporting
{
    using AventStack.ExtentReports;
    using AventStack.ExtentReports.Gherkin.Model;
    using AventStack.ExtentReports.Reporter;
    using ezvyapaar_csharp_automation.core.Configuration;
    using System;
    using System.IO;

    public class ExtentReportManager
    {
        private static readonly Lazy<ExtentReportManager> _instance = new Lazy<ExtentReportManager>(() => new ExtentReportManager());
        private readonly ExtentReports _extentReports;
        private readonly object _lock = new object();
        private readonly Dictionary<string, ExtentTest> _featureMap = new Dictionary<string, ExtentTest>();
        private readonly Dictionary<string, ExtentTest> _scenarioMap = new Dictionary<string, ExtentTest>();

        public static ExtentReportManager Instance => _instance.Value;

        private ExtentReportManager()
        {
            string reportPath = Path.Combine(ConfigManager.Instance.ReportFolder, $"ExtentReport_{DateTime.Now:yyyyMMdd_HHmmss}.html");

            Directory.CreateDirectory(ConfigManager.Instance.ReportFolder);

            var htmlReporter = new ExtentHtmlReporter(reportPath);
            htmlReporter.Config.DocumentTitle = "EzVyapaar Automation Test Report";
            htmlReporter.Config.ReportName = "EzVyapaar Website Automation Test Results";
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;

            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(htmlReporter);
            _extentReports.AddSystemInfo("Environment", "QA");
            _extentReports.AddSystemInfo("Browser", ConfigManager.Instance.Browser.ToString());
            _extentReports.AddSystemInfo("Application URL", ConfigManager.Instance.BaseUrl);
        }

        public ExtentTest CreateFeature(string featureName)
        {
            lock (_lock)
            {
                if (!_featureMap.ContainsKey(featureName))
                {
                    var feature = _extentReports.CreateTest<Feature>(featureName);
                    _featureMap.Add(featureName, feature);
                }
                return _featureMap[featureName];
            }
        }

        public ExtentTest CreateScenario(string featureName, string scenarioName)
        {
            var scenarioKey = $"{featureName}_{scenarioName}";

            lock (_lock)
            {
                if (!_scenarioMap.ContainsKey(scenarioKey))
                {
                    var feature = CreateFeature(featureName);
                    var scenario = feature.CreateNode<Scenario>(scenarioName);
                    _scenarioMap.Add(scenarioKey, scenario);
                }
                return _scenarioMap[scenarioKey];
            }
        }

        public void SetStepStatus(string featureName, string scenarioName, string stepName, string stepType, Status status, string details = "")
        {
            lock (_lock)
            {
                var scenarioKey = $"{featureName}_{scenarioName}";
                if (_scenarioMap.ContainsKey(scenarioKey))
                {
                    var scenario = _scenarioMap[scenarioKey];
                    ExtentTest step;

                    switch (stepType.ToLower())
                    {
                        case "given":
                            step = scenario.CreateNode<Given>(stepName);
                            break;
                        case "when":
                            step = scenario.CreateNode<When>(stepName);
                            break;
                        case "then":
                            step = scenario.CreateNode<Then>(stepName);
                            break;
                        case "and":
                            step = scenario.CreateNode<And>(stepName);
                            break;
                        default:
                            step = scenario.CreateNode<And>(stepName);
                            break;
                    }

                    switch (status)
                    {
                        case Status.Pass:
                            step.Pass(details);
                            break;
                        case Status.Fail:
                            step.Fail(details);
                            break;
                        case Status.Skip:
                            step.Skip(details);
                            break;
                        case Status.Warning:
                            step.Warning(details);
                            break;
                        default:
                            step.Info(details);
                            break;
                    }
                }
            }
        }

        public void AddScreenshot(string featureName, string scenarioName, string stepName, string screenShotPath)
        {
            lock (_lock)
            {
                var scenarioKey = $"{featureName}_{scenarioName}";
                if (_scenarioMap.ContainsKey(scenarioKey))
                {
                    var scenario = _scenarioMap[scenarioKey];
                    scenario.Info("Screenshot: ", MediaEntityBuilder.CreateScreenCaptureFromPath(screenShotPath).Build());
                }
            }
        }

        public void FlushReport()
        {
            _extentReports.Flush();
        }
    }
}
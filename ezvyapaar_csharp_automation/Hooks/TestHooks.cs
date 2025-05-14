using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using ezvyapaar_csharp_automation.core.Browser;
using ezvyapaar_csharp_automation.core.Configuration;
using ezvyapaar_csharp_automation.core.Reporting;
using ezvyapaar_csharp_automation.core.Utilities;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using BoDi;


namespace EzVyapaar.Automation.Hooks
{
    [Binding]
    public class TestHooks
    {
        private readonly IObjectContainer _container;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private static ExtentReportManager _reportManager;
        private IWebDriver _driver;
        private ExtentTest _feature;
        private ExtentTest _scenario;
        private readonly Logger _logger;

        public TestHooks(IObjectContainer container, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _container = container;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _logger = new Logger();
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _reportManager = new ExtentReportManager();
            _reportManager.InitializeReport();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _reportManager.FlushReport();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _reportManager.CreateFeature(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _logger.Info($"Starting scenario: {_scenarioContext.ScenarioInfo.Title}");

            // Initialize browser
            string browserType = ConfigManager.GetSetting("BrowserType") ?? "Chrome";
            _driver = BrowserFactory.GetDriver((BrowserType)Enum.Parse(typeof(BrowserType), browserType));

            // Configure browser
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(
                int.Parse(ConfigManager.GetSetting("ImplicitWaitInSeconds") ?? "10"));

            // Register driver in container for DI
            _container.RegisterInstanceAs(_driver);

            // Create scenario in report
            _scenario = _reportManager.CreateScenario(_scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            try
            {
                if (_scenarioContext.TestError != null)
                {
                    _logger.Error($"Scenario failed: {_scenarioContext.TestError.Message}");

                    // Capture screenshot for failed tests
                    string screenshotPath = ScreenshotManager.CaptureScreenshot(_driver, _scenarioContext.ScenarioInfo.Title);
                    _reportManager.AddScreenshotToReport(_scenario, screenshotPath);

                    // Log error details
                    _reportManager.LogErrorDetails(_scenario, _scenarioContext.TestError);
                }
                else
                {
                    _logger.Info($"Scenario passed: {_scenarioContext.ScenarioInfo.Title}");
                }
            }
            finally
            {
                // Close the browser
                if (_driver != null)
                {
                    _driver.Quit();
                }
            }
        }

        [BeforeStep]
        public void BeforeStep()
        {
            // Log step information
            _logger.Info($"Executing step: {_scenarioContext.StepContext.StepInfo.Text}");
        }

        [AfterStep]
        public void AfterStep()
        {
            // Add step details to report
            _reportManager.CreateStep(_scenario, _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString(),
                _scenarioContext.StepContext.StepInfo.Text, _scenarioContext.ScenarioExecutionStatus);

            // If step failed, capture and attach screenshot
            if (_scenarioContext.TestError != null)
            {
                _logger.Error($"Step failed: {_scenarioContext.TestError.Message}");
                string stepScreenshotPath = ScreenshotManager.CaptureScreenshot(_driver, $"{_scenarioContext.StepContext.StepInfo.Text}");
                _reportManager.AddScreenshotToReport(_scenario, stepScreenshotPath);
            }
        }
    }
}

//--based on perplexity solution--
//using ezvyapaar_csharp_automation.core.utilities;
//using openqa.selenium;
//using techtalk.specflow;

//namespace ezvyapaarautomation.hooks
//{
//    [binding]
//    public class testhooks
//    {
//        private readonly scenariocontext _scenariocontext;

//        public testhooks(scenariocontext scenariocontext)
//        {
//            _scenariocontext = scenariocontext;
//        }

//        [afterstep]
//        public void takescreenshotonfailure()
//        {
//            if (_scenariocontext.testerror != null)
//            {
//                var driver = (iwebdriver)_scenariocontext["webdriver"];
//                screenshothelper.takescreenshot(driver, _scenariocontext.scenarioinfo.title);
//            }
//        }
//    }
//}

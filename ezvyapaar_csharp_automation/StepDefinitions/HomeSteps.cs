// Step Definitions

// 1. Home Page Steps
namespace ezvyapaar_csharp_automation.core.StepDefinitions
{
    using ezvyapaar_csharp_automation.core.PageObjects;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    [Binding]
    public class HomeSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private HomePage _homePage;

        public HomeSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I am on the home page")]
        public void GivenIAmOnTheHomePage()
        {
            _homePage = new HomePage();
            _scenarioContext["HomePage"] = _homePage;
        }

        [Then(@"I should see the banner section")]
        public void ThenIShouldSeeTheBannerSection()
        {
            Assert.IsTrue(_homePage.IsBannerDisplayed(), "Banner section is not displayed on the home page");
        }

        [Then(@"I should see the featured products section")]
        public void ThenIShouldSeeTheFeaturedProductsSection()
        {
            Assert.IsTrue(_homePage.AreFeaturedProductsDisplayed(), "Featured products section is not displayed on the home page");
        }

        [Then(@"I should see the footer section")]
        public void ThenIShouldSeeTheFooterSection()
        {
            Assert.IsTrue(_homePage.IsFooterDisplayed(), "Footer section is not displayed on the home page");
        }

        [When(@"I click on the login button")]
        public void WhenIClickOnTheLoginButton()
        {
            _homePage.ClickOnLoginButton();
        }

        [When(@"I click on the cart icon")]
        public void WhenIClickOnTheCartIcon()
        {
            _homePage.ClickOnCartIcon();
        }

        [When(@"I search for ""(.*)""")]
        public void WhenISearchFor(string productName)
        {
            _homePage.SearchForProduct(productName);
        }

        [When(@"I click on the ""(.*)"" category")]
        public void WhenIClickOnTheCategory(string categoryName)
        {
            _homePage.ClickOnCategory(categoryName);
        }
    }
}

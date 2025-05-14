// 2. Login Steps
namespace ezvyapaar_csharp_automation.core.StepDefinitions
{
    using ezvyapaar_csharp_automation.core.PageObjects;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    [Binding]
    public class LoginSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private LoginPage _loginPage;

        public LoginSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            var homePage = new HomePage();
            homePage.ClickOnLoginButton();
            _loginPage = new LoginPage();
            _scenarioContext["LoginPage"] = _loginPage;
        }

        [Then(@"I should see the login form")]
        public void ThenIShouldSeeTheLoginForm()
        {
            Assert.IsTrue(_loginPage.IsLoginFormDisplayed(), "Login form is not displayed");
        }

        [When(@"I enter the email ""(.*)""")]
        public void WhenIEnterTheEmail(string email)
        {
            _loginPage.EnterEmail(email);
        }

        [When(@"I enter the password ""(.*)""")]
        public void WhenIEnterThePassword(string password)
        {
            _loginPage.EnterPassword(password);
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            _loginPage.ClickLoginButton();
        }

        [When(@"I login with email ""(.*)"" and password ""(.*)""")]
        public void WhenILoginWithEmailAndPassword(string email, string password)
        {
            _loginPage.Login(email, password);
        }

        [Then(@"I should see an error message ""(.*)""")]
        public void ThenIShouldSeeAnErrorMessage(string expectedErrorMessage)
        {
            var actualErrorMessage = _loginPage.GetErrorMessage();
            Assert.That(actualErrorMessage, Does.Contain(expectedErrorMessage), $"Error message does not match. Expected: {expectedErrorMessage}, Actual: {actualErrorMessage}");
        }

        [When(@"I click on the forgot password link")]
        public void WhenIClickOnTheForgotPasswordLink()
        {
            _loginPage.ClickForgotPasswordLink();
        }

        [When(@"I click on the register link")]
        public void WhenIClickOnTheRegisterLink()
        {
            _loginPage.ClickRegisterLink();
        }
    }
}
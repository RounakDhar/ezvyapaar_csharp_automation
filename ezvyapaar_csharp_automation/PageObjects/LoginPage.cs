// 3. Login Page
namespace ezvyapaar_csharp_automation.core.PageObjects
{
    using OpenQA.Selenium;
    using System;

    public class LoginPage : BasePage
    {
        // Locators
        private readonly By _emailInput = By.Id("email");
        private readonly By _passwordInput = By.Id("password");
        private readonly By _loginButton = By.CssSelector("button[type='submit']");
        private readonly By _forgotPasswordLink = By.LinkText("Forgot Password?");
        private readonly By _registerLink = By.LinkText("Register");
        private readonly By _errorMessage = By.CssSelector(".error-message");
        private readonly By _loginForm = By.CssSelector(".login-form");

        public LoginPage() : base()
        {
            if (!IsPageLoaded(_loginForm))
            {
                throw new Exception("Login page is not loaded properly");
            }
        }

        public void EnterEmail(string email)
        {
            EnterText(_emailInput, email);
        }

        public void EnterPassword(string password)
        {
            EnterText(_passwordInput, password);
        }

        public void ClickLoginButton()
        {
            Click(_loginButton);
        }

        public void ClickForgotPasswordLink()
        {
            Click(_forgotPasswordLink);
        }

        public void ClickRegisterLink()
        {
            Click(_registerLink);
        }

        public string GetErrorMessage()
        {
            if (IsElementDisplayed(_errorMessage))
            {
                return GetText(_errorMessage);
            }
            return string.Empty;
        }

        public void Login(string email, string password)
        {
            EnterEmail(email);
            EnterPassword(password);
            ClickLoginButton();
        }

        public bool IsLoginFormDisplayed()
        {
            return IsElementDisplayed(_loginForm);
        }
    }
}

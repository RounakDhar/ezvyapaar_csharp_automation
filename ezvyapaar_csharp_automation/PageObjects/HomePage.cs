// 2. Home Page
namespace ezvyapaar_csharp_automation.core.PageObjects
{
    using ezvyapaar_csharp_automation.core.Configuration;
    using OpenQA.Selenium;
    using System;

    public class HomePage : BasePage
    {
        // Locators
        private readonly By _headerLogo = By.CssSelector(".logo-wrapper img");
        private readonly By _searchBox = By.CssSelector("input[placeholder='Search for products']");
        private readonly By _searchButton = By.CssSelector("button.search-button");
        private readonly By _loginButton = By.CssSelector("a.login-button");
        private readonly By _cartIcon = By.CssSelector("a.cart-icon");
        private readonly By _categoryMenu = By.CssSelector(".category-menu");
        private readonly By _bannerSection = By.CssSelector(".banner-section");
        private readonly By _featuredProducts = By.CssSelector(".featured-products");
        private readonly By _newsletterSection = By.Id("newsletter-section");
        private readonly By _footerSection = By.CssSelector("footer");

        public HomePage() : base()
        {
            Driver.Navigate().GoToUrl(ConfigManager.Instance.BaseUrl);
            if (!IsPageLoaded(_headerLogo))
            {
                throw new Exception("Home page is not loaded properly");
            }
        }

        public bool IsBannerDisplayed()
        {
            return IsElementDisplayed(_bannerSection);
        }

        public bool AreFeaturedProductsDisplayed()
        {
            return IsElementDisplayed(_featuredProducts);
        }

        public bool IsFooterDisplayed()
        {
            return IsElementDisplayed(_footerSection);
        }

        public bool IsHeaderLogoDisplayed()
        {
            return IsElementDisplayed(_headerLogo);
        }

        public void ClickOnLoginButton()
        {
            Click(_loginButton);
        }

        public void ClickOnCartIcon()
        {
            Click(_cartIcon);
        }

        public void SearchForProduct(string productName)
        {
            EnterText(_searchBox, productName);
            Click(_searchButton);
        }

        public void ClickOnCategory(string categoryName)
        {
            By categoryLocator = By.XPath($"//div[@class='category-menu']//a[contains(text(), '{categoryName}')]");
            Click(categoryLocator);
        }
    }
}
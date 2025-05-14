// 4. Product Detail Steps
namespace ezvyapaar_csharp_automation.core.StepDefinitions
{
    using ezvyapaar_csharp_automation.core.PageObjects;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    [Binding]
    public class ProductDetailSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private ProductDetailPage _productDetailPage;

        public ProductDetailSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I am on the product detail page for ""(.*)""")]
        public void GivenIAmOnTheProductDetailPageFor(string productName)
        {
            var homePage = new HomePage();
            homePage.SearchForProduct(productName);

            var productsPage = new ProductsPage();
            productsPage.ClickOnProduct(productName);

            _productDetailPage = new ProductDetailPage();
            _scenarioContext["ProductDetailPage"] = _productDetailPage;
        }

        [Then(@"I should see the product title ""(.*)""")]
        public void ThenIShouldSeeTheProductTitle(string expectedTitle)
        {
            var actualTitle = _productDetailPage.GetProductTitle();
            Assert.That(actualTitle, Does.Contain(expectedTitle), $"Product title does not match. Expected: {expectedTitle}, Actual: {actualTitle}");
        }

        [Then(@"I should see the product description")]
        public void ThenIShouldSeeTheProductDescription()
        {
            var description = _productDetailPage.GetProductDescription();
            Assert.IsFalse(string.IsNullOrWhiteSpace(description), "Product description is empty");
        }

        [When(@"I set the quantity to (\d+)")]
        public void WhenISetTheQuantityTo(int quantity)
        {
            _productDetailPage.SetQuantity(quantity);
        }

        [When(@"I click the Add to Cart button")]
        public void WhenIClickTheAddToCartButton()
        {
            _productDetailPage.ClickAddToCartButton();
        }

        [When(@"I click the Buy Now button")]
        public void WhenIClickTheBuyNowButton()
        {
            _productDetailPage.ClickBuyNowButton();
        }

        [When(@"I add (\d+) quantity of the product to the cart")]
        public void WhenIAddQuantityOfTheProductToTheCart(int quantity)
        {
            _productDetailPage.AddToCart(quantity);
        }

        [Then(@"I should see the reviews section")]
        public void ThenIShouldSeeTheReviewsSection()
        {
            Assert.IsTrue(_productDetailPage.IsReviewsSectionDisplayed(), "Reviews section is not displayed");
        }

        [Then(@"I should see similar products section")]
        public void ThenIShouldSeeSimilarProductsSection()
        {
            Assert.IsTrue(_productDetailPage.IsSimilarProductsSectionDisplayed(), "Similar products section is not displayed");
        }
    }
}

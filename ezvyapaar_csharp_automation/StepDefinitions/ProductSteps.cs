// 3. Products Steps
namespace ezvyapaar_csharp_automation.core.StepDefinitions
{
    using ezvyapaar_csharp_automation.core.PageObjects;
    using NUnit.Framework;
    using TechTalk.SpecFlow;
    using System.Linq;

    [Binding]
    public class ProductsSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private ProductsPage _productsPage;

        public ProductsSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I am on the products page")]
        public void GivenIAmOnTheProductsPage()
        {
            var homePage = new HomePage();
            homePage.ClickOnCategory("All Products");
            _productsPage = new ProductsPage();
            _scenarioContext["ProductsPage"] = _productsPage;
        }

        [Then(@"I should see product items")]
        public void ThenIShouldSeeProductItems()
        {
            var productCount = _productsPage.GetProductCount();
            Assert.Greater(productCount, 0, "No products displayed on the products page");
        }

        [When(@"I sort products by ""(.*)""")]
        public void WhenISortProductsBy(string sortOption)
        {
            _productsPage.SortProductsBy(sortOption);
        }

        [Then(@"products should be sorted by price in ascending order")]
        public void ThenProductsShouldBeSortedByPriceInAscendingOrder()
        {
            var prices = _productsPage.GetProductPrices();
            var sortedPrices = prices.OrderBy(p => p).ToList();
            Assert.AreEqual(sortedPrices, prices, "Products are not sorted by price in ascending order");
        }

        [Then(@"products should be sorted by price in descending order")]
        public void ThenProductsShouldBeSortedByPriceInDescendingOrder()
        {
            var prices = _productsPage.GetProductPrices();
            var sortedPrices = prices.OrderByDescending(p => p).ToList();
            Assert.AreEqual(sortedPrices, prices, "Products are not sorted by price in descending order");
        }

        [When(@"I filter products by price range from (\d+) to (\d+)")]
        public void WhenIFilterProductsByPriceRange(int minPrice, int maxPrice)
        {
            _productsPage.ApplyPriceFilter(minPrice, maxPrice);
        }

        [Then(@"all displayed product prices should be between (\d+) and (\d+)")]
        public void ThenAllDisplayedProductPricesShouldBeBetween(int minPrice, int maxPrice)
        {
            var prices = _productsPage.GetProductPrices();
            Assert.IsTrue(prices.All(p => p >= minPrice && p <= maxPrice),
                $"Not all product prices are between {minPrice} and {maxPrice}");
        }

        [When(@"I apply filter ""(.*)"" with value ""(.*)""")]
        public void WhenIApplyFilterWithValue(string filterType, string filterValue)
        {
            var success = _productsPage.ApplyFilter(filterType, filterValue);
            Assert.IsTrue(success, $"Could not apply filter {filterType} with value {filterValue}");
        }

        [When(@"I click on product ""(.*)""")]
        public void WhenIClickOnProduct(string productName)
        {
            _productsPage.ClickOnProduct(productName);
        }

        [When(@"I go to page (\d+)")]
        public void WhenIGoToPage(int pageNumber)
        {
            _productsPage.GoToPage(pageNumber);
        }
    }
}
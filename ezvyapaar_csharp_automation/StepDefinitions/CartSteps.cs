using ezvyapaar_csharp_automation.core.PageObjects;
using NUnit.Framework;
using TechTalk.SpecFlow;
using ezvyapaar_csharp_automation.core.Utilities;

namespace EzVyapaar.Automation.StepDefinitions
{
    [Binding]
    public class CartSteps
    {
        private readonly CartPage _cartPage;
        private readonly ProductsPage _productsPage;
        private readonly ScenarioContext _scenarioContext;
        private readonly Logger _logger;

        public CartSteps(CartPage cartPage, ProductsPage productsPage, ScenarioContext scenarioContext)
        {
            _cartPage = cartPage;
            _productsPage = productsPage;
            _scenarioContext = scenarioContext;
            _logger = new Logger();
        }

        [When(@"I add item ""(.*)"" to cart")]
        public void WhenIAddItemToCart(string productName)
        {
            _logger.Info($"Adding product '{productName}' to cart");
            _productsPage.AddProductToCart(productName);

            // Store added product for later verification
            if (!_scenarioContext.ContainsKey("AddedProducts"))
            {
                _scenarioContext["AddedProducts"] = new List<string>();
            }

            var addedProducts = (List<string>)_scenarioContext["AddedProducts"];
            addedProducts.Add(productName);
        }

        [When(@"I navigate to cart page")]
        public void WhenINavigateToCartPage()
        {
            _logger.Info("Navigating to cart page");
            _cartPage.NavigateToCart();
        }

        [Then(@"my cart should contain (.*) items?")]
        public void ThenMyCartShouldContainItems(int expectedItemCount)
        {
            _logger.Info($"Verifying cart contains {expectedItemCount} items");
            int actualItemCount = _cartPage.GetCartItemCount();
            Assert.AreEqual(expectedItemCount, actualItemCount, $"Expected {expectedItemCount} items in cart but found {actualItemCount}");
        }

        [Then(@"my cart should contain item ""(.*)""")]
        public void ThenMyCartShouldContainItem(string productName)
        {
            _logger.Info($"Verifying cart contains product '{productName}'");
            bool isProductInCart = _cartPage.IsProductInCart(productName);
            Assert.IsTrue(isProductInCart, $"Product '{productName}' not found in cart");
        }

        [Then(@"the total price should be calculated correctly")]
        public void ThenTheTotalPriceShouldBeCalculatedCorrectly()
        {
            _logger.Info("Verifying cart total calculation");
            decimal expectedTotal = _cartPage.CalculateExpectedTotal();
            decimal actualTotal = _cartPage.GetCartTotal();

            Assert.AreEqual(expectedTotal, actualTotal, 0.01m, "Cart total doesn't match expected calculation");
        }

        [When(@"I remove item ""(.*)"" from cart")]
        public void WhenIRemoveItemFromCart(string productName)
        {
            _logger.Info($"Removing product '{productName}' from cart");
            _cartPage.RemoveProductFromCart(productName);

            // Update stored products list
            if (_scenarioContext.ContainsKey("AddedProducts"))
            {
                var addedProducts = (List<string>)_scenarioContext["AddedProducts"];
                addedProducts.Remove(productName);
            }
        }

        [When(@"I update quantity of ""(.*)"" to (.*)")]
        public void WhenIUpdateQuantityOfTo(string productName, int quantity)
        {
            _logger.Info($"Updating quantity of '{productName}' to {quantity}");
            _cartPage.UpdateProductQuantity(productName, quantity);
        }

        [When(@"I apply coupon code ""(.*)""")]
        public void WhenIApplyCouponCode(string couponCode)
        {
            _logger.Info($"Applying coupon code '{couponCode}'");
            _cartPage.ApplyCoupon(couponCode);
        }

        [Then(@"discount of (.*)% should be applied")]
        public void ThenDiscountShouldBeApplied(decimal discountPercentage)
        {
            _logger.Info($"Verifying discount of {discountPercentage}% is applied");
            decimal actualDiscount = _cartPage.GetAppliedDiscountPercentage();
            Assert.AreEqual(discountPercentage, actualDiscount, 0.01m, $"Expected discount of {discountPercentage}% but got {actualDiscount}%");
        }

        [When(@"I proceed to checkout")]
        public void WhenIProceedToCheckout()
        {
            _logger.Info("Proceeding to checkout");
            _cartPage.ProceedToCheckout();
        }
    }
}
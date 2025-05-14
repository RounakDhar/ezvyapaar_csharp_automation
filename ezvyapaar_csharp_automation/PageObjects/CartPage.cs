// 6. Cart Page
namespace ezvyapaar_csharp_automation.core.PageObjects
{
    using ezvyapaar_csharp_automation.core.Utilities;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CartPage : BasePage
    {
        // Locators
        private readonly By _cartItems = By.CssSelector(".cart-items");
        private readonly By _cartItemsList = By.CssSelector(".cart-item");
        private readonly By _checkoutButton = By.Id("checkout-button");
        private readonly By _totalAmount = By.CssSelector(".total-amount");
        private readonly By _emptyCartMessage = By.CssSelector(".empty-cart-message");
        private readonly By _continueShopping = By.LinkText("Continue Shopping");

        public CartPage() : base()
        {
            if (!IsPageLoaded(_cartItems) && !IsElementDisplayed(_emptyCartMessage))
            {
                throw new Exception("Cart page is not loaded properly");
            }
        }

        public int GetCartItemCount()
        {
            if (IsElementDisplayed(_emptyCartMessage))
            {
                return 0;
            }

            var items = Driver.FindElements(_cartItemsList);
            return items.Count;
        }

        public double GetTotalAmount()
        {
            if (IsElementDisplayed(_emptyCartMessage))
            {
                return 0;
            }

            var totalText = GetText(_totalAmount).Trim().Replace("₹", "").Replace(",", "");
            if (double.TryParse(totalText, out double total))
            {
                return total;
            }
            return 0;
        }

        public void ClickCheckoutButton()
        {
            if (!IsElementDisplayed(_emptyCartMessage))
            {
                Click(_checkoutButton);
            }
        }

        public void RemoveItemFromCart(string productName)
        {
            By removeButtonLocator = By.XPath($"//div[contains(@class, 'cart-item')]//h3[contains(text(), '{productName}')]/ancestor::div[contains(@class, 'cart-item')]//button[contains(@class, 'remove-button')]");

            if (IsElementDisplayed(removeButtonLocator))
            {
                Click(removeButtonLocator);
                WaitHelper.WaitForPageToLoad();
            }
        }

        public void UpdateQuantity(string productName, int quantity)
        {
            By quantityInputLocator = By.XPath($"//div[contains(@class, 'cart-item')]//h3[contains(text(), '{productName}')]/ancestor::div[contains(@class, 'cart-item')]//input[contains(@class, 'quantity-input')]");

            if (IsElementDisplayed(quantityInputLocator))
            {
                var element = WaitHelper.WaitForElement(quantityInputLocator);
                element.Clear();
                element.SendKeys(quantity.ToString());
                element.SendKeys(Keys.Tab);  // Trigger blur/change event
                WaitHelper.WaitForPageToLoad();
            }
        }

        public List<string> GetCartProductNames()
        {
            if (IsElementDisplayed(_emptyCartMessage))
            {
                return new List<string>();
            }

            var products = Driver.FindElements(By.CssSelector(".cart-item h3"));
            return products.Select(p => p.Text).ToList();
        }

        public bool IsCartEmpty()
        {
            return IsElementDisplayed(_emptyCartMessage);
        }

        public void ClickContinueShopping()
        {
            Click(_continueShopping);
        }
    }
}

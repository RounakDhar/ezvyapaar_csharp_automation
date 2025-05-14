// 5. Product Detail Page
namespace ezvyapaar_csharp_automation.core.PageObjects
{
    using ezvyapaar_csharp_automation.core.Utilities;
    using OpenQA.Selenium;
    using System;

    public class ProductDetailPage : BasePage
    {
        // Locators
        private readonly By _productTitle = By.CssSelector(".product-title");
        private readonly By _productPrice = By.CssSelector(".product-price");
        private readonly By _productDescription = By.CssSelector(".product-description");
        private readonly By _productImages = By.CssSelector(".product-images");
        private readonly By _quantityInput = By.Id("quantity");
        private readonly By _addToCartButton = By.CssSelector(".add-to-cart-btn");
        private readonly By _buyNowButton = By.CssSelector(".buy-now-btn");
        private readonly By _reviewsSection = By.Id("reviews-section");
        private readonly By _similarProductsSection = By.CssSelector(".similar-products");

        public ProductDetailPage() : base()
        {
            if (!IsPageLoaded(_productTitle))
            {
                throw new Exception("Product detail page is not loaded properly");
            }
        }

        public string GetProductTitle()
        {
            return GetText(_productTitle);
        }

        public double GetProductPrice()
        {
            var priceText = GetText(_productPrice).Trim().Replace("₹", "").Replace(",", "");
            if (double.TryParse(priceText, out double price))
            {
                return price;
            }
            return 0;
        }

        public string GetProductDescription()
        {
            return GetText(_productDescription);
        }

        public void SetQuantity(int quantity)
        {
            var element = WaitHelper.WaitForElement(_quantityInput);
            element.Clear();
            element.SendKeys(quantity.ToString());
        }

        public void ClickAddToCartButton()
        {
            Click(_addToCartButton);
        }

        public void ClickBuyNowButton()
        {
            Click(_buyNowButton);
        }

        public void AddToCart(int quantity = 1)
        {
            SetQuantity(quantity);
            ClickAddToCartButton();
        }

        public bool IsReviewsSectionDisplayed()
        {
            return IsElementDisplayed(_reviewsSection);
        }

        public bool IsSimilarProductsSectionDisplayed()
        {
            return IsElementDisplayed(_similarProductsSection);
        }
    }
}
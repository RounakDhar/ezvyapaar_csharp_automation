// 4. Products Page
namespace ezvyapaar_csharp_automation.core.PageObjects
{
    using ezvyapaar_csharp_automation.core.Utilities;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductsPage : BasePage
    {
        // Locators
        private readonly By _productsList = By.CssSelector(".product-list");
        private readonly By _productItems = By.CssSelector(".product-item");
        private readonly By _sortDropdown = By.Id("sort-dropdown");
        private readonly By _filterSection = By.CssSelector(".filter-section");
        private readonly By _priceSlider = By.CssSelector(".price-slider");
        private readonly By _paginationControls = By.CssSelector(".pagination");
        private readonly By _resultsCount = By.CssSelector(".results-count");

        public ProductsPage() : base()
        {
            if (!IsPageLoaded(_productsList))
            {
                throw new Exception("Products page is not loaded properly");
            }
        }

        public int GetProductCount()
        {
            var products = Driver.FindElements(_productItems);
            return products.Count;
        }

        public void SortProductsBy(string sortOption)
        {
            SelectDropdownByText(_sortDropdown, sortOption);
            WaitHelper.WaitForPageToLoad();
        }

        public void ApplyPriceFilter(int minPrice, int maxPrice)
        {
            // This is a placeholder implementation.
            // The actual implementation would depend on how the price filter works on the website.
            // It might be a slider, range inputs, or checkboxes.

            // Example using JavaScript to set a slider value
            if (IsElementDisplayed(_priceSlider))
            {
                var slider = Driver.FindElement(_priceSlider);
                ((IJavaScriptExecutor)Driver).ExecuteScript(
                    $"arguments[0].setAttribute('min', {minPrice}); arguments[0].setAttribute('max', {maxPrice});",
                    slider);

                // Trigger change event
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].dispatchEvent(new Event('change'));", slider);

                WaitHelper.WaitForPageToLoad();
            }
        }

        public void GoToPage(int pageNumber)
        {
            By pageLink = By.XPath($"//div[@class='pagination']//a[text()='{pageNumber}']");

            if (IsElementDisplayed(pageLink))
            {
                Click(pageLink);
                WaitHelper.WaitForPageToLoad();
            }
        }

        public bool ApplyFilter(string filterType, string filterValue)
        {
            By filterLocator = By.XPath($"//div[@class='filter-section']//label[contains(text(), '{filterValue}')]/preceding-sibling::input");

            if (IsElementDisplayed(filterLocator))
            {
                Click(filterLocator);
                WaitHelper.WaitForPageToLoad();
                return true;
            }

            return false;
        }

        public void ClickOnProduct(string productName)
        {
            By productLocator = By.XPath($"//div[@class='product-item']//h3[contains(text(), '{productName}')]/ancestor::div[@class='product-item']");
            ScrollToElement(productLocator);
            Click(productLocator);
        }

        public List<string> GetProductNames()
        {
            var products = Driver.FindElements(By.CssSelector(".product-item h3"));
            return products.Select(p => p.Text).ToList();
        }

        public List<double> GetProductPrices()
        {
            var priceElements = Driver.FindElements(By.CssSelector(".product-item .price"));
            var prices = new List<double>();

            foreach (var element in priceElements)
            {
                var priceText = element.Text.Trim().Replace("₹", "").Replace(",", "");
                if (double.TryParse(priceText, out double price))
                {
                    prices.Add(price);
                }
            }

            return prices;
        }
    }
}
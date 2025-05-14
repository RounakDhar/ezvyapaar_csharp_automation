Feature: Products Page Functionality
  As a customer
  I want to browse and filter products
  So that I can find and purchase items I need

Background:
  Given I am logged in as a registered user
  And I am on the products page

Scenario: View all products
  Then I should see a list of available products
  And each product should display name, price and image
  And each product should have an "Add to Cart" button

Scenario: Filter products by category
  When I select category "Electronics" from the filter options
  Then I should only see products in the "Electronics" category
  When I select category "Audio" from the filter options
  Then I should only see products in the "Audio" category

Scenario: Filter products by price range
  When I set minimum price filter to "5000"
  And I set maximum price filter to "20000"
  And I apply the price filter
  Then I should only see products priced between "5000" and "20000"

Scenario: Sort products by price - low to high
  When I select sort option "Price: Low to High"
  Then products should be displayed in ascending order of price

Scenario: Sort products by price - high to low
  When I select sort option "Price: High to Low"
  Then products should be displayed in descending order of price

Scenario: Sort products by rating
  When I select sort option "Rating"
  Then products should be displayed in descending order of rating

Scenario: Search for a specific product
  When I enter "Bluetooth" in the product search box
  And I click the search button
  Then I should see products containing "Bluetooth" in their name or description
  And I should see "Bluetooth Headphones" in the search results

Scenario: View product details
  When I click on product "Laptop Pro 15"
  Then I should be redirected to the product details page
  And I should see detailed information about "Laptop Pro 15"
  And I should see product specifications
  And I should see product reviews if available

Scenario: Add product to wishlist
  When I click the wishlist button for product "Smartphone X1"
  Then "Smartphone X1" should be added to my wishlist
  And I should see a confirmation message

Scenario: View products by pagination
  Given There are more than 10 products available
  Then I should see maximum 10 products per page
  And I should see pagination controls
  When I click on page "2"
  Then I should see the next set of products

Scenario: Check product availability
  When I view product "4K Smart TV 55\""
  Then I should see an "Out of Stock" indicator
  And the "Add to Cart" button should be disabled

Scenario: Filter products by availability
  When I select the "In Stock Only" filter option
  Then I should only see products that are in stock
  And I should not see "4K Smart TV 55\"" in the results
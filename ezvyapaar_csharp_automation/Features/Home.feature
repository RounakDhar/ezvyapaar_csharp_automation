Feature: Home Page Functionality
  As a user
  I want to interact with the home page
  So that I can access different features of the application

Background:
  Given I am on the home page

Scenario: Navigate to different sections from home page
  When I click on the "Products" link
  Then I should be redirected to the products page
  When I navigate back to home page
  And I click on the "Offers" link
  Then I should be redirected to the offers page
  When I navigate back to home page
  And I click on the "Contact Us" link
  Then I should be redirected to the contact us page

Scenario: Search for products from home page
  When I enter "smartphone" in the search box
  And I click the search button
  Then I should see search results for "smartphone"
  And the search results should contain "Smartphone X1"

Scenario: View featured products on home page
  Then I should see at least 3 featured products
  And each featured product should have an image
  And each featured product should have a name
  And each featured product should have a price

Scenario: Navigate to cart from home page
  When I click on the cart icon
  Then I should be redirected to the cart page

Scenario: User account options for logged-in user
  Given I am logged in as a registered user
  When I click on the user account menu
  Then I should see options for "My Profile"
  And I should see options for "My Orders"
  And I should see options for "Logout"

Scenario: Check promotional banner
  Then I should see a promotional banner
  When I click on the promotional banner
  Then I should be redirected to the promotion details page

Scenario: Newsletter subscription
  When I enter my email in the newsletter subscription field
  And I click the subscribe button
  Then I should see a subscription confirmation message
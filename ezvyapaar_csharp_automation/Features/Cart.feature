Feature: Shopping Cart
  As a customer
  I want to manage items in my shopping cart
  So that I can purchase the products I need

Background:
  Given I am logged in as a registered user
  And I am on the products page

Scenario: Add single item to cart
  When I add item "Smartphone X1" to cart
  And I navigate to cart page
  Then my cart should contain 1 item
  And my cart should contain item "Smartphone X1"
  And the total price should be calculated correctly

Scenario: Add multiple items to cart
  When I add item "Smartphone X1" to cart
  And I add item "Bluetooth Headphones" to cart
  And I add item "Power Bank 10000mAh" to cart
  And I navigate to cart page
  Then my cart should contain 3 items
  And my cart should contain item "Smartphone X1"
  And my cart should contain item "Bluetooth Headphones"
  And my cart should contain item "Power Bank 10000mAh"
  And the total price should be calculated correctly

Scenario: Remove item from cart
  When I add item "Smartphone X1" to cart
  And I add item "Bluetooth Headphones" to cart
  And I navigate to cart page
  And I remove item "Smartphone X1" from cart
  Then my cart should contain 1 item
  And my cart should contain item "Bluetooth Headphones"
  And the total price should be calculated correctly

Scenario: Update item quantity
  When I add item "Smartphone X1" to cart
  And I navigate to cart page
  And I update quantity of "Smartphone X1" to 3
  Then my cart should contain 1 item
  And the total price should be calculated correctly

Scenario: Apply coupon code
  When I add item "Smartphone X1" to cart
  And I navigate to cart page
  And I apply coupon code "SAVE20"
  Then discount of 20% should be applied
  And the total price should be calculated correctly

Scenario: Proceed to checkout
  When I add item "Smartphone X1" to cart
  And I navigate to cart page
  And I proceed to checkout
  Then I should be on the checkout page
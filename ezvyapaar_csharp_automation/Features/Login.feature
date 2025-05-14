Feature: User Login
  As a registered user
  I want to log in to the application
  So that I can access my account and make purchases

Background:
  Given I am on the login page

Scenario: Successful login with valid credentials
  When I enter valid username and password
  And I click on the login button
  Then I should be logged in successfully
  And I should be redirected to the home page
  And I should see my user name displayed

Scenario: Failed login with invalid credentials
  When I enter invalid username and password
  And I click on the login button
  Then I should see an error message "Invalid username or password"
  And I should remain on the login page

Scenario: Failed login with empty credentials
  When I leave username and password empty
  And I click on the login button
  Then I should see validation messages for required fields
  And I should remain on the login page

Scenario: Failed login with invalid username format
  When I enter username in invalid format
  And I enter valid password
  And I click on the login button
  Then I should see a validation message for invalid username format
  And I should remain on the login page

Scenario: Remember me functionality
  When I enter valid username and password
  And I check the "Remember me" option
  And I click on the login button
  And I close the browser and reopen it
  And I navigate to the application
  Then I should be already logged in

Scenario: Forgot password functionality
  When I click on the "Forgot Password" link
  Then I should be redirected to the password recovery page
  When I enter my registered email
  And I click on the reset password button
  Then I should see a confirmation message for password reset email

Scenario: Navigation to registration page
  When I click on the "Register" link
  Then I should be redirected to the registration page

Scenario Outline: Login with different user roles
  When I enter username "<username>" and password "<password>"
  And I click on the login button
  Then I should be logged in successfully
  And I should have access to features for role "<role>"

  Examples:
    | username               | password  | role       |
    | customer@ezvyapaar.com | Test@123  | Customer   |
    | admin@ezvyapaar.com    | Admin@123 | Admin      |
    | vendor@ezvyapaar.com   | Vendor@123| Vendor     |
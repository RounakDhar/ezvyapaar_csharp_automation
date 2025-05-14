
---

## 🛠 Tech Stack

| Tech | Version | Purpose |
|------|---------|---------|
| .NET | 6.0     | Core language runtime |
| Selenium | Latest | UI interaction |
| SpecFlow | Latest | BDD syntax |
| NUnit | Latest | Test runner |
| GitHub Actions / Jenkins | - | CI/CD pipelines |
| ExtentReports | Planned | Reporting |
| ExcelDataReader / CsvHelper | Optional | Test data management |

---

## 🚀 Getting Started

### ✅ Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- [.NET SDK 6.0+](https://dotnet.microsoft.com/en-us/download)
- Chrome, Edge, Firefox (browsers for testing)
- Git

---

### 🔧 Setup & Run

```bash
git clone https://github.com/your-org/EzVyapaarAutomation.git
cd EzVyapaarAutomation

# Restore dependencies
dotnet restore

# Build
dotnet build

# Run tests
dotnet test


# Ezvyapaar Test Automation Framework

This framework is designed for automating tests for the Ezvyapaar e-commerce website ([https://www.ezvyapaar.com/](https://www.ezvyapaar.com/)). It is built using C#, Selenium WebDriver, NUnit, and SpecFlow.

## Project Structure

The project is organized as follows:

Project Root│├───Ezvyapaar.Tests (Test Project)│   ││   ├───Features             (SpecFlow feature files)│   │   │   Checkout.feature│   │   │   Login.feature│   │   │   ...│   ││   ├───Steps                (SpecFlow step definitions)│   │   │   CheckoutSteps.cs│   │   │   LoginSteps.cs│   │   │   ...│   ││   ├───Pages                (Page Object Model)│   │   │   BasePage.cs│   │   │   HomePage.cs│   │   │   LoginPage.cs│   │   │   ProductDetailPage.cs│   │   │   CartPage.cs│   │   │   CheckoutPage.cs│   │   │   ...│   ││   ├───Utils                (Helper classes)│   │   │   BrowserManager.cs│   │   │   TestDataHelper.cs│   │   │   ...│   ││   ├───Data                 (Test data files)│   │   │   checkout_data.csv│   │   │   ...│   ││   ├───Config               (Configuration files)│   │   │   appsettings.json│   ││   ├───Hooks.cs             (SpecFlow hooks for setup/teardown)│   ││   └───Ezvyapaar.Tests.csproj (Project file)│└───Ezvyapaar.sln         (Solution file)
## Core Technologies

* **Selenium WebDriver:** For browser automation.
* **NUnit:** For test execution and assertions.
* **SpecFlow:** For Behavior-Driven Development (BDD).
* **ExtentReports:** For generating detailed test reports.
* **C#:** Programming language.
* **Microsoft.Extensions.Configuration:** For managing configuration settings.
* **System.Text.Json:** For JSON data handling.
* **CsvHelper:** For CSV data handling.
* **Bogus:** For generating fake data.
* **Polly**: For adding retry logic.

## Setup Instructions

1.  **Prerequisites:**

    * Visual Studio 2022 installed.
    * .NET SDK installed.
    * Ensure you have the correct browser drivers installed (e.g., ChromeDriver, GeckoDriver, EdgeDriver).  The `BrowserManager` class in the framework attempts to manage this, but manual setup might sometimes be necessary.

2.  **Clone the Repository:**

    * Clone the project repository to your local machine.

3.  **Open the Solution:**

    * Open the `Ezvyapaar.sln` file in Visual Studio 2022.

4.  **Install NuGet Packages:**

    * Visual Studio should automatically restore the NuGet packages defined in the project.  If not, or if you encounter issues:
        * Go to "Tools" > "NuGet Package Manager" > "Manage NuGet Packages for Solution."
        * Ensure that the following packages are installed:
            * `Selenium.WebDriver`
            * `Selenium.WebDriver.ChromeDriver` (or the driver for your target browser)
            * `Selenium.WebDriver.GeckoDriver` (for Firefox)
            * `Selenium.WebDriver.EdgeDriver` (for Edge)
            * `NUnit`
            * `NUnit3TestAdapter`
            * `SpecFlow`
            * `SpecFlow.Tools`
            * `SpecFlow.NUnit`
            * `ExtentReports`
             * `Polly`
            * `Microsoft.Extensions.Configuration`
            * `Microsoft.Extensions.Configuration.Json`
            * `System.Text.Json`
            * `CsvHelper`
            * `Bogus`

5.  **Configure `appsettings.json`:**

    * The `appsettings.json` file contains configuration settings such as the base URL of the website, the browser to use, and test data.
    * Modify the `appsettings.json` file in the `Config` folder to match your environment:

    ```json
    {
      "BaseUrl": "[https://www.ezvyapaar.com](https://www.ezvyapaar.com)",
      "Browser": "chrome", // or "firefox", "edge"
      "Chrome": {
        "Headless": "false" // or "true"
      },
       "Firefox": {
        "Headless": "false"
      },
      "Edge": {
        "Headless": "false"
      },
      "ValidUserEmail": "valid@example.com", // Replace with a valid email for testing
      "ValidUserPassword": "password123" // Replace with a valid password for testing
    }
    ```

6.  **Set up Test Data (Optional):**

    *If your tests require data from external files, populate the files in the `Data` folder.  For example, the `checkout_data.csv` file should contain data for checkout tests.

7.  **Build the Solution:**

    * In Visual Studio, go to "Build" > "Build Solution" or press "Ctrl+Shift+B."

## Running the Tests

1.  **Open Test Explorer:**

    * In Visual Studio, go to "Test" > "Windows" > "Test Explorer."

2.  **Run Tests:**

    * In the Test Explorer, you can:
        * Click "Run All" to run all tests.
        * Select specific tests or test categories and click "Run Selected Tests."

3.  **View Results:**

    * The Test Explorer will display the test results (Pass/Fail/Skipped).
    * Detailed test reports are generated using ExtentReports.  The reports are saved in the project directory as `TestResults.html` after each test run.  Open this file in your browser to view the results.

## Test Structure

The tests are structured using NUnit and SpecFlow:

* **NUnit:**
    * Test classes are marked with `[TestFixture]`.
    * Test methods are marked with `[Test]`.
    * Assertions are performed using `Assert` methods.
    * Test categories are used to group tests (e.g., `[Category("Smoke")]`, `[Category("Regression")]`).
* **SpecFlow:**
    * Features are defined in `.feature` files (e.g., `Checkout.feature`, `Login.feature`).
    * Step definitions are implemented in C# classes (e.g., `CheckoutSteps.cs`, `LoginSteps.cs`).
    * Scenarios and Scenario Outlines are used to define test cases.

## Page Object Model (POM)

The framework uses the Page Object Model (POM) to represent web pages and their elements.  This improves code reusability and maintainability.

* Each page of the web application has a corresponding Page Object class (e.g., `HomePage.cs`, `LoginPage.cs`).
* Page Objects contain:
    * Locators for the elements on the page (e.g., buttons, input fields).
    * Methods for interacting with those elements.

## Test Data Management

* The framework supports data-driven testing using external data sources.
* Test data can be stored in CSV files (using `CsvHelper`) or JSON files (`System.Text.Json`).
* The `TestDataHelper` class provides methods for reading data from these files.
* The `Bogus` library is used to generate realistic, dynamic test data.

## Configuration

* The `appsettings.json` file is used to store configuration settings such as:
    * Base URL
    * Browser type
    * Browser options (e.g., headless mode)
    * Test data file paths
    * Environment specific settings

## Reporting

* ExtentReports is used to generate detailed and visually appealing HTML reports.
* Reports include:
    * Test case details
    * Execution status (Pass/Fail/Skipped)
    * Screenshots on failure
    * Test execution time
    * Detailed logging

## Logging

* The framework uses NLog for logging.
* Log messages are written to the console and to log files.
* Logging levels: Debug, Info, Warning, Error, Fatal.

## Retry Mechanism

* The framework uses Polly to implement a retry mechanism for flaky tests.
* Tests that fail due to transient issues (e.g., network connectivity) are automatically retried.

## Parallel Execution

* NUnit supports parallel test execution to reduce the overall test execution time.
* The framework is designed to be compatible with parallel execution.

## Contributing

1.  Fork the repository.
2.  Create a new branch for your feature or bug fix.
3.  Write tests for your changes.
4.  Implement your changes.
5.  Ensure all tests pass.
6.  Submit a pull request.

## Code Example

### Example Test (HomePageTests.cs)

```csharp
using NUnit.Framework;
using OpenQA.Selenium;

namespace Ezvyapaar.Tests
{
    [TestFixture]
    [Category("HomePage")]
    public class HomePageTests : BaseTest
    {
        [Test]
        [Category("Smoke")]
        public void HomePage_Title_Should_Be_Correct()
        {
            // Arrange
            string expectedTitle = "Welcome to Ezvyapaar";
            var homePage = new HomePage(Driver);

            // Act
            homePage.NavigateTo(Configuration["BaseUrl"]);
            string actualTitle = homePage.GetHomePageTitle();

            // Assert
            Assert.AreEqual(expectedTitle, actualTitle, "Home page title is incorrect.");
        }
    }
}
Example Page Object (HomePage.cs)using OpenQA.Selenium;

namespace Ezvyapaar.Tests
{
    public class HomePage : BasePage
    {
        private readonly By _loginButton = By.Id("login-button");
        private readonly By _searchBox = By.Id("search-box");

        public HomePage(IWebDriver driver) : base(driver) { }

        public void ClickLoginButton()
        {
            ClickElement(_loginButton);
        }

        public void EnterSearchTerm(string searchTerm)
        {
            SendKeys(_searchBox, searchTerm);
        }
    }
}

namespace ezvyapaar_csharp_automation.core.Browser
{
    /// <summary>
    /// Enum representing supported browser types in the automation framework.
    /// Used by BrowserFactory to determine which WebDriver to instantiate.
    /// </summary>
    public enum BrowserType
    {
        /// <summary>
        /// Google Chrome browser
        /// </summary>
        Chrome,

        /// <summary>
        /// Mozilla Firefox browser
        /// </summary>
        Firefox,

        /// <summary>
        /// Microsoft Edge browser
        /// </summary>
        Edge,

        /// <summary>
        /// Google Chrome in headless mode (no UI)
        /// </summary>
        ChromeHeadless,

        /// <summary>
        /// Mozilla Firefox in headless mode (no UI)
        /// </summary>
        FirefoxHeadless,

        /// <summary>
        /// Microsoft Edge in headless mode (no UI)
        /// </summary>
        EdgeHeadless,

        /// <summary>
        /// Safari browser (for macOS testing)
        /// </summary>
        Safari,

        /// <summary>
        /// Internet Explorer browser (legacy support)
        /// </summary>
        InternetExplorer,

        /// <summary>
        /// Chrome browser for Android mobile testing
        /// </summary>
        AndroidChrome,

        /// <summary>
        /// Safari browser for iOS mobile testing
        /// </summary>
        IOSSafari
    }
}
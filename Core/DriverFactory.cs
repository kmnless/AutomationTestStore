using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using AutomationTestStore.Core;

namespace AutomationTestStore.Core;

public enum BrowserType { Chrome, Firefox }

// Factory Method — creates the right WebDriver based on BrowserType
public static class DriverFactory
{
    public static IWebDriver Create(BrowserType browser)
    {
        AppLogger.Log.Information("Creating {Browser} driver", browser);

        if (browser == BrowserType.Chrome)
        {
            var opts = new ChromeOptions();
            opts.AddArgument("--start-maximized");
            opts.AddArgument("--disable-notifications");
            return new ChromeDriver(opts);
        }
        else
        {
            var opts   = new FirefoxOptions();
            var driver = new FirefoxDriver(opts);
            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}

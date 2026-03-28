using OpenQA.Selenium;
using AutomationTestStore.Core;
using Xunit;

namespace AutomationTestStore.Tests;

// Creates and disposes a WebDriver instance for every test
public abstract class BaseTest(BrowserType browser) : IDisposable
{
    protected readonly IWebDriver Driver = DriverFactory.Create(browser);

    public void Dispose()
    {
        AppLogger.Log.Information("Quitting driver");
        Driver.Quit();
    }
}

// xUnit parallel collections — Chrome and Firefox run simultaneously
[CollectionDefinition("Chrome")]  public class ChromeCol  { }
[CollectionDefinition("Firefox")] public class FirefoxCol { }

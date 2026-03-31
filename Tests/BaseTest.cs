using System;
using OpenQA.Selenium;
using AutomationTestStore.Core;
using Xunit;

namespace AutomationTestStore.Tests;

// Creates and disposes a WebDriver instance for every test
public abstract class BaseTest : IDisposable
{
    protected readonly IWebDriver Driver;
    protected readonly BrowserType Browser;

    private bool _disposed = false;

    protected BaseTest(BrowserType browser)
    {
        Browser = browser;
        Driver = DriverFactory.Create(browser);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                AppLogger.Log.Information("Quitting driver");
                Driver?.Quit();
                Driver?.Dispose();
            }

            _disposed = true;
        }
    }
}

// xUnit parallel collections — Chrome and Firefox run simultaneously
[CollectionDefinition("Chrome")] public class ChromeCol { }
[CollectionDefinition("Firefox")] public class FirefoxCol { }
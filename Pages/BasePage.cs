using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationTestStore.Pages;

public abstract class BasePage
{
    protected readonly IWebDriver Driver;
    protected readonly WebDriverWait Wait;

    protected BasePage(IWebDriver driver)
    {
        Driver = driver;
        Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
        {
            PollingInterval = TimeSpan.FromMilliseconds(500)
        };

        // so isnt needed to catch these exceptions everywhere
        Wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));
    }

    protected IWebElement WaitAndFindElement(By locator)
    {
        return Wait.Until(d => d.FindElement(locator));
    }
}
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
    }

    protected IWebElement GetVisibleElement(By locator, string elementName)
    {
        try
        {
            return Wait.Until(driver =>
            {
                var elements = driver.FindElements(locator);

                if (elements.Count == 0) return null;

                var element = elements[0];
                return element.Displayed ? element : null;
            });
        }
        catch (WebDriverTimeoutException)
        {
            throw new NotFoundException($"Element '{elementName}' ({locator}) is not found.");
        }
    }
}
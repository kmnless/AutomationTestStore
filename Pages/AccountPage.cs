using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AutomationTestStore.Core;

namespace AutomationTestStore.Pages;

public class AccountPage(IWebDriver driver)
{
    private readonly WebDriverWait _wait = new(driver, TimeSpan.FromSeconds(10));

    public bool IsDisplayed()
    {
        _wait.Until(d => d.Url.Contains("account"));
        var heading = driver.FindElement(By.CssSelector("#maincontainer h1, .heading1")).Text;
        AppLogger.Log.Information("Account page heading: '{H}'", heading);
        return heading.Contains("Account", StringComparison.OrdinalIgnoreCase);
    }

    public string GetWelcomeText()
    {
        try
        {
            return _wait.Until(d =>
            {
                try
                {
                    var elements = d.FindElements(By.CssSelector(".menu_text, #customer_menu_top li a"));
                    var el = elements.FirstOrDefault(e => e.Displayed && e.Text.Contains("Welcome", StringComparison.OrdinalIgnoreCase));

                    // null for continue the loop
                    return el != null ? el.Text.Trim() : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            }) ?? string.Empty;
        }
        catch (WebDriverTimeoutException)
        {
            AppLogger.Log.Warning("Welcome text didn't appear in time.");
            return string.Empty;
        }
    }
}

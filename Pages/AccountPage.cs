using OpenQA.Selenium;
using AutomationTestStore.Core;

namespace AutomationTestStore.Pages;

public class AccountPage(IWebDriver driver) : BasePage(driver)
{
    private readonly By _heading = By.CssSelector("#maincontainer h1, .heading1");
    private readonly By _welcomeText = By.CssSelector(".menu_text, #customer_menu_top li a");

    public bool IsDisplayed()
    {
        Wait.Until(d => d.Url.Contains("account"));
        var heading = WaitAndFindElement(_heading).Text;
        AppLogger.Log.Information("Account page heading: '{H}'", heading);
        return heading.Contains("Account", StringComparison.OrdinalIgnoreCase);
    }

    public string GetWelcomeText()
    {
        var el = Wait.Until(d =>
        {
            var elements = d.FindElements(_welcomeText);
            return elements.FirstOrDefault(e => e.Displayed && e.Text.Contains("Welcome", StringComparison.OrdinalIgnoreCase));
        });

        return el?.Text.Trim() ?? string.Empty;
    }
}
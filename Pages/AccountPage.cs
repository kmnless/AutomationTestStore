using OpenQA.Selenium;
using AutomationTestStore.Core;

namespace AutomationTestStore.Pages;

public class AccountPage(IWebDriver driver) : BasePage(driver)
{
    private readonly By _heading = By.CssSelector("#maincontainer h1, .heading1");
    private readonly By _welcomeText = By.CssSelector(".menu_text, #customer_menu_top li a");

    public bool IsDisplayed()
    {
        Wait.Until(d => d.Url.Contains("account", StringComparison.OrdinalIgnoreCase));

        var headingElement = GetVisibleElement(_heading, "Account page heading");

        AppLogger.Log.Information("Account page heading: '{H}'", headingElement.Text);
        return headingElement.Text.Contains("Account", StringComparison.OrdinalIgnoreCase);
    }

    public string GetWelcomeText()
    {
        var welcomeElement = GetVisibleElement(_welcomeText, "Welcome text element");

        return welcomeElement.Text.Trim();
    }
}
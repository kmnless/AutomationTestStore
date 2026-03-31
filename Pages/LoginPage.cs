using OpenQA.Selenium;
using AutomationTestStore.Core;

namespace AutomationTestStore.Pages;

public class LoginPage(IWebDriver driver) : BasePage(driver)
{
    private readonly string Url = $"{ConfigHelper.BaseUrl}/index.php?rt=account/login";

    private readonly By _continueBtn = By.CssSelector("#accountFrm button[title='Continue']");

    public void Open()
    {
        AppLogger.Log.Information("Opening Login/Register page");
        Driver.Navigate().GoToUrl(Url);
    }

    public void ClickContinueNewCustomer()
    {
        AppLogger.Log.Information("Clicking Continue (new customer)");
        var btn = GetVisibleElement(_continueBtn, "Continue button (new customer)");
        btn.Click();
    }
}
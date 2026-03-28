using OpenQA.Selenium;
using AutomationTestStore.Core;

namespace AutomationTestStore.Pages;

public class LoginPage(IWebDriver driver)
{
    const string Url = "https://automationteststore.com/index.php?rt=account/login";

    public void Open()
    {
        AppLogger.Log.Information("Opening Login/Register page");
        driver.Navigate().GoToUrl(Url);
    }

    public void ClickContinueNewCustomer()
    {
        AppLogger.Log.Information("Clicking Continue (new customer)");
        driver.FindElement(By.CssSelector("#accountFrm button[title='Continue']")).Click();
    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AutomationTestStore.Core;

namespace AutomationTestStore.Pages;

public class RegistrationPage(IWebDriver driver) : BasePage(driver)
{
    private readonly By _firstNameInput = By.CssSelector("#AccountFrm_firstname");
    private readonly By _lastNameInput = By.CssSelector("#AccountFrm_lastname");
    private readonly By _emailInput = By.CssSelector("#AccountFrm_email");
    private readonly By _phoneInput = By.CssSelector("#AccountFrm_telephone");
    private readonly By _addressInput = By.CssSelector("#AccountFrm_address_1");
    private readonly By _cityInput = By.CssSelector("#AccountFrm_city");
    private readonly By _zipInput = By.CssSelector("#AccountFrm_postcode");
    private readonly By _countrySelect = By.CssSelector("#AccountFrm_country_id");
    private readonly By _regionSelect = By.CssSelector("#AccountFrm_zone_id");
    private readonly By _loginInput = By.CssSelector("#AccountFrm_loginname");
    private readonly By _passwordInput = By.CssSelector("#AccountFrm_password");
    private readonly By _confirmInput = By.CssSelector("#AccountFrm_confirm");
    private readonly By _privacyCheckbox = By.CssSelector("#AccountFrm_agree");
    private readonly By _continueBtn = By.CssSelector("button[title='Continue']");
    private readonly By _helpBlockError = By.CssSelector(".help-block");

    public void FillForm(UserData u)
    {
        AppLogger.Log.Information("Filling registration form for '{Login}'", u.LoginName);
        Type(_firstNameInput, u.FirstName);
        Type(_lastNameInput, u.LastName);
        Type(_emailInput, u.Email);
        Type(_phoneInput, u.Telephone);
        Type(_addressInput, u.Address1);
        Type(_cityInput, u.City);
        Type(_zipInput, u.ZipCode);
        SelectOption(_countrySelect, u.Country);
        SelectOption(_regionSelect, u.Region);
        Type(_loginInput, u.LoginName);
        Type(_passwordInput, u.Password);
        Type(_confirmInput, u.Password);

        AppLogger.Log.Information("Checking Privacy Policy agreement");
        WaitAndFindElement(_privacyCheckbox).Click();
    }

    public void SetLoginName(string value)
    {
        AppLogger.Log.Debug("Setting login name: '{Value}'", value);
        Type(_loginInput, value);
    }

    public void Submit() => WaitAndFindElement(_continueBtn).Click();

    public string GetLoginNameError()
    {
        // no masking exceptions now (:
        var el = Wait.Until(d =>
        {
            var elements = d.FindElements(_helpBlockError);
            return elements.FirstOrDefault(e => e.Displayed && e.Text.Contains("alphanumeric"));
        });

        return el?.Text.Trim() ?? string.Empty;
    }

    private void Type(By locator, string text)
    {
        var el = WaitAndFindElement(locator);
        el.Clear();
        el.SendKeys(text);
    }

    private void SelectOption(By locator, string text)
    {
        Wait.Until(d =>
        {
            var el = d.FindElement(locator);
            var selectElement = new SelectElement(el);

            if (selectElement.Options.Any(o => o.Text == text))
            {
                selectElement.SelectByText(text);
                return true;
            }
            return false;
        });
    }
}
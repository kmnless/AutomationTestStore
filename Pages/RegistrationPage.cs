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

        Type(_firstNameInput, u.FirstName, "First Name input");
        Type(_lastNameInput, u.LastName, "Last Name input");
        Type(_emailInput, u.Email, "Email input");
        Type(_phoneInput, u.Telephone, "Telephone input");
        Type(_addressInput, u.Address1, "Address input");
        Type(_cityInput, u.City, "City input");
        SelectOption(_countrySelect, u.Country, "Country Select Option");
        SelectOption(_regionSelect, u.Region, "Region/State Select Option");
        Type(_zipInput, u.ZipCode, "ZIP Code input");
        Type(_loginInput, u.LoginName, "Login Name input");
        Type(_passwordInput, u.Password, "Password input");
        Type(_confirmInput, u.Password, "Password Confirm input");

        AppLogger.Log.Information("Checking Privacy Policy agreement");
        GetVisibleElement(_privacyCheckbox, "Privacy Policy checkbox").Click();
    }

    public void SetLoginName(string value)
    {
        AppLogger.Log.Debug("Setting login name: '{Value}'", value);
        Type(_loginInput, value, "Login Name input");
    }

    public void Submit() => GetVisibleElement(_continueBtn, "Continue button").Click();

    public string GetLoginNameError()
    {
        try
        {
            var el = Wait.Until(d =>
            {
                try
                {
                    var elements = d.FindElements(_helpBlockError);
                    return elements.FirstOrDefault(e => e.Displayed && e.Text.Contains("alphanumeric", StringComparison.OrdinalIgnoreCase));
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            return el.Text.Trim();
        }
        catch (WebDriverTimeoutException)
        {
            throw new NotFoundException("Validation error message(alphanumeric) is not found.");
        }
    }

    private void Type(By locator, string text, string elementName)
    {
        var el = GetVisibleElement(locator, elementName);
        el.Clear();
        el.SendKeys(text);
    }

    private void SelectOption(By locator, string text, string elementName)
    {
        try
        {
            Wait.Until(d =>
            {
                try
                {
                    var elements = d.FindElements(locator);
                    if (elements.Count == 0 || !elements[0].Displayed) return false;

                    var selectElement = new SelectElement(elements[0]);

                    if (selectElement.Options.Any(o => o.Text == text))
                    {
                        selectElement.SelectByText(text);
                        return true;
                    }
                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
        }
        catch (WebDriverTimeoutException)
        {
            throw new NotFoundException($"Cannot select '{text}' option in element '{elementName}'.");
        }
    }
}
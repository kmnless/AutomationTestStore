using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AutomationTestStore.Core;

namespace AutomationTestStore.Pages;

public class RegistrationPage(IWebDriver driver)
{
    private readonly WebDriverWait _wait = new(driver, TimeSpan.FromSeconds(10));

    public void FillForm(UserData u)
    {
        AppLogger.Log.Information("Filling registration form for '{Login}'", u.LoginName);
        Type("#AccountFrm_firstname",  u.FirstName);
        Type("#AccountFrm_lastname",   u.LastName);
        Type("#AccountFrm_email",      u.Email);
        Type("#AccountFrm_telephone",  u.Telephone);
        Type("#AccountFrm_address_1",  u.Address1);
        Type("#AccountFrm_city",       u.City);
        Type("#AccountFrm_postcode",   u.ZipCode);
        Select("#AccountFrm_country_id", u.Country);
        Select("#AccountFrm_zone_id",    u.Region);
        Type("#AccountFrm_loginname",  u.LoginName);
        Type("#AccountFrm_password",   u.Password);
        Type("#AccountFrm_confirm",    u.Password);
        AppLogger.Log.Information("Checking Privacy Policy agreement");
        driver.FindElement(By.CssSelector("#AccountFrm_agree")).Click();
    }

    public void SetLoginName(string value)
    {
        AppLogger.Log.Debug("Setting login name: '{Value}'", value);
        Type("#AccountFrm_loginname", value);
    }

    public void Submit() =>
        driver.FindElement(By.CssSelector("button[title='Continue']")).Click();

    public string GetLoginNameError()
    {
        try
        {
            // race condition: need to wait
            return _wait.Until(d =>
            {
                try
                {
                    var elements = d.FindElements(By.CssSelector(".help-block"));
                    var el = elements.FirstOrDefault(e => e.Displayed && e.Text.Contains("alphanumeric"));

                    // null for coninue the loop
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
            return string.Empty;
        }
    }

    private void Type(string css, string text)
    {
        var el = _wait.Until(d => d.FindElement(By.CssSelector(css)));
        el.Clear();
        el.SendKeys(text);
    }

    private void Select(string css, string text)
    {
        _wait.Until(d =>
        {
            try
            {
                var el = d.FindElement(By.CssSelector(css));
                var selectElement = new SelectElement(el);

                if (selectElement.Options.Any(o => o.Text == text))
                {
                    selectElement.SelectByText(text);
                    return true;
                }
                // wait for options to load
                return false;
            }
            catch (StaleElementReferenceException)
            {
                // ajax reloaded select element, ignore and go next iteration
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        });
    }
}

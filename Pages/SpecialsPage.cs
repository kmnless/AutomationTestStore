using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AutomationTestStore.Core;

namespace AutomationTestStore.Pages;

public class SpecialsPage(IWebDriver driver)
{
    private readonly WebDriverWait _wait = new(driver, TimeSpan.FromSeconds(10));

    public void Open()
    {
        AppLogger.Log.Information("Opening Specials page");
        driver.FindElement(By.CssSelector("a[href*='special']")).Click();
        _wait.Until(d => d.Url.Contains("special"));
    }

    public List<string> GetProductsWithoutDiscount()
    {
        var bad = new List<string>();
        foreach (var product in driver.FindElements(By.CssSelector(".col-md-3.col-sm-6.col-xs-12")))
        {
            bool hasOld = product.FindElements(By.CssSelector(".priceold")).Any(e => e.Displayed);
            bool hasNew = product.FindElements(By.CssSelector(".pricenew")).Any(e => e.Displayed);

            if (!hasOld || !hasNew)
            {
                var name = TryGetName(product);
                AppLogger.Log.Warning("No discount on: '{Name}'", name);
                bad.Add(name);
            }
        }
        return bad;
    }

    private static string TryGetName(IWebElement p)
    {
        try
        {
            // i guess typo from devs
            return p.FindElement(By.CssSelector("a.prdocutname")).Text.Trim();
        }
        catch { return "Unknown"; }
    }
}

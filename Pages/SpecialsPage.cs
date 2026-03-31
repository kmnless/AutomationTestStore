using OpenQA.Selenium;
using AutomationTestStore.Core;

namespace AutomationTestStore.Pages;

public class SpecialsPage(IWebDriver driver) : BasePage(driver)
{
    private readonly By _specialsMenuLink = By.CssSelector("a[href*='special']");
    private readonly By _productBlock = By.CssSelector(".col-md-3.col-sm-6.col-xs-12");
    private readonly By _priceOld = By.CssSelector(".priceold");
    private readonly By _priceNew = By.CssSelector(".pricenew");
    private readonly By _productName = By.CssSelector("a.prdocutname");

    public void Open()
    {
        AppLogger.Log.Information("Opening Specials page");
        WaitAndFindElement(_specialsMenuLink).Click();
        Wait.Until(d => d.Url.Contains("special"));
    }

    public List<string> GetProductsWithoutDiscount()
    {
        var bad = new List<string>();

        // FindElements isnt throwing exception if nothing found
        foreach (var product in Driver.FindElements(_productBlock))
        {
            bool hasOld = product.FindElements(_priceOld).Any(e => e.Displayed);
            bool hasNew = product.FindElements(_priceNew).Any(e => e.Displayed);

            if (!hasOld || !hasNew)
            {
                var name = TryGetName(product);
                AppLogger.Log.Warning("No discount on: '{Name}'", name);
                bad.Add(name);
            }
        }
        return bad;
    }

    private string TryGetName(IWebElement p)
    {
        try { return p.FindElement(_productName).Text.Trim(); }
        catch { return "Unknown"; }
    }
}
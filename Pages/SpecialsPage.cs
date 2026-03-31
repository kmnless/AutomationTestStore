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

        GetVisibleElement(_specialsMenuLink, "Specials Menu Link").Click();

        try
        {
            Wait.Until(d => d.Url.Contains("special", StringComparison.OrdinalIgnoreCase));
        }
        catch (WebDriverTimeoutException)
        {
            throw new NotFoundException("Cannot open Specials page.");
        }
    }

    public List<string> GetProductsWithoutDiscount()
    {
        var bad = new List<string>();

        try
        {
            Wait.Until(d => d.FindElements(_productBlock).Count > 0);
        }
        catch (WebDriverTimeoutException)
        {
            throw new NotFoundException("Product Block not found on Specials page.");
        }

        foreach (var product in Driver.FindElements(_productBlock))
        {
            bool hasOld = product.FindElements(_priceOld).Any(e => e.Displayed);
            bool hasNew = product.FindElements(_priceNew).Any(e => e.Displayed);

            if (!hasOld || !hasNew)
            {
                var name = GetProductName(product);
                // AppLogger.Log.Warning("No discount on: '{Name}'", name);
                bad.Add(name);
            }
        }
        return bad;
    }

    private string GetProductName(IWebElement product)
    {
        var nameElements = product.FindElements(_productName);

        if (nameElements.Count == 0 || !nameElements[0].Displayed)
        {
            throw new NotFoundException("Cannot find product name.");
        }

        return nameElements[0].Text.Trim();
    }
}
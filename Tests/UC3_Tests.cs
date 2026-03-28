using AutomationTestStore.Core;
using AutomationTestStore.Pages;
using FluentAssertions;
using Xunit;

namespace AutomationTestStore.Tests;

[Collection("Chrome")]
public class UC3_Chrome() : BaseTest(BrowserType.Chrome)
{
    [Fact]
    public void SpecialsPage_AllProducts_ShouldHaveDiscount()
    {
        AppLogger.Log.Information("[UC-3][Chrome] Checking specials");
        Driver.Navigate().GoToUrl("https://automationteststore.com");

        var page = new SpecialsPage(Driver);
        page.Open();

        var missing = page.GetProductsWithoutDiscount();
        missing.Should().BeEmpty(
            "every product on Specials page must have a discount, missing: "
            + string.Join(", ", missing));
    }
}

[Collection("Firefox")]
public class UC3_Firefox() : BaseTest(BrowserType.Firefox)
{
    [Fact]
    public void SpecialsPage_AllProducts_ShouldHaveDiscount()
    {
        AppLogger.Log.Information("[UC-3][Firefox] Checking specials");
        Driver.Navigate().GoToUrl("https://automationteststore.com");

        var page = new SpecialsPage(Driver);
        page.Open();

        var missing = page.GetProductsWithoutDiscount();
        missing.Should().BeEmpty(
            "every product on Specials page must have a discount, missing: "
            + string.Join(", ", missing));
    }
}

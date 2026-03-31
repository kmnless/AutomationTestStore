using AutomationTestStore.Core;
using AutomationTestStore.Pages;
using FluentAssertions;
using Xunit;

namespace AutomationTestStore.Tests;

public abstract class UC3_Tests : BaseTest
{
    protected readonly BrowserType browser;

    protected UC3_Tests(BrowserType browser) : base(browser)
    {
        this.browser = browser;
    }

    [Fact]
    public void SpecialsPage_AllProducts_ShouldHaveDiscount()
    {
        AppLogger.Log.Information("[UC-3][{Browser}] Checking specials", browser);
        Driver.Navigate().GoToUrl(ConfigHelper.BaseUrl);

        var page = new SpecialsPage(Driver);
        page.Open();

        var missing = page.GetProductsWithoutDiscount();
        missing.Should().BeEmpty(
            "every product on Specials page must have a discount, missing: "
            + string.Join(", ", missing));
    }
}

[Collection("Chrome")]
public class UC3_Chrome : UC3_Tests
{
    public UC3_Chrome() : base(BrowserType.Chrome) { }
}

[Collection("Firefox")]
public class UC3_Firefox : UC3_Tests
{
    public UC3_Firefox() : base(BrowserType.Firefox) { }
}
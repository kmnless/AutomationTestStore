using AutomationTestStore.Core;
using AutomationTestStore.Pages;
using FluentAssertions;
using Xunit;

namespace AutomationTestStore.Tests;

public abstract class UC2_Tests : BaseTest
{
    protected readonly BrowserType browser;

    protected UC2_Tests(BrowserType browser) : base(browser)
    {
        this.browser = browser;
    }

    [Theory, MemberData(nameof(TestData.InvalidLogins), MemberType = typeof(TestData))]
    public void InvalidLoginName_ShouldShowError(string loginName, string description)
    {
        AppLogger.Log.Information("[UC-2][{Browser}] Case: '{Desc}'", browser, description);

        var login = new LoginPage(Driver);
        login.Open();
        login.ClickContinueNewCustomer();

        var reg = new RegistrationPage(Driver);
        reg.SetLoginName(loginName);
        reg.Submit();

        var error = reg.GetLoginNameError();
        error.Should().NotBeNullOrWhiteSpace("error must appear for: " + description);
        error.Should().MatchRegex(@"Login name must be alphanumeric only and between \d+ and \d+ characters!");
    }
}

[Collection("Chrome")]
public class UC2_Chrome : UC2_Tests
{
    public UC2_Chrome() : base(BrowserType.Chrome) { }
}

[Collection("Firefox")]
public class UC2_Firefox : UC2_Tests
{
    public UC2_Firefox() : base(BrowserType.Firefox) { }
}
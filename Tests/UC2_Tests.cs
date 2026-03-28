using AutomationTestStore.Core;
using AutomationTestStore.Pages;
using FluentAssertions;
using Xunit;

namespace AutomationTestStore.Tests;

[Collection("Chrome")]
public class UC2_Chrome() : BaseTest(BrowserType.Chrome)
{
    [Theory, MemberData(nameof(TestData.InvalidLogins), MemberType = typeof(TestData))]
    public void InvalidLoginName_ShouldShowError(string loginName, string description)
    {
        AppLogger.Log.Information("[UC-2][Chrome] Case: '{Desc}'", description);

        var login = new LoginPage(Driver);
        login.Open();
        login.ClickContinueNewCustomer();

        var reg = new RegistrationPage(Driver);
        reg.SetLoginName(loginName);
        reg.Submit();

        var error = reg.GetLoginNameError();
        error.Should().NotBeNullOrWhiteSpace("error must appear for: " + description);
        error.Should().MatchRegex(
            @"Login name must be alphanumeric only and between \d+ and \d+ characters!");
    }
}

[Collection("Firefox")]
public class UC2_Firefox() : BaseTest(BrowserType.Firefox)
{
    [Theory, MemberData(nameof(TestData.InvalidLogins), MemberType = typeof(TestData))]
    public void InvalidLoginName_ShouldShowError(string loginName, string description)
    {
        AppLogger.Log.Information("[UC-2][Firefox] Case: '{Desc}'", description);

        var login = new LoginPage(Driver);
        login.Open();
        login.ClickContinueNewCustomer();

        var reg = new RegistrationPage(Driver);
        reg.Submit();
        reg.SetLoginName(loginName);
        reg.Submit();

        var error = reg.GetLoginNameError();
        error.Should().NotBeNullOrWhiteSpace("error must appear for: " + description);
        error.Should().MatchRegex(
            @"Login name must be alphanumeric only and between \d+ and \d+ characters!");
    }
}

using System;
using AutomationTestStore.Core;
using AutomationTestStore.Pages;
using FluentAssertions;
using Xunit;

namespace AutomationTestStore.Tests;

public abstract class UC1_Tests : BaseTest
{
    protected readonly BrowserType browser;

    protected UC1_Tests(BrowserType browser) : base(browser)
    {
        this.browser = browser;
    }

    [Theory, MemberData(nameof(TestData.ValidUsers), MemberType = typeof(TestData))]
    public void CreateAccount_ShouldLoginSuccessfully(UserData baseUser)
    {
        var uniqueSuffix = DateTime.Now.Ticks.ToString()[^6..];
        var user = baseUser with
        {
            LoginName = baseUser.LoginName + uniqueSuffix,
            Email = uniqueSuffix + baseUser.Email
        };

        AppLogger.Log.Information("[UC-1][{Browser}] Login: '{L}'", browser, user.LoginName);

        var login = new LoginPage(Driver);
        login.Open();
        login.ClickContinueNewCustomer();

        var reg = new RegistrationPage(Driver);
        reg.FillForm(user);
        reg.Submit();

        var account = new AccountPage(Driver);
        account.IsDisplayed().Should().BeTrue("My Account page must open");
        account.GetWelcomeText().Should().Contain(user.FirstName, "header must contain the username (first name)");
    }
}

[Collection("Chrome")]
public class UC1_Chrome : UC1_Tests
{
    public UC1_Chrome() : base(BrowserType.Chrome) { }
}

[Collection("Firefox")]
public class UC1_Firefox : UC1_Tests
{
    public UC1_Firefox() : base(BrowserType.Firefox) { }
}
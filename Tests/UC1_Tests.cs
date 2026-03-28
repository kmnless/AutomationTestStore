using AutomationTestStore.Core;
using AutomationTestStore.Pages;
using FluentAssertions;
using Xunit;

namespace AutomationTestStore.Tests;

[Collection("Chrome")]
public class UC1_Chrome() : BaseTest(BrowserType.Chrome)
{
    [Theory, MemberData(nameof(TestData.ValidUsers), MemberType = typeof(TestData))]
    public void CreateAccount_ShouldLoginSuccessfully(UserData baseUser)
    {
        // generate unique login and email to avoid conflicts with existing accounts since the site doesnt allow duplicates
        var uniqueSuffix = DateTime.Now.Ticks.ToString()[^6..];
        var user = baseUser with
        {
            LoginName = baseUser.LoginName + uniqueSuffix,
            Email = uniqueSuffix + baseUser.Email
        };

        AppLogger.Log.Information("[UC-1] Login: '{L}'", user.LoginName);

        var login = new LoginPage(Driver);
        login.Open();
        login.ClickContinueNewCustomer();

        var reg = new RegistrationPage(Driver);
        reg.FillForm(user);
        reg.Submit();

        var account = new AccountPage(Driver);
        account.IsDisplayed().Should().BeTrue("My Account page must open");
        account.GetWelcomeText().Should().Contain(user.FirstName,
            "header must contain the username (first name)");
    }

    [Collection("Firefox")]
    public class UC1_Firefox() : BaseTest(BrowserType.Firefox)
    {
        [Theory, MemberData(nameof(TestData.ValidUsers), MemberType = typeof(TestData))]
        public void CreateAccount_ShouldLoginSuccessfully(UserData user)
        {
            AppLogger.Log.Information("[UC-1][Firefox] Login: '{L}'", user.LoginName);

            var login = new LoginPage(Driver);
            login.Open();
            login.ClickContinueNewCustomer();

            var reg = new RegistrationPage(Driver);
            reg.FillForm(user);
            reg.Submit();

            var account = new AccountPage(Driver);
            account.IsDisplayed().Should().BeTrue("My Account page must open");
            account.GetWelcomeText().Should().Contain(user.FirstName,
                "header must contain the username (first name)");
        }
    }
}

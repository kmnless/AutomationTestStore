using Xunit;

namespace AutomationTestStore.Core;

public record UserData(
    string FirstName,
    string LastName,
    string Email,
    string Telephone,
    string Address1,
    string City,
    string ZipCode,
    string Country,
    string Region,
    string LoginName,
    string Password
);

public static class TestData
{
    public static TheoryData<UserData> ValidUsers => new()
    {
        new UserData("John", "Doe", "john.test001@mailinator.com", "1234567890",
                     "123 Main St", "New York", "10001", "United States",
                     "New York", "jdoe_test001", "Test@12345"),

        new UserData("Jane", "Smith", "jane.test002@mailinator.com", "0987654321",
                     "456 Oak Ave", "Los Angeles", "90001", "United States",
                     "California", "jsmith_test002", "Test@67890")
    };

    public static TheoryData<string, string> InvalidLogins => new()
    {
        { "", "empty" },
        { "a", "too short (1 char)" },
        { "ab", "too short (2 chars)" },
        { "invalid name!", "special chars / spaces" },
        { new string('x', 65), "too long (65 chars)" }
    };
}
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AutomationTestStore.Core;

public static class ConfigHelper
{
    private static readonly IConfigurationRoot Config;

    static ConfigHelper()
    {
        Config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }

    public static string BaseUrl => Config["AppSettings:BaseUrl"]
        ?? throw new InvalidOperationException("BaseUrl not found in appsettings.json");
}
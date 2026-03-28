using Serilog;

namespace AutomationTestStore.Core;

// Singleton logger — console + rolling file
public static class AppLogger
{
    public static readonly ILogger Log = new LoggerConfiguration()
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}")
        .WriteTo.File("logs/tests-.log", rollingInterval: RollingInterval.Day)
        .CreateLogger();
}

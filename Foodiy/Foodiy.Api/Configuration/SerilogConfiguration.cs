using Serilog;

namespace Foodiy.Api.Configuration;

public static class SerilogConfiguration
{
    public static LoggerConfiguration Configure(this LoggerConfiguration config)
    {
        return config
            .MinimumLevel.Information()
            .WriteTo.Console();
    }
}

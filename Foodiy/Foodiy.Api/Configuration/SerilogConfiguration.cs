using Serilog;
using Serilog.Events;

namespace Foodiy.Api.Configuration;

public static class SerilogConfiguration
{
    private const string _outputTemplate = "[{Timestamp:HH:mm:ss.fff}][{Level:u3}] {Message}{NewLine}{Exception}";

    public static LoggerConfiguration Configure(this LoggerConfiguration config)
    {
        return config
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .WriteTo.Console(outputTemplate: _outputTemplate);
    }
}

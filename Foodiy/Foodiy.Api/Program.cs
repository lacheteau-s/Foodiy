using Foodiy.Api.Configuration;
using Serilog;

namespace Foodiy.Api;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        try
        {
            Log.Information("Starting application");

            var app = WebApplication
                .CreateBuilder(args)
                .ConfigureServices()
                .Build()
                .ConfigureMiddlewares();

            Log.Information("Application running");

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
using Foodiy.Api.Configuration;
using Foodiy.Data;
using Serilog;

namespace Foodiy.Api;

public class Program
{
    public async static Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .Configure()
            .CreateBootstrapLogger();

        try
        {
            Log.Information("Starting application");

            var app = WebApplication
                .CreateBuilder(args)
                .ConfigureServices()
                .Build()
                .ConfigureMiddlewares();

            var dbManager = app.Services.GetRequiredService<IDatabaseManager>();

            await dbManager.InitializeDatabase();

            Log.Information("Application running");

            await app.RunAsync();
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
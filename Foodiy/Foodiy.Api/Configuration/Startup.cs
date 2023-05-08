using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Foodiy.Api.Configuration;

public static class Startup
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        // Add services to the container.

        services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteConvention(new RouteAttribute("/api")));
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        builder.Host.UseSerilog((context, config) => config.Configure());

        return builder;
    }

    public static WebApplication ConfigureMiddlewares(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSerilogRequestLogging();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace SerilogDIExample;

public class Program
{
    public static void Main()
    {
        var builder = new ConfigurationBuilder();
        BuildConfiguration(builder);

        Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(builder.Build())
                            .Enrich.FromLogContext()
                            .WriteTo.Console()
                            .CreateLogger();
        Log.Logger.Information("Application log starts now");

        var host = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                        services.TryAddTransient<IGreetingService, GreetingService>();
                    })
                    .UseSerilog()
                    .Build();

        var svc = ActivatorUtilities.CreateInstance<GreetingService>(host.Services);
        svc.Run();

    }
    public static void BuildConfiguration(IConfigurationBuilder builder)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }
        builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional:false, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}")
                .AddEnvironmentVariables();

    }
}

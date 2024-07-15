using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Greggs.Products.Api;

public class Program
{
    private static decimal version = 1.0m;
    
    public static void Main(string[] args)
    {
        //SERILOG
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()       
            .WriteTo.Console()             
            .WriteTo.File("Logs/Log.txt")  
            .CreateLogger();
        
        try
        {
            Log.Information("Starting up version {version}", version);
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);
        builder.UseSerilog();
        builder.ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
        return builder;
    }
}

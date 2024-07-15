using Greggs.Produces.Data.DataAccess;
using Greggs.Products.Api.Services;
using Greggs.Products.Api.Services.ProductService;
using Greggs.Products.Models.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Greggs.Products.Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthorization();
        
        services
            .AddFastEndpoints()
            .SwaggerDocument(o =>
            {
                o.DocumentSettings = s =>
                {
                    s.Version = "v1";
                    s.Title = "Greggs Products API V1";
                };
            });

        ConfigureDependencyInjection(services);
    }

    public void ConfigureDependencyInjection(IServiceCollection services)
    {
        services.AddScoped<IProductAccess, ProductAccess>();
        services.AddScoped<ICurrencyConverter, CurrencyConverter>();
        services.AddScoped<IEntrepreneurProductService, EntrepreneurProductService>();
        services.AddScoped<IFanaticProductService, FanaticProductService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapFastEndpoints(c => c.Errors.UseProblemDetails());
        });
        app.UseSwaggerGen();
    }
}
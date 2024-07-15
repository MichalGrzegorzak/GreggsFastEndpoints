using System.Threading;
using System.Threading.Tasks;
using Greggs.Products.Api.Endpoints.Fanatic.GetProduct;

namespace Greggs.Products.Api.Endpoints.Processors;

/// <summary>
/// Tracks response execution, allow us to reacts for certain things
/// example: exceptions, certain values, etc.
/// </summary>
public class ForeignCurrencyLogger<TRequest, TResponse> : IPostProcessor<TRequest, TResponse>
{
    public Task PostProcessAsync(IPostProcessorContext<TRequest, TResponse> ctx, CancellationToken ct)
    {
        var logger = ctx.HttpContext.Resolve<ILogger<TResponse>>();

        if (ctx.Response is FanaticGetProductResponse response)
        {
            var result = response.Results.FirstOrDefault();
            if (result != null && result.Currency != "GBP")
            {
                logger.LogWarning($"Foreign currency was requested: {response.Results.First().Currency}");
            }
        }
           
        return Task.CompletedTask;
    }
}
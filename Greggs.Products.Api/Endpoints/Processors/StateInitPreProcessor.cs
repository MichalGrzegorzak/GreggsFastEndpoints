using System.Threading;
using System.Threading.Tasks;
using Greggs.Products.Api.Endpoints.Base;
using Microsoft.AspNetCore.Http;

namespace Greggs.Products.Api.Endpoints.Processors;

public class StateInitPreProcessor : IPreProcessor<RequestBase>
{
    public async Task PreProcessAsync(IPreProcessorContext<RequestBase> ctx, CancellationToken ct)
    {
        //start's the timer
        var state = ctx.HttpContext.ProcessorState<StateBag>();
        var logger = ctx.HttpContext.Resolve<ILogger<DurationLogger>>();
            
        state.EndpointName = ctx.HttpContext.GetEndpoint()?.DisplayName!;
        logger.LogInformation($"START: {state.EndpointName}");
        
        await Task.CompletedTask; 
    }
}
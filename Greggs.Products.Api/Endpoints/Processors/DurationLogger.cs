using System.Threading;
using System.Threading.Tasks;
using Greggs.Products.Api.Endpoints.Base;

namespace Greggs.Products.Api.Endpoints.Processors;

public class DurationLogger : PostProcessor<RequestBase, StateBag, object>
{
    public override Task PostProcessAsync(IPostProcessorContext<RequestBase, object> ctx, 
        StateBag state, 
        CancellationToken ct)
    {
        var logger = ctx.HttpContext.Resolve<ILogger<DurationLogger>>();
        logger.LogInformation("FINISHED: {@endpoint} duration: {@duration} ms.", state.EndpointName, state.DurationMillis);

        return Task.CompletedTask;
    }
}
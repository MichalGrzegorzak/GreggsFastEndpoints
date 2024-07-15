using System.Threading;
using System.Threading.Tasks;
using Greggs.Products.Api.Endpoints.Processors;
using Greggs.Products.Models;
using Greggs.Products.Models.Entities;
using Greggs.Products.Models.Interfaces;

namespace Greggs.Products.Api.Endpoints.Fanatic.GetProduct;

public class FanaticGetProductEndpoint : EndpointWithMapping<FanaticGetProductRequest, FanaticGetProductResponse, FanaticGetProductRequest>
{
    private readonly IFanaticProductService _productService;
    private readonly ILogger<FanaticGetProductEndpoint> _logger;

    public FanaticGetProductEndpoint(IFanaticProductService productService, ILogger<FanaticGetProductEndpoint> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    public override void Configure()
    {
        Get("/api/fanatic/products");
        AllowAnonymous();

        //duration tracking
        PreProcessor<StateInitPreProcessor>();
        PostProcessor<DurationLogger>();
    }

    public override async Task HandleAsync(FanaticGetProductRequest req, CancellationToken ct)
    {
        var query = new QuerySpec<Product>();
        query.SetPaging(req.PageStart, req.PageSize);

        //forcing sort by Created date
        query.AddSorting(p => p.Created, SortDirection.Descending);

        var results = _productService.GetProductsList(query).ToList();

        //await SendOkAsync(new GetProductResponse(req)) //you can choose to include or not request in the response
        await SendOkAsync(new FanaticGetProductResponse()
        {
            Success = true,
            Results = results
        }, ct);
    }
}
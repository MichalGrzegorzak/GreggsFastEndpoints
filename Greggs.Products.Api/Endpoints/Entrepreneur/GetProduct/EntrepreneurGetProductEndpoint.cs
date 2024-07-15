using System.Threading;
using System.Threading.Tasks;
using Greggs.Products.Api.Endpoints.Fanatic.GetProduct;
using Greggs.Products.Api.Endpoints.Processors;
using Greggs.Products.Models;
using Greggs.Products.Models.Entities;
using Greggs.Products.Models.Extensions;
using Greggs.Products.Models.Interfaces;

namespace Greggs.Products.Api.Endpoints.Entrepreneur.GetProduct;

public class EntrepreneurGetProductEndpoint : Endpoint<EntrepreneurGetProductRequest, FanaticGetProductResponse>
{
    private readonly IEntrepreneurProductService _productService;
    private readonly ILogger<EntrepreneurGetProductEndpoint> _logger;

    public EntrepreneurGetProductEndpoint(IEntrepreneurProductService productService, ILogger<EntrepreneurGetProductEndpoint> logger)
    {
        _productService = productService;
        _logger = logger;
    }
    
    public override void Configure()
    {
        Get("/api/entrepreneur/products");
        AllowAnonymous();
        Validator<EntrepreneurGetProductRequestValidator>();
        
        PreProcessor<StateInitPreProcessor>();
        PostProcessor<DurationLogger>();
        PostProcessor<ForeignCurrencyLogger<EntrepreneurGetProductRequest,FanaticGetProductResponse>>();
    }

    public override async Task HandleAsync(EntrepreneurGetProductRequest req, CancellationToken ct)
    {
        var state = ProcessorState<StateBag>();
        _logger.LogInformation("EntrepreneurGetProductEndpoint executed at {@duration} ms.", state.DurationMillis);
        
        var query = new QuerySpec<Product>();
        query.SetPaging(req.PageStart, req.PageSize);
        
        var results = _productService.GetProductsList(query, req.Currency).ToList();
        
        await SendOkAsync(new FanaticGetProductResponse(req)
        {
            Success = true,
            Results = results
        }, ct);
        
        await Task.Delay(500, ct); //let's test longer execution for this endpoint
    }
}

/// <summary>
/// Fluent validator for this request
/// </summary>
public class EntrepreneurGetProductRequestValidator : Validator<EntrepreneurGetProductRequest>
{
    public EntrepreneurGetProductRequestValidator()
    {
        RuleFor(x => x.Currency).IsValidIsoCurrency();
    }
}
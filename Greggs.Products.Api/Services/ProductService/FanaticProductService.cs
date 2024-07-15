using Greggs.Products.Models;
using Greggs.Products.Models.Dto;
using Greggs.Products.Models.Entities;
using Greggs.Products.Models.Interfaces;

namespace Greggs.Products.Api.Services.ProductService;

/// <summary>
/// Product service for Fanatic role
/// </summary>
public class FanaticProductService : IFanaticProductService
{
    private readonly IProductAccess _productAccess;
    private readonly ILogger<FanaticProductService> _logger;

    public FanaticProductService(IProductAccess productAccess, ILogger<FanaticProductService> logger)
    {
        _productAccess = productAccess;
        _logger = logger;
    }

    public IEnumerable<ProductDto> GetProductsList(QuerySpec<Product> query)
    {
        Guard.Against.Null(query, nameof(query));

        var products = _productAccess.List(query);
        var results = products.Select(p => new ProductDto(p));
        return results;
    }
}
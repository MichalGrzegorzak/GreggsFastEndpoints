using Greggs.Products.Models;
using Greggs.Products.Models.Dto;
using Greggs.Products.Models.Entities;
using Greggs.Products.Models.Interfaces;

namespace Greggs.Products.Api.Services.ProductService;

/// <summary>
/// Product service for Entrepreneur role
/// </summary>
public class EntrepreneurProductService : IEntrepreneurProductService
{
    private readonly IProductAccess _productAccess;
    private readonly ILogger<EntrepreneurProductService> _logger;
    private readonly ICurrencyConverter _currencyConverter;
    
    public EntrepreneurProductService(IProductAccess productAccess, 
        ILogger<EntrepreneurProductService> logger, 
        ICurrencyConverter currencyConverter)
    {
        _productAccess = productAccess;
        _logger = logger;
        _currencyConverter = currencyConverter;
    }

    /// <summary>
    /// Gets list of product, and convert it to specified currency if required
    /// </summary>
    public IEnumerable<ProductDto> GetProductsList(QuerySpec<Product> query, string currency)
    {
        Guard.Against.Null(query, nameof(query));
        Guard.Against.NullOrEmpty(currency, nameof(currency));
        
        var productDtos = _productAccess.List(query)
            .Select(p => new ProductDto(p))
            .ToList();
        
        foreach (var dto in productDtos)
        {
            dto.Currency = currency;
            dto.Price = _currencyConverter.ConvertGbpPriceToForeign(dto.Price, currency);
        }
        
        return productDtos;
    }
}


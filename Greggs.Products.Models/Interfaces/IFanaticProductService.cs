using Greggs.Products.Models.Dto;
using Greggs.Products.Models.Entities;

namespace Greggs.Products.Models.Interfaces;

public interface IFanaticProductService
{
    IEnumerable<ProductDto> GetProductsList(QuerySpec<Product> query);
}
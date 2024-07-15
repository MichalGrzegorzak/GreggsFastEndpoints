using Greggs.Products.Models.Dto;
using Greggs.Products.Models.Entities;

namespace Greggs.Products.Models.Interfaces;

public interface IEntrepreneurProductService
{
    IEnumerable<ProductDto> GetProductsList(QuerySpec<Product> query, string currency);
}
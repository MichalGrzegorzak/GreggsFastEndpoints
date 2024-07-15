using Greggs.Products.Models.Entities;

namespace Greggs.Products.Models.Dto;

public class ProductDto
{
    public ProductDto()
    {
    }
    
    public ProductDto(Product prod)
    {
        Name = prod.Name;
        Price = prod.PriceInPounds;
        Currency = "GBP";
        Created = prod.Created;
    }
    
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
    public DateTime Created { get; set; }
}
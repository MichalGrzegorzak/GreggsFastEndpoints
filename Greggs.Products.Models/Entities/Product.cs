namespace Greggs.Products.Models.Entities;

public class Product
{
    public string Name { get; set; }
    public decimal PriceInPounds { get; set; }
    
    public DateTime Created { get; set; } = DateTime.UtcNow;
}
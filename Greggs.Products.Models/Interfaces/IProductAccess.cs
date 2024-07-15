using Greggs.Products.Models.Entities;

namespace Greggs.Products.Models.Interfaces;

public interface IProductAccess : IDataAccess<Product>
{
    public List<Product> List(QuerySpec<Product> spec);
}
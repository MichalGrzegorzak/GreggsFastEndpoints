using System.Linq.Expressions;
using Greggs.Products.Models;
using Greggs.Products.Models.Entities;
using Greggs.Products.Models.Interfaces;

namespace Greggs.Produces.Data.DataAccess;

/// <summary>
/// DISCLAIMER: This is only here to help enable the purpose of this exercise, this doesn't reflect the way we work!
/// </summary>
public class ProductAccess : IProductAccess
{
    private static readonly IEnumerable<Product> ProductDatabase = new List<Product>()
    {
        new() { Name = "Sausage Roll", PriceInPounds = 1m, Created = new DateTime(2023, 1, 1)},
        new() { Name = "Vegan Sausage Roll", PriceInPounds = 1.1m, Created = new DateTime(2023, 3, 1) },
        new() { Name = "Steak Bake", PriceInPounds = 1.2m, Created = new DateTime(2022, 1, 1) },
        new() { Name = "Yum Yum", PriceInPounds = 0.7m, Created = new DateTime(2021, 1, 1) },
        new() { Name = "Pink Jammie", PriceInPounds = 0.5m, Created = new DateTime(2024, 1, 1) },
        new() { Name = "Mexican Baguette", PriceInPounds = 2.1m, Created = new DateTime(2000, 1, 1) },
        new() { Name = "Bacon Sandwich", PriceInPounds = 1.95m, Created = new DateTime(2023, 2, 1) },
        new() { Name = "Coca Cola", PriceInPounds = 1.2m, Created = new DateTime(2023, 4, 1) }
    };

    /// <summary>
    /// Ordering missing, skipping is broken
    /// </summary>
    public IEnumerable<Product> List(int? pageStart, int? pageSize, bool ascending = false)
    {
        var queryable = ProductDatabase.AsQueryable();

        if (pageStart.HasValue)
            queryable = queryable.Skip(pageStart.Value); //looks like a bug, as we don't really skip pages this way

        if (pageSize.HasValue)
            queryable = queryable.Take(pageSize.Value);

        return queryable;
    }
    
    /// <summary>
    /// Better List method - with query specification
    /// </summary>
    public List<Product> List(QuerySpec<Product> spec)
    {
        var queryable = ProductDatabase.AsQueryable();

        foreach (var sort in spec.Sorting)
        {
            queryable = OrderByProperty(queryable, sort.Field, sort.Direction == SortDirection.Ascending);
        }

        queryable = queryable
            .Skip((spec.Paging.PageStart - 1) * spec.Paging.PageSize)
            .Take(spec.Paging.PageSize);

        return queryable.ToList();
    }
    private static IQueryable<T> OrderByProperty<T>(IQueryable<T> source, Expression<Func<T, object>> field, bool ascending)
    {
        var parameter = field.Parameters.First();
        
        MemberExpression property;
        if (field.Body is UnaryExpression unaryExpression)
            property = (MemberExpression)unaryExpression.Operand;
        else
            property = (MemberExpression)field.Body;
        
        var lambda = Expression.Lambda(property, parameter);

        string methodName = ascending ? "OrderBy" : "OrderByDescending";
        var method = typeof(Queryable).GetMethods().First(
            m => m.Name == methodName
                 && m.IsGenericMethodDefinition
                 && m.GetGenericArguments().Length == 2
                 && m.GetParameters().Length == 2);

        var genericMethod = method.MakeGenericMethod(typeof(T), property.Type);

        return (IQueryable<T>)genericMethod.Invoke(null, new object[] { source, lambda });
    }
}
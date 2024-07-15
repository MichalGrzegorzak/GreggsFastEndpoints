namespace Greggs.Products.Models.Interfaces;

public interface IDataAccess<T>
{
    IEnumerable<T> List(int? pageStart, int? pageSize, bool ascending = false);
}
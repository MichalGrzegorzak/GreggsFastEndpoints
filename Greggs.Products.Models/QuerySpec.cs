using System.Linq.Expressions;

namespace Greggs.Products.Models;

/// <summary>
/// Query specification pattern
/// Can be used to specify Paging, sorting, or filtering(*)
/// * - missing
/// </summary>
public class QuerySpec<T>
{
    public Pager Paging { get; private set; } = new();
    public List<Sort> Sorting { get; private set; } = new();

    public void SetPaging(int? page, int? size)
    {
        Paging = new Pager { PageStart = page ?? 1, PageSize = size ?? 10 };
    }

    public void AddSorting(Expression<Func<T, object>> field, SortDirection direction)
    {
        Sorting.Add(new Sort { Field = field, Direction = direction });
    }
    
    public class Pager
    {
        public int PageStart { get; set; }

        public int PageSize { get; set; } = 100;
    }

    public class Sort
    {
        public Expression<Func<T, object>> Field { get; set; }
        public SortDirection Direction { get; set; }
    }
}

public enum SortDirection
{
    Ascending,
    Descending
}
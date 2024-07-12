namespace Application.Common.Models;

/// <summary>
/// A list type that allows pagination. This class breaks
/// a generic collection into a list of pages.
/// </summary>
public class PaginatedList<T>
{
    public IReadOnlyCollection<T> Items { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public int Count => Items.Count;

    public int FirstItemNumber { get; }
    public int LastItemNumber { get; }

    public PaginatedList(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
        FirstItemNumber = pageSize * (pageNumber - 1) + 1;
        LastItemNumber = FirstItemNumber + Items.Count - 1;
    }

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    //Get the number of pages after the current page
    public int NextPages => TotalPages - PageNumber;

    //Get the number of pages before the current page
    public int PreviousPages => PageNumber - 1;

    /// <summary>
    /// Retrieves the items at the specified <paramref name="pageNumber"/>
    /// given the specified <paramref name="pageSize"/>
    /// </summary>
    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}

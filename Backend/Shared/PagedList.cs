using MongoDB.Driver.Linq;

namespace Shared;

public class PagedList<T>
{
    private PagedList(List<T> items, int totalCount, int page, int pageSize)
    {
        Items = items.AsReadOnly();
        TotalCount = totalCount;
        Page = page;
        PageSize = pageSize;
    }

    public IReadOnlyList<T> Items { get; }
    public int TotalCount { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasNextPage => Page < TotalPages;
    public bool HasPreviousPage => Page > 1;

    public static async Task<PagedList<T>> CreateAsync(IEnumerable<T> source, int page, int pageSize)
    {
        if (page < 1) throw new ArgumentOutOfRangeException(nameof(page), "Page must be greater than or equal to 1.");
        if (pageSize < 1) throw new ArgumentOutOfRangeException(nameof(pageSize), "PageSize must be greater than or equal to 1.");

        var totalCount = await Task.Run(() => source.Count()); 
        var items = await Task.Run(() => source.Skip((page - 1) * pageSize).Take(pageSize).ToList()); 

        return new PagedList<T>(items, totalCount, page, pageSize);
    }
}



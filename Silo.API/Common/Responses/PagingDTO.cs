namespace Silo.API.Common.Responses;

public class PagingDTO<T> 
{
    public IReadOnlyList<T> Items { get; set; } = new List<T>();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages =>
        (int)Math.Ceiling(TotalCount / (double)PageSize);

    public PagingDTO() { }
    public PagingDTO(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        Items = items.ToList();
        TotalCount = count;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}

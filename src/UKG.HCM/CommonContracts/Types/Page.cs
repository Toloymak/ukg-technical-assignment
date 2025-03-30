namespace CommonContracts.Types;

public class Page<T>
{
    public required IReadOnlyCollection<T> Items { get; set; }
    
    public required int TotalCount { get; set; }
    public required int PageNumber { get; set; }
    public required int PageSize { get; set; }
    
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
using CommonContracts.Types;
using Microsoft.EntityFrameworkCore;

namespace UKG.HCM.Infrastructure.Extensions;

public static class PageExtensions
{
    /// Gets a page of items from the source IQueryable.
    public static async Task<Page<T>> ToPage<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken ct)
    {
        var totalCount = await source.CountAsync(ct);
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToArrayAsync(ct);

        return new Page<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}
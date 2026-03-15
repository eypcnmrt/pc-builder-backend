namespace PcBuilderBackend.Application.Common
{
    public class PagedData<T>
    {
        public IEnumerable<T> Items { get; init; } = [];
        public int TotalCount { get; init; }
        public int PageCount { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }

        public static PagedData<T> Create(IEnumerable<T> items, int totalCount, int page, int pageSize) => new()
        {
            Items = items,
            TotalCount = totalCount,
            PageCount = (int)Math.Ceiling((double)totalCount / pageSize),
            Page = page,
            PageSize = pageSize
        };
    }
}

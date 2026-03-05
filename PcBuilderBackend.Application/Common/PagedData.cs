namespace PcBuilderBackend.Application.Common
{
    public class PagedData<T>
    {
        public IEnumerable<T> Items { get; init; } = [];
        public int TotalCount { get; init; }
        public int PageCount { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
    }
}

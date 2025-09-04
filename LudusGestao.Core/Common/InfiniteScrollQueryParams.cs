namespace LudusGestao.Core.Common
{
    public class InfiniteScrollQueryParams
    {
        public string? Cursor { get; set; }
        public int PageSize { get; set; } = 10;
    }
}

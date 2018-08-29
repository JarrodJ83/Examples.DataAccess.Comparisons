namespace Requests
{
    public abstract class PagedRequest<TResponse> : IRequest<TResponse>
    {
        public int Offset { get; set; }
        public int PageSize { get; set; }
        public PagedRequest(int offset, int pageSize)
        {
            Offset = offset;
            PageSize = pageSize;
        }
    }
}

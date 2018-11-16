namespace Requests
{
    public abstract class PagedRequest<TResponse> : IRequest<TResponse>
    {
        public int Offset { get; set; }
        public int PageSize { get; set; }
    }
}

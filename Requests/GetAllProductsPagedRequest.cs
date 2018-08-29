using DomainModel;

namespace Requests
{
    public class GetAllProductsPagedRequest : PagedRequest<PagedData<Product>>
    {
        public GetAllProductsPagedRequest(int offset, int pageSize)
            : base(offset, pageSize)
        {
            
        }
    }
}

using DomainModel;

namespace Queries
{
    public class AllProductsPagedQry : PagedQuery<Product[]>
    {
        public AllProductsPagedQry(int offset, int pageSize)
            : base(offset, pageSize)
        {
            
        }
    }
}

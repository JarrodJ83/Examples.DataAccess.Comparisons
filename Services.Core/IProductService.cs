using System.Threading.Tasks;
using DomainModel;

namespace Services.Core
{
    public interface IProductService
    {
        Task<PagedData<Product>> GetAllProductsPagedAsync(int offset, int pageSize);
    }
}

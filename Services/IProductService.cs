using System.Threading.Tasks;
using DomainModel;

namespace Services
{
    public interface IProductService
    {
        Task<PagedData<Product>> GetAllProductsPagedAsync(int offset, int pageSize);
    }
}

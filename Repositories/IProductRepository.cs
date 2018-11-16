using System.Threading.Tasks;
using DomainModel;
using Repositories.Core;

namespace Repositories
{
    #region
    // TODO: ISP Violation (Too many methods to implement by client)
    #endregion
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product[]> GetPageOfProductsAsync(int offset, int pageSize);
        Task<int> GetAllProductsCountAsync();
    }
}

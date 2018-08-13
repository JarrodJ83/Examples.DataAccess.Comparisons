using DomainModel;
using Repositories.Core;

namespace Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Product[] GetAllProductsPaged(int offset, int pageSize);
    }
}

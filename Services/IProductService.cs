using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace Services
{
    public interface IProductService
    {
        Task<PagedData<Product>> GetAllProductsPagedAsync(int offset, int pageSize);
        Task<Product> GetProductAsync(int productId);
    }
}

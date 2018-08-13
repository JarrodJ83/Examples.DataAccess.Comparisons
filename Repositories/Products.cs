﻿using System.Linq;
using System.Threading.Tasks;
using DomainModel;
using Repositories.Core;

namespace Repositories
{
    public class Products : BaseRepository<Product>, IProductRepository
    {
        private readonly IEntityStore<Product> _productStore;

        public Products(IEntityStore<Product> productStore) : base(productStore)
        {
            _productStore = productStore;
        }

        public async Task<Product[]> GetPageOfProductsAsync(int offset, int pageSize)
        {
            return _productStore.Entities.Skip(offset).Take(pageSize).ToArray();
        }

        public async Task<int> GetAllProductsCount()
        {
            return _productStore.Entities.Count;
        }
    }
}

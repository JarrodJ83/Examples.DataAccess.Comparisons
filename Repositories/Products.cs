using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel;
using Repositories.Core;

namespace Repositories
{
    public class Products : IRepository<Product>
    {
        private readonly ProductStore _productStore;

        public Products(ProductStore productStore)
        {
            _productStore = productStore;
        }
        public Product GetById(int id)
        {
            return _productStore.Products.SingleOrDefault(product => product.Id == id);
        }

        public IEnumerable<Product> List()
        {
            return _productStore.Products;
        }

        public IEnumerable<Product> List(Func<Product, bool> predicate)
        {
            return _productStore.Products.Where(predicate);
        }

        public void Add(Product entity)
        {
            _productStore.Products.Add(entity);
        }

        public void Delete(Product entity)
        {
            _productStore.Products.Remove(entity);
        }

        public void Edit(Product entity)
        {
            var productToUpdate = _productStore.Products.SingleOrDefault(product => product.Id == entity.Id);

            _productStore.Products.Remove(productToUpdate);

            _productStore.Products.Add(entity);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel
{
    class ProductStore
    {
        public List<Product> Products;

        public ProductStore(int numberOfProducts)
        {
            Products = new List<Product>();
            var rand = new Random();

            var currentId = 1;
            Enumerable.Range(0, numberOfProducts).ToList()
                .ForEach(id =>
                {
                    var newProduct = new Product(currentId, LoremNET.Lorem.Words(1, 4, true), rand.NextDouble() * rand.Next(1, 1000));

                    Products.Add(newProduct);

                    currentId++;
                });
        }
    }
}

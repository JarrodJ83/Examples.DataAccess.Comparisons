using System;
using System.Collections.Generic;
using System.Linq;
using Repositories.Core;

namespace DomainModel
{
    public class ProductStore : IEntityStore<Product>
    {
        public List<Product> Entities { get; }

        public ProductStore(int numberOfProducts)
        {
            Entities = new List<Product>();
            var rand = new Random();

            var currentId = 1;
            Enumerable.Range(0, numberOfProducts).ToList()
                .ForEach(id =>
                {
                    var newProduct = new Product(currentId, LoremNET.Lorem.Words(1, 4, true), rand.NextDouble() * rand.Next(1, 1000));

                    Entities.Add(newProduct);

                    currentId++;
                });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel
{
    public interface IProductStore
    {
        List<Product> Entities { get; }
    }
    public class ProductStore : IProductStore
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
                    var newProduct = new Product(currentId, LoremNET.Lorem.Words(1, 4, true), Math.Round(rand.NextDouble() * rand.Next(1, 1000), 2));

                    Entities.Add(newProduct);

                    currentId++;
                });
        }

        private static IProductStore _current;
        public static IProductStore Current
        {
            get
            {
                //throw new Exception("Database unaccessible!");

                if (_current == null)
                    _current = new ProductStore(1000);

                return _current;
            }
        }
    }
}

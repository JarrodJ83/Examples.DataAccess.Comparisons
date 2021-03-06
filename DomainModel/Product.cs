﻿using Repositories.Core;

namespace DomainModel
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Product(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
using System;
using Repositories.Core;

namespace DomainModel
{
    public class Category : Entity
    {
        public string Description { get; set; }
        public Category(string description)
        {
            Description = description;
        }
    }
}

using IVC.ECommercePricing.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVC.ECommercePricing.Application
{
    // This would have methods on it to pull the data from the db or relevant data source
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new()
        {
            { new Product("Lettuce", 0.32m, 0.15m, 0.20m) },
            { new Product("Tomato", 0.52m, 0.15m, 0.20m) },
            { new Product("Chicken", 2.15m, 0.20m, 0.20m) },
            { new Product("Bread", 1.55m, 0.15m, 0.15m) },
            { new Product("Jam", 3.26m, 0.20m, 0.15m) }
        };

        public List<Product> GetProducts()
        {
            return _products;
        }

        public Product? GetProductByName(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(productName));
            return _products.FirstOrDefault(p => string.Equals(p.Name, productName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVC.ECommercePricing.Application.Models
{
    public class ShoppingCartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public ShoppingCartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public decimal CalculateTotalCost()
        {
            var pricePerUnit = Product.CalculatePricePerUnit();
            return pricePerUnit * Quantity;
        }
    }
}

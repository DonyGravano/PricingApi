using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVC.ECommercePricing.Application.Models
{
    public class ShoppingCartSummary
    {
        public decimal TotalPrice { get; set; }
        public decimal Tax { get; set; }

        public ShoppingCartSummary(decimal totalPrice, decimal tax)
        {
            TotalPrice = totalPrice;
            Tax = tax;
        }

        public override string ToString()
        {
            return $"Total price {TotalPrice}, Tax {Tax}";
        }
    }
}

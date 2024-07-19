using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IVC.ECommercePricing.Application.Models;

namespace IVC.ECommercePricing.Application
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IProductRepository _productRepository;

        public ShoppingCartService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public string? CalculateTotalCostOfShoppingCart(string shoppingCart)
        {
            if (string.IsNullOrWhiteSpace(shoppingCart))
                return new ShoppingCartSummary(0, 0).ToString();
            var items = shoppingCart.Split(',');
            var totalTax = 0m;
            var totalPrice = 0m;
            var preTaxPrice = 0m;
            foreach (var item in items)
            {
                var breakdown = item.Trim().Split(' ');
                if (breakdown.Length != 2)
                {
                    return null;
                }
                if (!int.TryParse(breakdown[0], out int quantity))
                {
                    // We could throw an exception here but there is currently no middleware to handle exceptions gracefully
                    quantity = 0;
                }

                var product = _productRepository.GetProductByName(breakdown[1]);
                if (product == null)
                {
                    return $"A product in the shopping cart was not a valid product. Product name: {breakdown[1]}";
                }
                totalTax += product.CalculateTaxAmount() * quantity;
                preTaxPrice += product.CalculatePreTaxPrice() * quantity;
                totalPrice += product.CalculatePricePerUnit() * quantity;
            }

            return new ShoppingCartSummary(totalPrice, totalTax).ToString();
        }
    }
}

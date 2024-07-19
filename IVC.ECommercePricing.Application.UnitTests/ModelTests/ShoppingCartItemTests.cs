using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using IVC.ECommercePricing.Application.Models;

namespace IVC.ECommercePricing.Application.UnitTests.ModelTests
{
    public class ShoppingCartItemTests
    {
        [Fact]
        public void CalculateTotalCost_QuantityIsZero_ReturnsZero()
        {
            var shoppingCartItem = new ShoppingCartItem(new Product("Lettuce", 0.32m, 0.15m, 0.2m), 0);

            var result = shoppingCartItem.CalculateTotalCost();

            result.Should().Be(0);
        }

        [Fact]
        public void CalculateTotalCost_ReturnsCorrectTotalCost()
        {
            var shoppingCartItem = new ShoppingCartItem(new Product("Lettuce", 0.32m, 0.15m, 0.2m), 4);

            var result = shoppingCartItem.CalculateTotalCost();
            
            result.Should().Be(1.8m);
        }
    }
}

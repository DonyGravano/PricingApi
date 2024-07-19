using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IVC.ECommercePricing.Application.Models;

namespace IVC.ECommercePricing.Application.UnitTests.ModelTests
{
    public class ShoppingCartSummaryTests
    {

        [Theory]
        [InlineData(10, 5, "Total price 10, Tax 5")]
        [InlineData(13, 2, "Total price 13, Tax 2")]
        [InlineData(0, 0, "Total price 0, Tax 0")]
        public void ToString_ShouldReturnCorrectlyFormattedString(decimal totalPrice, decimal tax, string expectedOutputed)
        {
            var shoppingCartSummary = new ShoppingCartSummary(totalPrice, tax);

            var result = shoppingCartSummary.ToString();
            result.Should().Be(expectedOutputed);   
        }
    }
}

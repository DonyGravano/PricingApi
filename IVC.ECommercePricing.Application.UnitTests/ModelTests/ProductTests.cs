using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using IVC.ECommercePricing.Application.Models;

namespace IVC.ECommercePricing.Application.UnitTests.ModelTests
{
    public class ProductTests
    {
        private readonly Fixture _fixture =  new();

        [Theory]
        [InlineData(0.32, 0.15, 0.2, 0.45)]
        [InlineData(0.52, 0.15, 0.2, 0.72)]
        public void CalculatePricePerUnit_WhenCalled_ReturnsCorrectPrice(decimal cost, decimal revenuePercentage,
            decimal taxPercentage, decimal expectedValue)
        {
            var model = new Product(_fixture.Create<string>(), cost, revenuePercentage, taxPercentage);

            var result = model.CalculatePricePerUnit();

            result.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData(0.32, 0.15, 0.20, 0.08)]
        [InlineData(0.52, 0.15, 0.20, 0.12)]
        [InlineData(2.15, 0.20, 0.20, 0.52)]
        public void CalculateTaxAmount_ShouldReturnCorrectTaxAmount(decimal costValue, decimal revenuePercentage, decimal taxPercentage, decimal expectedTaxAmount)
        {
            Product product = new Product(_fixture.Create<string>(), costValue, revenuePercentage, taxPercentage);
            decimal taxAmount = product.CalculateTaxAmount();
            taxAmount.Should().Be(expectedTaxAmount);
        }
    }
}

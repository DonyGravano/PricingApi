using FluentAssertions;
using IVC.ECommercePricing.Application.Models;
using FluentAssertions.Execution;
using System.Xml.Linq;
using AutoFixture.Xunit2;

namespace IVC.ECommercePricing.Application.UnitTests.RepositoryTests
{
    public class InMemoryProductRepositoryTests
    {
        private readonly InMemoryProductRepository _repository = new();
        
        [Fact]
        public void GetProducts_ShouldReturnAllProducts()
        {
            List<Product> products = _repository.GetProducts();
            using (new AssertionScope())
            {
                products.Should().NotBeNull();
                products.Should().HaveCount(5);
            }
        }

        [Theory]
        [InlineData("Lettuce", 0.32, 0.15, 0.20)]
        [InlineData("Tomato", 0.52, 0.15, 0.20)]
        [InlineData("Chicken", 2.15, 0.20, 0.20)]
        [InlineData("Bread", 1.55, 0.15, 0.15)]
        [InlineData("Jam", 3.26, 0.20, 0.15)]
        public void GetProductByName_ShouldReturnCorrectProduct(string name, decimal costValue, decimal revenuePercentage, decimal taxPercentage)
        {
            var product = _repository.GetProductByName(name);

            using (new AssertionScope())
            {
                product.Should().NotBeNull();
                product.Name.Should().Be(name);
                product.Cost.Should().Be(costValue);
                product.RevenuePercentage.Should().Be(revenuePercentage);
                product.TaxPercentage.Should().Be(taxPercentage);
            }
        }

        [Theory]
        [AutoData]
        public void GetProductByName_InvalidName_ReturnsNull(string name)
        {

            var product = _repository.GetProductByName(name);
            product.Should().BeNull();
        }

        [Fact]
        public void GetProductByName_EmptyName_ShouldThrowException()
        {
            Action act = () => _repository.GetProductByName("");
            act.Should().Throw<ArgumentException>().WithMessage("Value cannot be null or whitespace. (Parameter 'productName')");
        }

        [Fact]
        public void GetProductByName_NullName_ShouldThrowException()
        {
            Action act = () => _repository.GetProductByName(null);
            act.Should().Throw<ArgumentException>().WithMessage("Value cannot be null or whitespace. (Parameter 'productName')");
        }
    }
}

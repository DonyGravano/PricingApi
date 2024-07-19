using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoFixture;
using FluentAssertions;
using IVC.ECommercePricing.Application.Models;
using Moq;

namespace IVC.ECommercePricing.Application.UnitTests.ServiceTests;

public class ShoppingCartServiceTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly ShoppingCartService _shoppingCartService;

    public ShoppingCartServiceTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _shoppingCartService = new ShoppingCartService(_mockProductRepository.Object);

        var products = new List<Product>
        {
            new("Lettuce", 0.32m, 0.15m, 0.20m),
            new("Tomato", 0.52m, 0.15m, 0.20m),
            new("Chicken", 2.15m, 0.20m, 0.20m),
            new("Bread", 1.55m, 0.15m, 0.15m),
            new("Jam", 3.26m, 0.20m, 0.15m)
        };


        _mockProductRepository.Setup(x => x.GetProducts()).Returns(products);
        // There is definitely a smarter way of doing this, I just can't think of it at the moment
        foreach (var product in products)
        {
            _mockProductRepository.Setup(x => x.GetProductByName(product.Name)).Returns(product);
        }
    }

    [Fact]
    public void Constructor_EnsureNotNullAndCorrectExceptionParameterName()
    {
        var assertion = new GuardClauseAssertion(new Fixture().Customize(new AutoMoqCustomization()));
        assertion.Verify(typeof(ShoppingCartService).GetConstructors());
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void CalculateTotalCostOfShoppingCart_CalledWithEmptyString_ReturnsDefaultSummary(string shoppingCart)
    {
        var result = _shoppingCartService.CalculateTotalCostOfShoppingCart(shoppingCart);

        result.Should().Be("Total price 0, Tax 0");
    }

    [Theory]
    [InlineData("3 Tomato, 1 Chicken, 2 Bread", "Total price 9.36, Tax 1.42")]
    [InlineData("2 Jam, 5 Lettuce, 3 Chicken", "Total price 20.55, Tax 3.14")]
    [InlineData("1 Lettuce, 1 Chicken", "Total price 3.55, Tax 0.60")]
    public void CalculateTotalCostOfShoppingCart_CalledWithValidShoppingCart_CalculatesValuesCorrectly(string shoppingCart, string expectedResult)
    {
        var result = _shoppingCartService.CalculateTotalCostOfShoppingCart(shoppingCart);

        result.Should().Be(expectedResult);
    }
}
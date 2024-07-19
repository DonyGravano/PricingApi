using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IVC.ECommercePricing.API.Controllers;
using IVC.ECommercePricing.Application;
using IVC.ECommercePricing.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace IVC.ECommercePricing.API.UnitTests.ControllerTests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _controller = new ProductController(_mockProductRepository.Object);
        }

        [Fact]
        public void GetUnitPrice_ShouldReturnOkResult_WhenProductExists()
        {
            // Arrange
            var itemName = "Lettuce";
            var product = new Product(itemName, 0.32m, 0.15m, 0.20m);
            _mockProductRepository
                .Setup(repo => repo.GetProductByName(itemName))
                .Returns(product);

            // Act
            var result = _controller.GetUnitPrice(itemName) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var unitPrice = (decimal)result.Value;
            Assert.Equal(0.45m, unitPrice);
        }

        [Fact]
        public void GetUnitPrice_ShouldReturnBadRequest_WhenProductDoesNotExist()
        {
            // Arrange
            var itemName = "NonExistentProduct";
            _mockProductRepository
                .Setup(repo => repo.GetProductByName(itemName))
                .Returns((Product)null);

            // Act
            var result = _controller.GetUnitPrice(itemName) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal($"No item was found with name of {itemName}", result.Value);
        }

        [Fact]
        public void GetUnitPrice_ShouldThrowArgumentException_WhenItemNameIsNull()
        {
            // Arrange
            string itemName = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _controller.GetUnitPrice(itemName));
            Assert.Equal("Value cannot be null or whitespace. (Parameter 'itemName')", exception.Message);
        }

        [Fact]
        public void GetUnitPrice_ShouldThrowArgumentException_WhenItemNameIsWhiteSpace()
        {
            // Arrange
            var itemName = " ";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _controller.GetUnitPrice(itemName));
            Assert.Equal("Value cannot be null or whitespace. (Parameter 'itemName')", exception.Message);
        }
    }
}

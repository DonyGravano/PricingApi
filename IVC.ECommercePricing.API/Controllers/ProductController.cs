using IVC.ECommercePricing.Application;
using Microsoft.AspNetCore.Mvc;

namespace IVC.ECommercePricing.API.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        [HttpGet("unit-price/{itemName}")]
        public IActionResult GetUnitPrice(string itemName)
        {
            if (string.IsNullOrWhiteSpace(itemName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(itemName));

            var product = _productRepository.GetProductByName(itemName);
            if (product == null)
            {
                return BadRequest($"No item was found with name of {itemName}");
            }
            var unitPrice = product.CalculatePricePerUnit();
            return Ok(unitPrice);
        }
    }

}

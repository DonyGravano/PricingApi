using IVC.ECommercePricing.Application;
using Microsoft.AspNetCore.Mvc;

namespace IVC.ECommercePricing.API.Controllers;

[Route("[controller]")]
public class ShoppingCartController : Controller
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService ?? throw new ArgumentNullException(nameof(shoppingCartService));
    }

    [HttpGet("cart-price")]
    public IActionResult GetCartPrice([FromQuery] string cartItems)
    {
        if (string.IsNullOrWhiteSpace(cartItems))
            return BadRequest("No items in shopping cart");
        var summary = _shoppingCartService.CalculateTotalCostOfShoppingCart(cartItems);
        if (summary == null)
        {
            return BadRequest($"Invalid formatted shopping cart: {cartItems}");
        }
        return Ok(summary);
    }
}
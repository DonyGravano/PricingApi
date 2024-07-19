namespace IVC.ECommercePricing.Application;

public interface IShoppingCartService
{
    string? CalculateTotalCostOfShoppingCart(string shoppingCart);
}
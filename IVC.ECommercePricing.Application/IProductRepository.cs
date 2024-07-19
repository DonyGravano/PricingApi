using IVC.ECommercePricing.Application.Models;

namespace IVC.ECommercePricing.Application;

public interface IProductRepository
{
    List<Product> GetProducts();
    Product? GetProductByName(string productName);
}
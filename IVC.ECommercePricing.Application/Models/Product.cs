namespace IVC.ECommercePricing.Application.Models
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public decimal RevenuePercentage { get; set; }
        public decimal TaxPercentage { get; set; }

        public Product(string name, decimal cost, decimal revenuePercentage, decimal taxPercentage)
        {
            Name = name;
            Cost = cost;
            RevenuePercentage = revenuePercentage;
            TaxPercentage = taxPercentage;
        }

        public decimal CalculatePricePerUnit()
        {
            var pricePerUnit = Cost * (1 + RevenuePercentage) * (1 + TaxPercentage);

            var multiplier = (decimal)Math.Pow(10, Convert.ToDouble(2));
            return Math.Ceiling(pricePerUnit * multiplier) / multiplier;
        }

        public decimal CalculateTaxAmount()
        {
            decimal preTaxPrice = Cost * (1 + RevenuePercentage);
            decimal taxPrice = preTaxPrice * TaxPercentage;
            return Math.Ceiling(taxPrice * 100) / 100;
        }

        public decimal CalculatePreTaxPrice()
        {
            var multiplier = (decimal)Math.Pow(10, Convert.ToDouble(2));
            var preTaxPrice = Cost * (1 + RevenuePercentage);
            return Math.Ceiling(preTaxPrice * multiplier) / multiplier;
        }
    }
}

namespace Greggs.Products.Models.Interfaces;

public interface ICurrencyConverter
{
    //default: 2 decimals after dot
    decimal ConvertGbpPriceToForeign(decimal priceGbp, string toCurrency, int? roundingPrecision = 2);
}
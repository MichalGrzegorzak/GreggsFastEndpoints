namespace Greggs.Products.Models.Errors;

public class UnknownCurrencyException(string currency) 
    : Exception($"Unknown currency: {currency}")
{
}
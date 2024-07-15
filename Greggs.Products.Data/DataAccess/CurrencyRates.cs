namespace Greggs.Produces.Data.DataAccess;

public static class CurrencyRates
{
    public static IReadOnlyDictionary<string, decimal> Rates = new Dictionary<string, decimal>
    {
        { "GBP", 1.00m },
        { "EUR", 1.11m },
    };
}
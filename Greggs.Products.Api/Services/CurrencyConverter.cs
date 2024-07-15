using System;
using Greggs.Produces.Data.DataAccess;
using Greggs.Products.Models.Errors;
using Greggs.Products.Models.Interfaces;

namespace Greggs.Products.Api.Services;

/// <summary>
/// Converts GBP to foreign currency or throws ex if currency missing
/// </summary>
public class CurrencyConverter : ICurrencyConverter
{
    public decimal ConvertGbpPriceToForeign(decimal priceGbp, string toCurrency, int? roundingPrecision = 2)
    {
        Guard.Against.NullOrEmpty(toCurrency, nameof(toCurrency));
        Guard.Against.NegativeOrZero(priceGbp, nameof(priceGbp));
        
        if (!CurrencyRates.Rates.TryGetValue(toCurrency, out var rate))
            throw new UnknownCurrencyException(toCurrency);

        var result = priceGbp * rate;

        if (roundingPrecision.HasValue)
            result = Math.Round(result, roundingPrecision.Value);
        
        return result;
    }
}
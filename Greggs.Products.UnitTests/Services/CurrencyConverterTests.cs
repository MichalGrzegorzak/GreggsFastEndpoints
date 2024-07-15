using Greggs.Products.Api.Services;

namespace Greggs.Products.UnitTests.Services;

public class CurrencyConverterTests
{
    private readonly CurrencyConverter _sut = new();
    
    [Fact]
    public void ConvertGbpPriceToForeign_to_GBP()
    {
        var result = _sut.ConvertGbpPriceToForeign(100m, "GBP");
        result.Should().Be(100);
    }
    
    [Fact]
    public void ConvertGbpPriceToForeign_to_EUR_without_rounding()
    {
        var result = _sut.ConvertGbpPriceToForeign(111.1m, "EUR", null);
        result.Should().Be(123.321M);
    }
    
    [Fact]
    public void ConvertGbpPriceToForeign_to_EUR_with_rounding_default()
    {
        var result = _sut.ConvertGbpPriceToForeign(111.1m, "EUR");
        result.Should().Be(123.32m);
    }
    
    [Fact]
    public void ConvertGbpPriceToForeign_to_EUR_with_rounding_to_2_decimals()
    {
        var result = _sut.ConvertGbpPriceToForeign(111.1m, "EUR", 2);
        result.Should().Be(123.32m);
    }
    
    [Fact]
    public void ConvertGbpPriceToForeign_to_EUR_with_rounding_to_1_decimals()
    {
        var result = _sut.ConvertGbpPriceToForeign(111.1m, "EUR", 1);
        result.Should().Be(123.3m);
    }
    
    [Fact]
    public void ConvertGbpPriceToForeign_to_EUR_with_rounding_to_0_decimals()
    {
        var result = _sut.ConvertGbpPriceToForeign(111.1m, "EUR", 0);
        result.Should().Be(123);
    }
}
using FluentValidation;

namespace Greggs.Products.Models.Extensions;

public static class ValidationExtensions
{
    private static readonly List<string> ValidCurrencyCodes = new() { "GBP", "EUR" };
    
    public static IRuleBuilderOptions<T, string> IsValidIsoCurrency<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} is required!")
            .Length(3).WithMessage("{PropertyName} must be in ISO format (3 chars)")
            .Must(country => ValidCurrencyCodes.Contains(country))
            .WithMessage("{PropertyName} not supported currency code: {PropertyValue}");
    }
}
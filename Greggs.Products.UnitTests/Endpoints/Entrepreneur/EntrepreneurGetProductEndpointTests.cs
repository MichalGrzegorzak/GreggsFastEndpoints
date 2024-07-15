using System.Linq;
using System.Threading.Tasks;
using Greggs.Products.Api.Endpoints.Entrepreneur.GetProduct;
using Greggs.Products.Api.Endpoints.Fanatic.GetProduct;
using Greggs.Products.UnitTests.Helpers;

namespace Greggs.Products.UnitTests.Endpoints.Entrepreneur;

public class EntrepreneurGetProductEndpointTests(App app) : TestBase<App>
{
    [Fact, Priority(10)]
    public async Task GET_Invalid_Currency_FAILS()
    {
        var (rsp, res) = await app.Client.GETAsync<EntrepreneurGetProductEndpoint, EntrepreneurGetProductRequest, ProblemDetails>(
            new()
            {
                Currency = "EUR1"
            });

        rsp.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        res.Errors.Count().Should().Be(1);
        res.Errors.Select(e => e.Name).Should().Equal("currency");
        res.Errors.Select(e => e.Reason).Should().Equal("Currency must be in ISO format (3 chars)");
    }
    
    [Fact, Priority(20)]
    public async Task GET_Unsupported_Currency_FAILS()
    {
        var (rsp, res) = await app.Client.GETAsync<EntrepreneurGetProductEndpoint, EntrepreneurGetProductRequest, ProblemDetails>(
            new()
            {
                Currency = "CHF"
            });

        rsp.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        res.Errors.Count().Should().Be(1);
        res.Errors.Select(e => e.Name).Should().Equal("currency");
        res.Errors.Select(e => e.Reason).Should().Equal("Currency not supported currency code: CHF");
    }
    
    [Fact, Priority(31)]
    public async Task GET_Products_GBP()
    {
        var (rsp, res) = await app.Client.GETAsync<EntrepreneurGetProductEndpoint, EntrepreneurGetProductRequest, FanaticGetProductResponse>(
            new()
            {
                PageStart = 0,
                PageSize = 3,
                Currency = "GBP"
            });

        rsp.StatusCode.Should().Be(HttpStatusCode.OK);
        res.Success.Should().BeTrue();
        res.Results.Should().NotBeEmpty().And.HaveCount(3);
        res.Results.Select(p => p.Currency).Should().BeEquivalentTo(["GBP", "GBP", "GBP"]);
    }
    
    [Fact, Priority(32)]
    public async Task GET_Products_EUR()
    {
        var (rsp, res) = await app.Client.GETAsync<EntrepreneurGetProductEndpoint, EntrepreneurGetProductRequest, FanaticGetProductResponse>(
            new()
            {
                PageStart = 1,
                PageSize = 2,
                Currency = "EUR"
            });

        rsp.StatusCode.Should().Be(HttpStatusCode.OK);
        res.Success.Should().BeTrue();
        res.Results.Should().NotBeEmpty().And.HaveCount(2);
        res.Results.Select(p => p.Currency).Should().BeEquivalentTo(["EUR", "EUR"]);
        res.Results.Select(p => p.Price).Should().BeEquivalentTo((new[] { 1.11m, 1.22m }).AsEnumerable());
    }
}
using System.Linq;
using System.Threading.Tasks;
using Greggs.Products.Api.Endpoints.Fanatic.GetProduct;
using Greggs.Products.UnitTests.Helpers;

namespace Greggs.Products.UnitTests.Endpoints.Fanatic;

public class FanaticGetProductEndpointTests(App app) : TestBase<App>
{
    [Fact, Priority(10)]
    public async Task GET_Products()
    {
        var (rsp, res) = await app.Client.GETAsync<FanaticGetProductEndpoint, FanaticGetProductRequest, FanaticGetProductResponse>(
            new()
            {
                PageStart = 0,
                PageSize = 3,
            });

        rsp.StatusCode.Should().Be(HttpStatusCode.OK);
        res.Success.Should().BeTrue();
        res.Results.Should().NotBeEmpty().And.HaveCount(3);
        res.Results.Select(p => p.Currency).Should().BeEquivalentTo(["GBP", "GBP", "GBP"]);
    }
    
    [Fact, Priority(20)]
    public async Task GET_Products_In_Descending_Order()
    {
        var (rsp, res) = await app.Client.GETAsync<FanaticGetProductEndpoint, FanaticGetProductRequest, FanaticGetProductResponse>(
            new()
            {
                PageStart = 0,
                PageSize = 99,
            });

        rsp.StatusCode.Should().Be(HttpStatusCode.OK);
        res.Success.Should().BeTrue();
        res.Results.Should().NotBeEmpty().And.HaveCount(8);
        
        var createdResults = res.Results.Select(p => p.Created).ToList();
        
        var orderedResults = createdResults.OrderDescending().ToList();
        orderedResults.Should().Equal(createdResults); //checks the order
    }
}
using Greggs.Products.Api.Endpoints.Base;
using Greggs.Products.Models.Dto;

namespace Greggs.Products.Api.Endpoints.Fanatic.GetProduct;

public class FanaticGetProductResponse : ResponseBase<ProductDto>
{
    public FanaticGetProductResponse()
    {
    }
    public FanaticGetProductResponse(RequestBase req) : base(req)
    {
    }
}
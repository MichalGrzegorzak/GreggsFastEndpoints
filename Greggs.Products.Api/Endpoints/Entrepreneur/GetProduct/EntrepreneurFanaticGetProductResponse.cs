using Greggs.Products.Api.Endpoints.Base;
using Greggs.Products.Models.Dto;

namespace Greggs.Products.Api.Endpoints.Entrepreneur.GetProduct;

public class EntrepreneurFanaticGetProductResponse : ResponseBase<ProductDto>
{
    public EntrepreneurFanaticGetProductResponse()
    {
    }
    public EntrepreneurFanaticGetProductResponse(RequestBase req) : base(req)
    {
    }
}
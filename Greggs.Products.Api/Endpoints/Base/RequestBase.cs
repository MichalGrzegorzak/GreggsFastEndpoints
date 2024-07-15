using System.Text.Json.Serialization;
using Greggs.Products.Api.Endpoints.Entrepreneur.GetProduct;
using Greggs.Products.Api.Endpoints.Fanatic.GetProduct;

namespace Greggs.Products.Api.Endpoints.Base;

// MARKER for FastEndpoints requests

//for issue with polymorphic deserialization with System.Text.Json
[JsonDerivedType(typeof(EntrepreneurGetProductRequest), "entrepreneurGetProductRequest")]
[JsonDerivedType(typeof(FanaticGetProductRequest), "getProductRequest")]
public abstract class RequestBase
{
}
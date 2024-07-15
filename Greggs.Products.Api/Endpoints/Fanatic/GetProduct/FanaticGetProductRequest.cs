using Greggs.Products.Api.Endpoints.Base;

namespace Greggs.Products.Api.Endpoints.Fanatic.GetProduct;

public class FanaticGetProductRequest : RequestBase
 {
     public int? PageStart { get; set; }
     public int? PageSize { get; set; }
 }
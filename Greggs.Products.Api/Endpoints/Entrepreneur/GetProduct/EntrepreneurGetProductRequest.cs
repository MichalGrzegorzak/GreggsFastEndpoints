using System.ComponentModel;
using Greggs.Products.Api.Endpoints.Base;

namespace Greggs.Products.Api.Endpoints.Entrepreneur.GetProduct;

public class EntrepreneurGetProductRequest : RequestBase
{
    public int? PageStart { get; set; }
    
    //[DefaultValue(10)]
    public int? PageSize { get; set; }
    
    [DefaultValue("EUR")]
    public string Currency { get; set; }
}
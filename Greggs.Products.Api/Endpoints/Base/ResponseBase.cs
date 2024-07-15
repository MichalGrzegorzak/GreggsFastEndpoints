namespace Greggs.Products.Api.Endpoints.Base;

public class ResponseBase<T> where T : class
{
    public bool Success { get; set; }
    public RequestBase Request { get; set; }

    public IList<T> Results { get; set; } = new List<T>();

    public ResponseBase()
    {
    }
    
    /// <summary>
    /// Use if you want to return request details back
    /// </summary>
    public ResponseBase(RequestBase req)
    {
        Request = req;
    }
}
using System.Diagnostics;

namespace Greggs.Products.Api.Endpoints.Processors;

public class StateBag
{
    private readonly Stopwatch _sw = new();

    public string EndpointName { get; set; }
    public long DurationMillis => _sw.ElapsedMilliseconds+100; //+offset init time

    public StateBag()
    {
        _sw.Start();
    }
}
using RDSolutions.Common.Fake.WithConstructor.Constructor;

namespace RDSolutions.Common.Fake;

internal class ExampleClass
{
    private readonly IConfiguration _conf;
    private readonly ICacheConfiguration _cache;

    public ExampleClass(IConfiguration conf, ICacheConfiguration cache)
    {
        _conf = conf;
        _cache = cache;
    }
}

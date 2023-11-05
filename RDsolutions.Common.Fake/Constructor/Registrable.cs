using Microsoft.Extensions.DependencyInjection;
using RDSolutions.Common.Fake.WithConstructor.Constructor;

namespace RDSolutions.Common.Fake.Constructor;

public class Registrable : IRegistrable
{
    private readonly ICacheConfiguration _conf;

    public Registrable(ICacheConfiguration conf)
    {
        _conf = conf;
    }

    public void Register(IServiceCollection services)
    {
        services.AddScoped<ICacheConfiguration, CacheConfiguration>();
    }
}

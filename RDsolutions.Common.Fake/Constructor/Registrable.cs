using Microsoft.Extensions.DependencyInjection;
using RDSolutions.Common.Fake.WithConstructor.Constructor;
using System;

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
        Console.WriteLine(_conf.MyProperty);
        services.AddScoped<ICacheConfiguration, CacheConfiguration>();
    }
}

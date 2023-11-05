using Microsoft.Extensions.DependencyInjection;

namespace RDSolutions.Common.Fake.WithoutConstructor.NoConstructor;

public class Registrable : IRegistrable
{
    public void Register(IServiceCollection services)
    {
        services.AddScoped<IConfiguration, Configuration>();
    }
}

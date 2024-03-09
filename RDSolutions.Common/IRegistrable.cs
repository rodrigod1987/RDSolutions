using Microsoft.Extensions.DependencyInjection;

namespace RDSolutions.Common;

public interface IRegistrable
{
    void Register(IServiceCollection services);
}

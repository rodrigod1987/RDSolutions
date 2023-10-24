using Microsoft.Extensions.DependencyInjection;
using RDSolutions.Common;
using RDSolutions.Common.Fake;
using System;

namespace RDsolutions.Common.Fake
{
    public class Registrable : IRegistrable
    {
        private readonly Configuration _conf;

        public Registrable(Configuration conf)
        {
            _conf = conf;
        }

        public void Register(IServiceCollection services)
        {
            if (_conf.MyProperty == 10)
            {
                services.AddScoped(typeof(ExampleClass));
            }
        }
    }
}

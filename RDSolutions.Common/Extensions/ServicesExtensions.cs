using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RDSolutions.Common.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection RegisterComponents(this IServiceCollection services, string namespaceStartWith)
        {
            //Acessar todas as dlls procurando por aquelas que implementam IRegistrable e chamar o m�todo Register passando 
            //o argumento services. Com isso consigo isolar em cada um dos m�dulos aquilo que eu devo injetar como depend�ncia.
            var assemblies = Assembly
                .GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Where(x => x.Name.StartsWith(namespaceStartWith));

            foreach (var assemblyName in assemblies)
            {
                Assembly assembly = Assembly.Load(assemblyName);

                var myTypes = assembly
                    .GetTypes()
                    .Where(mytype => mytype.GetInterfaces().Contains(typeof(IRegistrable)));

                foreach (Type registrable in myTypes)
                {
                    if (registrable.GetInterface(nameof(IRegistrable)) != null)
                    {
                        var myClass = Activator.CreateInstance(registrable) as IRegistrable;
                        myClass.Register(services);
                    }
                }
            }

            return services;
        }
    }
}

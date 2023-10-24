using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RDSolutions.Common.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection RegisterComponents(this IServiceCollection services, string namespaceStartWith)
        {
            //Acessar todas as dlls procurando por aquelas que implementam IRegistrable e chamar o m�todo Register passando 
            //o argumento services. Com isso consigo isolar em cada um dos m�dulos aquilo que eu devo injetar como depend�ncia.
            var assemblies = Assembly
                .GetCallingAssembly()
                .GetReferencedAssemblies()
                .Where(x => x.Name.StartsWith(namespaceStartWith))
                .ToList();

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
                        var constructors = registrable.GetConstructors();
                        if (constructors.Any())
                        {
                            foreach (var constructor in constructors)
                            {
                                List<object> parameters = new List<object>();
                                var args = constructor.GetParameters();

                                foreach (var arg in args)
                                {
                                    var argType = arg.ParameterType;
                                    parameters.Add(services.Where(s => s.ServiceType.Equals(argType)).FirstOrDefault().ImplementationInstance);
                                }

                                var myClass = Activator.CreateInstance(registrable, parameters.ToArray()) as IRegistrable;
                                myClass.Register(services);
                            }
                        }
                    }
                }
            }

            return services;
        }
    }
}

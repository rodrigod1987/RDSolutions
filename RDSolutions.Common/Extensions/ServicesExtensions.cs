using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace RDSolutions.Common.Extensions;

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
                    try
                    {
                        var myClass = Activator.CreateInstance(registrable) as IRegistrable;
                        myClass.Register(services);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(string.Format("The type {0} should not have a parametrized constructor.", registrable.FullName));
                        throw;
                    }
                }
            }
        }
            
        return services;
    }
}

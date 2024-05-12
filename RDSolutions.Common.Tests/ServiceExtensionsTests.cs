using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDSolutions.Common.Extensions;
using WithoutConstructor = RDSolutions.Common.Fake.WithoutConstructor.NoConstructor;
using WithConstructor = RDSolutions.Common.Fake.WithConstructor.Constructor;
using System.Linq;
using System;
using Shouldly;

namespace RDSolutions.Common.Tests;

[TestClass]
public class ServiceExtensionsTests
{
    [TestMethod]
    public void ServiceExtensionsTestWithoutConstructor()
    {
        var serviceCollection = new ServiceCollection() as IServiceCollection;
        var configuration = new WithoutConstructor.Configuration { MyProperty = 10 };
        var cacheConfiguration = new WithoutConstructor.CacheConfiguration { MyProperty = 10 };

        serviceCollection.AddSingleton(configuration);
        serviceCollection.AddSingleton(cacheConfiguration);
        serviceCollection.RegisterComponents("RDSolutions.Common.Fake.WithoutConstructor");

        serviceCollection.Count.ShouldBe(3);
    }

    [TestMethod]
    public void ServiceExtensionsTestWithConstructor()
    {
        var serviceCollection = new ServiceCollection() as IServiceCollection;
        var configuration = new WithConstructor.Configuration { MyProperty = 10 };
        serviceCollection.AddSingleton(configuration);

        Should.Throw<Exception>(() => serviceCollection.RegisterComponents("RDSolutions.Common.Fake.WithConstructor"));
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDSolutions.Common.Extensions;
using RDSolutions.Common.Fake;
using System;
using System.Linq;

namespace RDSolutions.Common.Tests
{
    [TestClass]
    public class ServiceExtensionsTests
    {
        [TestMethod]
        public void ServiceExtensionsTest()
        {
            var serviceCollection = new ServiceCollection() as IServiceCollection;
            var configuration = new Configuration { MyProperty = 10 };

            serviceCollection.AddSingleton(configuration);
            serviceCollection.RegisterComponents("RDSolutions");

            Assert.AreEqual(2, serviceCollection.Count());
        }
    }
}

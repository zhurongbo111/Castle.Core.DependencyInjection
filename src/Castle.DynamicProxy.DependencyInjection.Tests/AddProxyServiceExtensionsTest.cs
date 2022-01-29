
using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    [TestClass]
    public class AddProxyServiceExtensionsTest
    {
        [TestMethod]
        [DataRow(0, DisplayName = "When interface is registered as transient type parameter, the interceptor should be called")]
        [DataRow(1, DisplayName = "When interface is registered as transient generic type, the interceptor should be called")]
        [DataRow(2, DisplayName = "When interface is registered as transient type parameter and factory, the interceptor should be called")]
        [DataRow(3, DisplayName = "When interface is registered as transient generic type and factory, the interceptor should be called")]
        public void AddTransientInterfaceProxyServiceWithInterceptor(int serviceRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor<AddProxyServiceTestInterceptor>();
            };

            switch (serviceRegisterType)
            {
                case 0:
                    services.AddTransientProxyService(typeof(IAddProxyServiceTest), typeof(AddProxyServiceTest), setup);
                    break;
                case 1:
                    services.AddTransientProxyService<IAddProxyServiceTest, AddProxyServiceTest>(setup);
                    break;
                case 2:
                    services.AddTransientProxyService(typeof(IAddProxyServiceTest), sp => new AddProxyServiceTest(), setup);
                    break;
                case 3:
                    services.AddTransientProxyService<IAddProxyServiceTest>(sp => new AddProxyServiceTest(), setup);
                    break;
            }

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IAddProxyServiceTest>().Say();

            Assert.AreEqual(1, AddProxyServiceTestInterceptor.CalledCount);
        }

        [TestMethod]
        [DataRow(0, DisplayName = "When interface is registered as scoped type parameter, the interceptor should be called")]
        [DataRow(1, DisplayName = "When interface is registered as scoped generic type, the interceptor should be called")]
        [DataRow(2, DisplayName = "When interface is registered as scoped type parameter and factory, the interceptor should be called")]
        [DataRow(3, DisplayName = "When interface is registered as scoped generic type and factory, the interceptor should be called")]
        public void AddScopedInterfaceProxyServiceWithInterceptor(int serviceRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor<AddProxyServiceTestInterceptor>();
            };

            switch (serviceRegisterType)
            {
                case 0:
                    services.AddScopedProxyService(typeof(IAddProxyServiceTest), typeof(AddProxyServiceTest), setup);
                    break;
                case 1:
                    services.AddScopedProxyService<IAddProxyServiceTest, AddProxyServiceTest>(setup);
                    break;
                case 2:
                    services.AddScopedProxyService(typeof(IAddProxyServiceTest), sp => new AddProxyServiceTest(), setup);
                    break;
                case 3:
                    services.AddScopedProxyService<IAddProxyServiceTest>(sp => new AddProxyServiceTest(), setup);
                    break;
            }

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IAddProxyServiceTest>().Say();

            Assert.AreEqual(1, AddProxyServiceTestInterceptor.CalledCount);
        }

        [TestMethod]
        [DataRow(0, DisplayName = "When class is registered as singleton type parameter, the interceptor should be called")]
        [DataRow(1, DisplayName = "When class is registered as singleton generic type, the interceptor should be called")]
        [DataRow(2, DisplayName = "When class is registered as singleton type parameter and factory, the interceptor should be called")]
        [DataRow(3, DisplayName = "When class is registered as singleton generic type and factory, the interceptor should be called")]
        [DataRow(4, DisplayName = "When class is registered as singleton type parameter and instance, the interceptor should be called")]
        [DataRow(5, DisplayName = "When class is registered as singleton generic type and instance, the interceptor should be called")]
        public void AddSingletonInterfaceProxyServiceWithInterceptor(int serviceRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor<AddProxyServiceTestInterceptor>();
            };

            switch (serviceRegisterType)
            {
                case 0:
                    services.AddSingletonProxyService(typeof(IAddProxyServiceTest), typeof(AddProxyServiceTest), setup);
                    break;
                case 1:
                    services.AddSingletonProxyService<IAddProxyServiceTest, AddProxyServiceTest>(setup);
                    break;
                case 2:
                    services.AddSingletonProxyService(typeof(IAddProxyServiceTest), sp => new AddProxyServiceTest(), setup);
                    break;
                case 3:
                    services.AddSingletonProxyService<IAddProxyServiceTest>(sp => new AddProxyServiceTest(), setup);
                    break;
                case 4:
                    services.AddSingletonProxyService(typeof(IAddProxyServiceTest), new AddProxyServiceTest(), setup);
                    break;
                case 5:
                    services.AddSingletonProxyService<IAddProxyServiceTest>(new AddProxyServiceTest(), setup);
                    break;
            }

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IAddProxyServiceTest>().Say();

            Assert.AreEqual(1, AddProxyServiceTestInterceptor.CalledCount);
        }

        [TestMethod]
        [DataRow(0, DisplayName = "When class is registered as transient type parameter, the interceptor should be called")]
        [DataRow(1, DisplayName = "When class is registered as transient generic type, the interceptor should be called")]
        [DataRow(2, DisplayName = "When class is registered as transient type parameter and factory, the interceptor should be called")]
        [DataRow(3, DisplayName = "When class is registered as transient generic type and factory, the interceptor should be called")]
        [DataRow(4, DisplayName = "When class is registered as transient only one type parameter, the interceptor should be called")]
        [DataRow(5, DisplayName = "When class is registered as transient only one generic type, the interceptor should be called")]
        public void AddTransientClassProxyServiceWithInterceptor(int serviceRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor<AddProxyServiceTestInterceptor>();
            };

            switch (serviceRegisterType)
            {
                case 0:
                    services.AddTransientProxyService(typeof(AddProxyServiceTest), typeof(AddProxyServiceTest), setup);
                    break;
                case 1:
                    services.AddTransientProxyService<AddProxyServiceTest, AddProxyServiceTest>(setup);
                    break;
                case 2:
                    services.AddTransientProxyService(typeof(AddProxyServiceTest), sp => new AddProxyServiceTest(), setup);
                    break;
                case 3:
                    services.AddTransientProxyService<AddProxyServiceTest>(sp => new AddProxyServiceTest(), setup);
                    break;
                case 4:
                    services.AddTransientProxyService<AddProxyServiceTest>(setup);
                    break;
                case 5:
                    services.AddTransientProxyService(typeof(AddProxyServiceTest), setup);
                    break;
            }

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<AddProxyServiceTest>().Say();

            Assert.AreEqual(1, AddProxyServiceTestInterceptor.CalledCount);
        }

        [TestMethod]
        [DataRow(0, DisplayName = "When class is registered as scoped type parameter, the interceptor should be called")]
        [DataRow(1, DisplayName = "When class is registered as scoped generic type, the interceptor should be called")]
        [DataRow(2, DisplayName = "When class is registered as scoped type parameter and factory, the interceptor should be called")]
        [DataRow(3, DisplayName = "When class is registered as scoped generic type and factory, the interceptor should be called")]
        [DataRow(4, DisplayName = "When class is registered as scoped only one type parameter, the interceptor should be called")]
        [DataRow(5, DisplayName = "When class is registered as scoped only one generic type, the interceptor should be called")]
        public void AddScopedClassProxyServiceWithInterceptor(int serviceRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor<AddProxyServiceTestInterceptor>();
            };

            switch (serviceRegisterType)
            {
                case 0:
                    services.AddScopedProxyService(typeof(AddProxyServiceTest), typeof(AddProxyServiceTest), setup);
                    break;
                case 1:
                    services.AddScopedProxyService<AddProxyServiceTest, AddProxyServiceTest>(setup);
                    break;
                case 2:
                    services.AddScopedProxyService(typeof(AddProxyServiceTest), sp => new AddProxyServiceTest(), setup);
                    break;
                case 3:
                    services.AddScopedProxyService<AddProxyServiceTest>(sp => new AddProxyServiceTest(), setup);
                    break;
                case 4:
                    services.AddScopedProxyService<AddProxyServiceTest>(setup);
                    break;
                case 5:
                    services.AddScopedProxyService(typeof(AddProxyServiceTest), setup);
                    break;
            }

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<AddProxyServiceTest>().Say();

            Assert.AreEqual(1, AddProxyServiceTestInterceptor.CalledCount);
        }

        [TestMethod]
        [DataRow(0, DisplayName = "When class is registered as singleton type parameter, the interceptor should be called")]
        [DataRow(1, DisplayName = "When class is registered as singleton generic type, the interceptor should be called")]
        [DataRow(2, DisplayName = "When class is registered as singleton type parameter and factory, the interceptor should be called")]
        [DataRow(3, DisplayName = "When class is registered as singleton generic type and factory, the interceptor should be called")]
        [DataRow(4, DisplayName = "When class is registered as singleton type parameter and instance, the interceptor should be called")]
        [DataRow(5, DisplayName = "When class is registered as singleton generic type and instance, the interceptor should be called")]
        [DataRow(6, DisplayName = "When class is registered as singleton only one type parameter type, the interceptor should be called")]
        [DataRow(7, DisplayName = "When class is registered as singleton only one generic type, the interceptor should be called")]
        public void AddSingletonClassProxyServiceWithInterceptor(int serviceRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor<AddProxyServiceTestInterceptor>();
            };

            switch (serviceRegisterType)
            {
                case 0:
                    services.AddSingletonProxyService(typeof(AddProxyServiceTest), typeof(AddProxyServiceTest), setup);
                    break;
                case 1:
                    services.AddSingletonProxyService<AddProxyServiceTest, AddProxyServiceTest>(setup);
                    break;
                case 2:
                    services.AddSingletonProxyService(typeof(AddProxyServiceTest), sp => new AddProxyServiceTest(), setup);
                    break;
                case 3:
                    services.AddSingletonProxyService<AddProxyServiceTest>(sp => new AddProxyServiceTest(), setup);
                    break;
                case 4:
                    services.AddSingletonProxyService(typeof(AddProxyServiceTest), new AddProxyServiceTest(), setup);
                    break;
                case 5:
                    services.AddSingletonProxyService<AddProxyServiceTest>(new AddProxyServiceTest(), setup);
                    break;
                case 6:
                    services.AddSingletonProxyService<AddProxyServiceTest>(setup);
                    break;
                case 7:
                    services.AddSingletonProxyService(typeof(AddProxyServiceTest), setup);
                    break;
            }

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<AddProxyServiceTest>().Say();

            Assert.AreEqual(1, AddProxyServiceTestInterceptor.CalledCount);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            AddProxyServiceTestInterceptor.CalledCount = 0;
        }
    }
}

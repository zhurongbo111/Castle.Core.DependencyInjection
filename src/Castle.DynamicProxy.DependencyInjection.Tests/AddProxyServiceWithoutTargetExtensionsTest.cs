using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    [TestClass]
    public class AddProxyServiceWithoutTargetExtensionsTest
    {
        [TestMethod]
        [DataRow(ServiceLifetime.Singleton)]
        [DataRow(ServiceLifetime.Scoped)]
        [DataRow(ServiceLifetime.Transient)]
        public void AddProxyServiceWithoutTargetTypeParameterTest(ServiceLifetime serviceLifetime)
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor<AddProxyServiceWithoutTargetTestInterceptor>();
            };
            services.AddProxyServiceWithoutTarget(typeof(IAddProxyServiceTest), serviceLifetime, setup);

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IAddProxyServiceTest>().Say();

            Assert.AreEqual(1, AddProxyServiceWithoutTargetTestInterceptor.CalledCount);
        }

        [TestMethod]
        public void AddProxyServiceWithoutTargetGenericTypeTest()
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor<AddProxyServiceWithoutTargetTestInterceptor>();
            };
            services.AddProxyServiceWithoutTarget<IAddProxyServiceTest>(ServiceLifetime.Transient, setup);

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IAddProxyServiceTest>().Say();

            Assert.AreEqual(1, AddProxyServiceWithoutTargetTestInterceptor.CalledCount);
        }
        public void AddTransientProxyServiceWithoutTargetTypeParameterTest()
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor<AddProxyServiceWithoutTargetTestInterceptor>();
            };
            services.AddTransientProxyServiceWithoutTarget(typeof(IAddProxyServiceTest), setup);

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IAddProxyServiceTest>().Say();

            Assert.AreEqual(1, AddProxyServiceWithoutTargetTestInterceptor.CalledCount);
        }

        [TestMethod]
        public void AddTransientProxyServiceWithoutTargetGenericTypeTest()
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor<AddProxyServiceWithoutTargetTestInterceptor>();
            };
            services.AddTransientProxyServiceWithoutTarget<IAddProxyServiceTest>(setup);

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IAddProxyServiceTest>().Say();

            Assert.AreEqual(1, AddProxyServiceWithoutTargetTestInterceptor.CalledCount);
        }

        public void AddScopedProxyServiceWithoutTargetTypeParameterTest()
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor<AddProxyServiceWithoutTargetTestInterceptor>();
            };
            services.AddScopedProxyServiceWithoutTarget(typeof(IAddProxyServiceTest), setup);

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IAddProxyServiceTest>().Say();

            Assert.AreEqual(1, AddProxyServiceWithoutTargetTestInterceptor.CalledCount);
        }

        [TestMethod]
        public void AddScopedProxyServiceWithoutTargetGenericTypeTest()
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor<AddProxyServiceWithoutTargetTestInterceptor>();
            };
            services.AddScopedProxyServiceWithoutTarget<IAddProxyServiceTest>(setup);

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IAddProxyServiceTest>().Say();

            Assert.AreEqual(1, AddProxyServiceWithoutTargetTestInterceptor.CalledCount);
        }

        public void AddSingletonProxyServiceWithoutTargetTypeParameterTest()
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor<AddProxyServiceWithoutTargetTestInterceptor>();
            };
            services.AddSingletonProxyServiceWithoutTarget(typeof(IAddProxyServiceTest), setup);

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IAddProxyServiceTest>().Say();

            Assert.AreEqual(1, AddProxyServiceWithoutTargetTestInterceptor.CalledCount);
        }

        [TestMethod]
        public void AddSingletonProxyServiceWithoutTargetGenericTypeTest()
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor<AddProxyServiceWithoutTargetTestInterceptor>();
            };
            services.AddSingletonProxyServiceWithoutTarget<IAddProxyServiceTest>(setup);

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IAddProxyServiceTest>().Say();

            Assert.AreEqual(1, AddProxyServiceWithoutTargetTestInterceptor.CalledCount);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            AddProxyServiceWithoutTargetTestInterceptor.CalledCount = 0;
        }
    }
}

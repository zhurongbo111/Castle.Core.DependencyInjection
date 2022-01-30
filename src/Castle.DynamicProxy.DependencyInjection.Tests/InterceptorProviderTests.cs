
using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    [TestClass]
    public class InterceptorProviderTests
    {
        [TestMethod]
        public void RegisterInterceptorWithTypeShouldBeWork()
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor<InterceptorProviderTestInterceptor>();
            };

            services.AddTransientProxyService<IInterceptorProviderTest, InterceptorProviderTest>(setup);

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IInterceptorProviderTest>().Say();

            Assert.AreEqual(1, InterceptorProviderTestInterceptor.CalledCount);
        }

        [TestMethod]
        public void RegisterInterceptorWithInstanceShouldBeWork()
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor(new InterceptorProviderTestInterceptor());
            };

            services.AddTransientProxyService<IInterceptorProviderTest, InterceptorProviderTest>(setup);

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IInterceptorProviderTest>().Say();

            Assert.AreEqual(1, InterceptorProviderTestInterceptor.CalledCount);
        }

        [TestMethod]
        public void RegisterInterceptorWithFactoryShouldBeWork()
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor(sp => new InterceptorProviderTestInterceptor());
            };

            services.AddTransientProxyService<IInterceptorProviderTest, InterceptorProviderTest>(setup);

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IInterceptorProviderTest>().Say();

            Assert.AreEqual(1, InterceptorProviderTestInterceptor.CalledCount);
        }

        public void RegisterAsyncInterceptorWithTypeShouldBeWork()
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithAsyncInterceptor<InterceptorProviderTestAsyncInterceptor>();
            };

            services.AddTransientProxyService<IInterceptorProviderTest, InterceptorProviderTest>(setup);

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IInterceptorProviderTest>().Say();

            Assert.AreEqual(1, InterceptorProviderTestAsyncInterceptor.CalledCount);
        }

        [TestMethod]
        public void RegisterAsyncInterceptorWithInstanceShouldBeWork()
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithAsyncInterceptor(new InterceptorProviderTestAsyncInterceptor());
            };

            services.AddTransientProxyService<IInterceptorProviderTest, InterceptorProviderTest>(setup);

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IInterceptorProviderTest>().Say();

            Assert.AreEqual(1, InterceptorProviderTestAsyncInterceptor.CalledCount);
        }

        [TestMethod]
        public void RegisterAsyncInterceptorWithFactoryShouldBeWork()
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithAsyncInterceptor(sp => new InterceptorProviderTestAsyncInterceptor());
            };

            services.AddTransientProxyService<IInterceptorProviderTest, InterceptorProviderTest>(setup);

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IInterceptorProviderTest>().Say();

            Assert.AreEqual(1, InterceptorProviderTestAsyncInterceptor.CalledCount);
        }

        [TestMethod]
        public void WithGenerateOptionShouldBeWork()
        {
            IServiceCollection services = new ServiceCollection();
            Action<IProxyServiceBuilder> setup = builder =>
            {
                builder.WithInterceptor<InterceptorProviderTestInterceptor>();
                builder.WithProxyGenerationOptions(ops => ops.Selector = new InterceptorSelector());
            };

            services.AddTransientProxyService<IInterceptorProviderTest, InterceptorProviderTest>(setup);

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IInterceptorProviderTest>().Say();

            Assert.AreEqual(0, InterceptorProviderTestAsyncInterceptor.CalledCount);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            InterceptorProviderTestInterceptor.CalledCount = 0;
            InterceptorProviderTestAsyncInterceptor.CalledCount = 0;
        }
    }
}


using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    [TestClass]
    public class WithoutTargetTests
    {
        [TestMethod]
        public void InterfaceWithoutTarget()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddProxyServiceWithoutTarget<IWithoutTargetService>(ServiceLifetime.Transient, builder =>
            {
                builder.WithInterceptor<WithoutTargetInterceptor>();
            });

            var sp = services.BuildServiceProvider();
            var service = sp.GetRequiredService<IWithoutTargetService>();
            service.Say("");

            Assert.AreEqual(1, WithoutTargetInterceptor.CalledCount);
        }

        [TestMethod]
        public void InterfaceWithoutTargetAsync()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddProxyServiceWithoutTarget<IWithoutTargetService>(ServiceLifetime.Transient, builder =>
            {
                builder.WithAsyncInterceptor<WithoutTargetInterceptorAsync>();
            });

            var sp = services.BuildServiceProvider();
            var service = sp.GetRequiredService<IWithoutTargetService>();
            service.Say("");

            Assert.AreEqual(1, WithoutTargetInterceptorAsync.CalledCount);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            WithoutTargetInterceptor.CalledCount = 0;
            WithoutTargetInterceptorAsync.CalledCount = 0;
        }
    }
}

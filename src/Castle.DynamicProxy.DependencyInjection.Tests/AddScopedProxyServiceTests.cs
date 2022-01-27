
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    [TestClass]
    public class AddScopedProxyServiceTests
    {
        [TestMethod]
        public void ScopedProxyServiceWithType()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IMethodInvokeCounter, MethodInvokeCounter>();
            services.AddScopedProxyService(typeof(IAddScopedProxyService), typeof(AddScopedProxyService), builder =>
            {
                builder.WithInterceptor<AddScopedProxyServiceInterceptor>();
            });

            var sp = services.BuildServiceProvider();
            var service = sp.GetRequiredService<IAddScopedProxyService>();
            service.Say("");

            Assert.AreEqual(1, sp.GetRequiredService<IMethodInvokeCounter>().InvokeCount);
        }

        [TestMethod]
        public void ScopedProxyServiceWithGenericType()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IMethodInvokeCounter, MethodInvokeCounter>();
            services.AddScopedProxyService<IAddScopedProxyService, AddScopedProxyService>(builder =>
            {
                builder.WithInterceptor<AddScopedProxyServiceInterceptor>();
            });

            var sp = services.BuildServiceProvider();
            var service = sp.GetRequiredService<IAddScopedProxyService>();
            service.Say("");

            Assert.AreEqual(1, sp.GetRequiredService<IMethodInvokeCounter>().InvokeCount);
        }

        [TestMethod]
        public void ScopedProxyServiceWithClassType()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IMethodInvokeCounter, MethodInvokeCounter>();
            services.AddScopedProxyService(typeof(AddScopedProxyService), builder =>
            {
                builder.WithInterceptor<AddScopedProxyServiceInterceptor>();
            });

            var sp = services.BuildServiceProvider();
            var service = sp.GetRequiredService<AddScopedProxyService>();
            service.Say("");

            Assert.AreEqual(1, sp.GetRequiredService<IMethodInvokeCounter>().InvokeCount);
        }

        [TestMethod]
        public void ScopedProxyServiceWithClassGenericType()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IMethodInvokeCounter, MethodInvokeCounter>();
            services.AddScopedProxyService<AddScopedProxyService>(builder =>
            {
                builder.WithInterceptor<AddScopedProxyServiceInterceptor>();
            });

            var sp = services.BuildServiceProvider();
            var service = sp.GetRequiredService<AddScopedProxyService>();
            service.Say("");

            Assert.AreEqual(1, sp.GetRequiredService<IMethodInvokeCounter>().InvokeCount);
        }
    }
}

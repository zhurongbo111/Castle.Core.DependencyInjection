using System;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    public static partial class DynamicProxyServiceCollectionExtensions
    {
        public static IServiceCollection AddTransient(this IServiceCollection services, Type serviceType, Type implementationType, Action<ProxyServiceBuilder> proxySetup)
        {
            return services.AddTransient(serviceType, implementationType).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection services, Action<ProxyServiceBuilder> proxySetup) where TService : class where TImplementation : class, TService
        {
            return services.AddTransient(typeof(TService), typeof(TImplementation), proxySetup);
        }

        public static IServiceCollection AddTransient(this IServiceCollection services, Type serviceType, Action<ProxyServiceBuilder> proxySetup)
        {
            return services.AddTransient(serviceType, serviceType, proxySetup);
        }

        public static IServiceCollection AddTransient<TService>(this IServiceCollection services, Action<ProxyServiceBuilder> proxySetup) where TService : class
        {
            return services.AddTransient(typeof(TService), proxySetup);
        }

        public static IServiceCollection AddTransient(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory, Action<ProxyServiceBuilder> proxySetup)
        {
            return services.AddTransient(serviceType, implementationFactory).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddTransient<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory, Action<ProxyServiceBuilder> proxySetup)
            where TService : class
        {
            return services.AddTransient(typeof(TService), implementationFactory, proxySetup);
        }

        public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<ProxyServiceBuilder> proxySetup)
            where TService : class where TImplementation : class, TService
        {
            return services.AddTransient(typeof(TService), implementationFactory, proxySetup);
        }
    }
}

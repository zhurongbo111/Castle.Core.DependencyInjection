using System;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    public static partial class DynamicProxyServiceCollectionExtensions
    {
        public static IServiceCollection AddScoped(this IServiceCollection services, Type serviceType, Type implementationType, Action<ProxyServiceBuilder> proxySetup)
        {
            return services.AddScoped(serviceType, implementationType).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, Action<ProxyServiceBuilder> proxySetup)
            where TService : class where TImplementation : class, TService
        {
            return services.AddScoped(typeof(TService), typeof(TImplementation), proxySetup);
        }

        public static IServiceCollection AddScoped(this IServiceCollection services, Type serviceType, Action<ProxyServiceBuilder> proxySetup)
        {
            return services.AddScoped(serviceType, serviceType, proxySetup);
        }

        public static IServiceCollection AddScoped<TService>(this IServiceCollection services, Action<ProxyServiceBuilder> proxySetup) where TService : class
        {
            return services.AddScoped(typeof(TService), proxySetup);
        }

        public static IServiceCollection AddScoped(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory, Action<ProxyServiceBuilder> proxySetup)
        {
            return services.AddScoped(serviceType, implementationFactory).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddScoped<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory, Action<ProxyServiceBuilder> proxySetup) where TService : class
        {
            return services.AddScoped(typeof(TService), implementationFactory, proxySetup);
        }

        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<ProxyServiceBuilder> proxySetup)
            where TService : class where TImplementation : class, TService
        {
            return services.AddScoped(typeof(TService), implementationFactory, proxySetup);
        }
    }
}

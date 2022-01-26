using System;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    public static partial class DynamicProxyServiceCollectionExtensions
    {
        public static IServiceCollection AddScopedWithProxy(this IServiceCollection services, Type serviceType, Type implementationType, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddScoped(serviceType, implementationType).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddScopedWithProxy<TService, TImplementation>(this IServiceCollection services, Action<IProxyServiceBuilder> proxySetup)
            where TService : class where TImplementation : class, TService
        {
            return services.AddScopedWithProxy(typeof(TService), typeof(TImplementation), proxySetup);
        }

        public static IServiceCollection AddScopedWithProxy(this IServiceCollection services, Type serviceType, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddScopedWithProxy(serviceType, serviceType, proxySetup);
        }

        public static IServiceCollection AddScopedWithProxy<TService>(this IServiceCollection services, Action<IProxyServiceBuilder> proxySetup) where TService : class
        {
            return services.AddScopedWithProxy(typeof(TService), proxySetup);
        }

        public static IServiceCollection AddScopedWithProxy(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddScoped(serviceType, implementationFactory).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddScopedWithProxy<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory, Action<IProxyServiceBuilder> proxySetup) where TService : class
        {
            return services.AddScopedWithProxy(typeof(TService), implementationFactory, proxySetup);
        }

        public static IServiceCollection AddScopedWithProxy<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<IProxyServiceBuilder> proxySetup)
            where TService : class where TImplementation : class, TService
        {
            return services.AddScopedWithProxy(typeof(TService), implementationFactory, proxySetup);
        }
    }
}

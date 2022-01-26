using System;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    public static partial class DynamicProxyServiceCollectionExtensions
    {
        public static IServiceCollection AddTransientWithProxy(this IServiceCollection services, Type serviceType, Type implementationType, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddTransient(serviceType, implementationType).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddTransientWithProxy<TService, TImplementation>(this IServiceCollection services, Action<IProxyServiceBuilder> proxySetup) where TService : class where TImplementation : class, TService
        {
            return services.AddTransientWithProxy(typeof(TService), typeof(TImplementation), proxySetup);
        }

        public static IServiceCollection AddTransientWithProxy(this IServiceCollection services, Type serviceType, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddTransientWithProxy(serviceType, serviceType, proxySetup);
        }

        public static IServiceCollection AddTransientWithProxy<TService>(this IServiceCollection services, Action<IProxyServiceBuilder> proxySetup) where TService : class
        {
            return services.AddTransientWithProxy(typeof(TService), proxySetup);
        }

        public static IServiceCollection AddTransientWithProxy(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddTransient(serviceType, implementationFactory).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddTransientWithProxy<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory, Action<IProxyServiceBuilder> proxySetup)
            where TService : class
        {
            return services.AddTransientWithProxy(typeof(TService), implementationFactory, proxySetup);
        }

        public static IServiceCollection AddTransientWithProxy<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<IProxyServiceBuilder> proxySetup)
            where TService : class where TImplementation : class, TService
        {
            return services.AddTransientWithProxy(typeof(TService), implementationFactory, proxySetup);
        }
    }
}

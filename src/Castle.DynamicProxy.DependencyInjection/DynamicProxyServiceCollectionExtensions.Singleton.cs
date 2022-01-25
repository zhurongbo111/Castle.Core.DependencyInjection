using System;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    public static partial class DynamicProxyServiceCollectionExtensions
    {
        public static IServiceCollection AddSingletonWithProxy(this IServiceCollection services, Type serviceType, Type implementationType, Action<ProxyServiceBuilder> proxySetup)
        {
            return services.AddSingleton(serviceType, implementationType).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddSingletonWithProxy<TService, TImplementation>(this IServiceCollection services, Action<ProxyServiceBuilder> proxySetup)
            where TService : class where TImplementation : class, TService
        {

            return services.AddSingletonWithProxy(typeof(TService), typeof(TImplementation), proxySetup);
        }

        public static IServiceCollection AddSingletonWithProxy(this IServiceCollection services, Type serviceType, Action<ProxyServiceBuilder> proxySetup)
        {
            return services.AddSingletonWithProxy(serviceType, serviceType, proxySetup);
        }

        public static IServiceCollection AddSingletonWithProxy<TService>(this IServiceCollection services, Action<ProxyServiceBuilder> proxySetup)
            where TService : class
        {
            return services.AddSingletonWithProxy(typeof(TService), proxySetup);
        }

        public static IServiceCollection AddSingletonWithProxy(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory, Action<ProxyServiceBuilder> proxySetup)
        {

            return services.AddSingleton(serviceType, implementationFactory).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddSingletonWithProxy<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory, Action<ProxyServiceBuilder> proxySetup)
            where TService : class
        {
            return services.AddSingletonWithProxy(typeof(TService), implementationFactory, proxySetup);
        }

        public static IServiceCollection AddSingletonWithProxy<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<ProxyServiceBuilder> proxySetup)
            where TService : class where TImplementation : class, TService
        {

            return services.AddSingletonWithProxy(typeof(TService), implementationFactory, proxySetup);
        }

        public static IServiceCollection AddSingletonWithProxy(this IServiceCollection services, Type serviceType, object implementationInstance, Action<ProxyServiceBuilder> proxySetup)
        {
            return services.AddSingleton(serviceType, implementationInstance).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddSingletonWithProxy<TService>(this IServiceCollection services, TService implementationInstance, Action<ProxyServiceBuilder> proxySetup)
            where TService : class
        {
            return services.AddSingletonWithProxy(typeof(TService), implementationInstance, proxySetup);
        }
    }
}

using System;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    public static partial class DynamicProxyServiceCollectionExtensions
    {
        public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType, Type implementationType, Action<ProxyServiceBuilder> proxySetup)
        {
            return services.AddSingleton(serviceType, implementationType).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services, Action<ProxyServiceBuilder> proxySetup)
            where TService : class where TImplementation : class, TService
        {

            return services.AddSingleton(typeof(TService), typeof(TImplementation), proxySetup);
        }

        public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType, Action<ProxyServiceBuilder> proxySetup)
        {
            return services.AddSingleton(serviceType, serviceType, proxySetup);
        }

        public static IServiceCollection AddSingleton<TService>(this IServiceCollection services, Action<ProxyServiceBuilder> proxySetup)
            where TService : class
        {
            return services.AddSingleton(typeof(TService), proxySetup);
        }

        public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory, Action<ProxyServiceBuilder> proxySetup)
        {

            return services.AddSingleton(serviceType, implementationFactory).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddSingleton<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory, Action<ProxyServiceBuilder> proxySetup)
            where TService : class
        {
            return services.AddSingleton(typeof(TService), implementationFactory, proxySetup);
        }

        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<ProxyServiceBuilder> proxySetup)
            where TService : class where TImplementation : class, TService
        {

            return services.AddSingleton(typeof(TService), implementationFactory, proxySetup);
        }

        public static IServiceCollection AddSingleton(this IServiceCollection services, Type serviceType, object implementationInstance, Action<ProxyServiceBuilder> proxySetup)
        {
            return services.AddSingleton(serviceType, implementationInstance).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddSingleton<TService>(this IServiceCollection services, TService implementationInstance, Action<ProxyServiceBuilder> proxySetup)
            where TService : class
        {
            return services.AddSingleton(typeof(TService), implementationInstance, proxySetup);
        }
    }
}

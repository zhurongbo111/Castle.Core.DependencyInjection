using System;

using Microsoft.Extensions.DependencyInjection;

namespace CastleDynamicProxy.DependencyInjection
{
    public static partial class AddProxyServiceExtensions
    {
        public static IServiceCollection AddSingletonProxyService(this IServiceCollection services, Type serviceType, Type implementationType, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddSingleton(serviceType, implementationType).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddSingletonProxyService<TService, TImplementation>(this IServiceCollection services, Action<IProxyServiceBuilder> proxySetup)
            where TService : class where TImplementation : class, TService
        {

            return services.AddSingletonProxyService(typeof(TService), typeof(TImplementation), proxySetup);
        }

        public static IServiceCollection AddSingletonProxyService(this IServiceCollection services, Type serviceType, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddSingletonProxyService(serviceType, serviceType, proxySetup);
        }

        public static IServiceCollection AddSingletonProxyService<TService>(this IServiceCollection services, Action<IProxyServiceBuilder> proxySetup)
            where TService : class
        {
            return services.AddSingletonProxyService(typeof(TService), proxySetup);
        }

        public static IServiceCollection AddSingletonProxyService(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory, Action<IProxyServiceBuilder> proxySetup)
        {

            return services.AddSingleton(serviceType, implementationFactory).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddSingletonProxyService<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory, Action<IProxyServiceBuilder> proxySetup)
            where TService : class
        {
            return services.AddSingletonProxyService(typeof(TService), implementationFactory, proxySetup);
        }

        public static IServiceCollection AddSingletonProxyService(this IServiceCollection services, Type serviceType, object implementationInstance, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddSingleton(serviceType, implementationInstance).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddSingletonProxyService<TService>(this IServiceCollection services, TService implementationInstance, Action<IProxyServiceBuilder> proxySetup)
            where TService : class
        {
            return services.AddSingletonProxyService(typeof(TService), implementationInstance, proxySetup);
        }
    }
}

using System;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    public static partial class AddProxyServiceExtensions
    {
        public static IServiceCollection AddTransientProxyService(this IServiceCollection services, Type serviceType, Type implementationType, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddTransient(serviceType, implementationType).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddTransientProxyService<TService, TImplementation>(this IServiceCollection services, Action<IProxyServiceBuilder> proxySetup) where TService : class where TImplementation : class, TService
        {
            return services.AddTransientProxyService(typeof(TService), typeof(TImplementation), proxySetup);
        }

        public static IServiceCollection AddTransientProxyService(this IServiceCollection services, Type serviceType, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddTransientProxyService(serviceType, serviceType, proxySetup);
        }

        public static IServiceCollection AddTransientProxyService<TService>(this IServiceCollection services, Action<IProxyServiceBuilder> proxySetup) where TService : class
        {
            return services.AddTransientProxyService(typeof(TService), proxySetup);
        }

        public static IServiceCollection AddTransientProxyService(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddTransient(serviceType, implementationFactory).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddTransientProxyService<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory, Action<IProxyServiceBuilder> proxySetup)
            where TService : class
        {
            return services.AddTransientProxyService(typeof(TService), implementationFactory, proxySetup);
        }

        public static IServiceCollection AddTransientProxyService<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<IProxyServiceBuilder> proxySetup)
            where TService : class where TImplementation : class, TService
        {
            return services.AddTransientProxyService(typeof(TService), implementationFactory, proxySetup);
        }
    }
}

using System;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    public static partial class AddProxyServiceExtensions
    {
        public static IServiceCollection AddScopedProxyService(this IServiceCollection services, Type serviceType, Type implementationType, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddScoped(serviceType, implementationType).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddScopedProxyService<TService, TImplementation>(this IServiceCollection services, Action<IProxyServiceBuilder> proxySetup)
            where TService : class where TImplementation : class, TService
        {
            return services.AddScopedProxyService(typeof(TService), typeof(TImplementation), proxySetup);
        }

        public static IServiceCollection AddScopedProxyService(this IServiceCollection services, Type serviceType, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddScopedProxyService(serviceType, serviceType, proxySetup);
        }

        public static IServiceCollection AddScopedProxyService<TService>(this IServiceCollection services, Action<IProxyServiceBuilder> proxySetup) where TService : class
        {
            return services.AddScopedProxyService(typeof(TService), proxySetup);
        }

        public static IServiceCollection AddScopedProxyService(this IServiceCollection services, Type serviceType, Func<IServiceProvider, object> implementationFactory, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddScoped(serviceType, implementationFactory).EnableInterceptor(serviceType, proxySetup);
        }

        public static IServiceCollection AddScopedProxyService<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory, Action<IProxyServiceBuilder> proxySetup) where TService : class
        {
            return services.AddScopedProxyService(typeof(TService), implementationFactory, proxySetup);
        }
    }
}

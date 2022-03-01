using System;

using Castle.DynamicProxy.Generators;

using Microsoft.Extensions.DependencyInjection;

namespace CastleDynamicProxy.DependencyInjection
{
    public static partial class AddProxyServiceExtensions
    {
        public static IServiceCollection AddProxyServiceWithoutTarget(this IServiceCollection services, Type serviceType, ServiceLifetime serviceLifetime, Action<IProxyServiceBuilder> proxySetup)
        {
            if (serviceType.IsGenericTypeDefinition)
            {
                throw new GeneratorException($"Can not create proxy for type {serviceType.FullName} because it is an open generic type.");
            }

            services.AddProxyCore();

            var proxyBuilder = new WithoutTargetProxyServiceBuilder(serviceType, serviceLifetime, services);

            proxySetup.Invoke(proxyBuilder);
            proxyBuilder.Build();

            return services;
        }

        public static IServiceCollection AddProxyServiceWithoutTarget<TService>(this IServiceCollection services, ServiceLifetime serviceLifetime, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddProxyServiceWithoutTarget(typeof(TService), serviceLifetime, proxySetup);
        }

        public static IServiceCollection AddTransientProxyServiceWithoutTarget(this IServiceCollection services, Type serviceType, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddProxyServiceWithoutTarget(serviceType, ServiceLifetime.Transient, proxySetup);
        }

        public static IServiceCollection AddTransientProxyServiceWithoutTarget<TService>(this IServiceCollection services, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddProxyServiceWithoutTarget<TService>(ServiceLifetime.Transient, proxySetup);
        }

        public static IServiceCollection AddScopedProxyServiceWithoutTarget(this IServiceCollection services, Type serviceType, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddProxyServiceWithoutTarget(serviceType, ServiceLifetime.Scoped, proxySetup);
        }

        public static IServiceCollection AddScopedProxyServiceWithoutTarget<TService>(this IServiceCollection services, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddProxyServiceWithoutTarget<TService>(ServiceLifetime.Scoped, proxySetup);
        }

        public static IServiceCollection AddSingletonProxyServiceWithoutTarget(this IServiceCollection services, Type serviceType, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddProxyServiceWithoutTarget(serviceType, ServiceLifetime.Singleton, proxySetup);
        }

        public static IServiceCollection AddSingletonProxyServiceWithoutTarget<TService>(this IServiceCollection services, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.AddProxyServiceWithoutTarget<TService>(ServiceLifetime.Singleton, proxySetup);
        }
    }
}

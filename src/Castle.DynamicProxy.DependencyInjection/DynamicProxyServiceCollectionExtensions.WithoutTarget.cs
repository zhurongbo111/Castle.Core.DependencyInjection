using System;

using Castle.DynamicProxy.Generators;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    public static partial class DynamicProxyServiceCollectionExtensions
    {
        public static IServiceCollection AddProxyServiceWithoutTarget(this IServiceCollection services, Type serviceType, ServiceLifetime serviceLifetime, Action<ProxyServiceBuilder> proxySetup)
        {
            if (serviceType.IsGenericTypeDefinition)
            {
                throw new GeneratorException($"Can not create proxy for type {serviceType.FullName} because it is an open generic type.");
            }

            services.AddProxyCore();

            var proxyBuilder = new ProxyServiceBuilder(null)
            {
                Services = services,
                ProxyType = serviceType,
                ProxyLifetime = serviceLifetime,
            };

            proxySetup.Invoke(proxyBuilder);
            proxyBuilder.Build();

            return services;
        }

        public static IServiceCollection AddProxyServiceWithoutTarget<TService>(this IServiceCollection services, ServiceLifetime serviceLifetime, Action<ProxyServiceBuilder> proxySetup)
        {
            return services.AddProxyServiceWithoutTarget(typeof(TService), serviceLifetime, proxySetup);
        }
    }
}

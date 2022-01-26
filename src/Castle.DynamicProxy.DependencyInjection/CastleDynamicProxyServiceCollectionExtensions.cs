using System;
using System.Linq;

using Castle.DynamicProxy.Generators;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Castle.DynamicProxy.DependencyInjection
{
    public static class CastleDynamicProxyServiceCollectionExtensions
    {
        public static IServiceCollection AddProxyCore(this IServiceCollection services)
        {
            services.TryAddSingleton<IProxyBuilder>(new DefaultProxyBuilder());
            services.TryAddSingleton<IProxyGenerator>(sp => new ProxyGenerator(sp.GetRequiredService<IProxyBuilder>()));
            return services;
        }

        public static IServiceCollection EnableInterceptor<TService>(this IServiceCollection services, Action<IProxyServiceBuilder> proxySetup)
        {
            return services.EnableInterceptor(typeof(TService), proxySetup);
        }

        public static IServiceCollection EnableInterceptor(this IServiceCollection services, Type serviceType, Action<IProxyServiceBuilder> proxySetup)
        {
            if (serviceType.IsGenericTypeDefinition)
            {
                throw new GeneratorException($"Can not create proxy for type {serviceType.FullName} because it is an open generic type.");
            }

            var serviceDescriptors = services.Where(sd => sd.ServiceType == serviceType).ToList();
            if (!serviceDescriptors.Any())
            {
                throw new InvalidOperationException("Please register this service firstly");
            }

            services.AddProxyCore();

            foreach (var serviceDescriptor in serviceDescriptors)
            {
                var proxyBuilder = new ProxyServiceBuilder(services, serviceDescriptor);
                proxySetup.Invoke(proxyBuilder);
                proxyBuilder.Build();
            }

            return services;
        }
    }
}

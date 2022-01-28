using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    public class WithoutTargetProxyServiceBuilder : AbstractProxyServiceBuilder
    {
        private readonly Type _serviceType;
        private readonly ServiceLifetime _serviceLifetime;

        public WithoutTargetProxyServiceBuilder(Type serviceType, ServiceLifetime serviceLifetime, IServiceCollection services)
            : base(services)
        {
            _serviceType = serviceType;
            _serviceLifetime = serviceLifetime;
        }

        public override void Build()
        {
            var services = this.Services;
            services.Add(ServiceDescriptor.Describe(_serviceType, ProxyServiceImplementationFactory, _serviceLifetime));
        }

        private object ProxyServiceImplementationFactory(IServiceProvider sp)
        {
            var proxyGenerator = sp.GetRequiredService<IProxyGenerator>();

            var interceptors = InterceptorProviders.Select(descriptor => descriptor.Get(sp)).ToArray();

            return _serviceType.IsInterface ?
                proxyGenerator.CreateInterfaceProxyWithoutTarget(_serviceType, GenerationOptions, interceptors) :
                proxyGenerator.CreateClassProxy(_serviceType, GenerationOptions, ConstructUtils.GetConstructArgumentsFromClass(_serviceType, sp), interceptors);
        }
    }
}

using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    public class ProxyServiceBuilder : AbstractProxyServiceBuilder
    {
        private readonly ServiceDescriptor _oiginServiceDescriptor;

        public ProxyServiceBuilder(IServiceCollection services, ServiceDescriptor oiginServiceDescriptor)
            : base(services)
        {
            _oiginServiceDescriptor = oiginServiceDescriptor;
        }

        public override void Build()
        {
            var services = this.Services;

            services.Remove(_oiginServiceDescriptor);
            services.Add(ServiceDescriptor.Describe(_oiginServiceDescriptor.ServiceType, ProxyServiceImplementationFactory, _oiginServiceDescriptor.Lifetime));
        }

        private object ProxyServiceImplementationFactory(IServiceProvider sp)
        {
            var proxyGenerator = sp.GetRequiredService<IProxyGenerator>();

            object target;
            if (_oiginServiceDescriptor.ImplementationInstance != null)
            {
                target = _oiginServiceDescriptor.ImplementationInstance;
            }
            else if (_oiginServiceDescriptor.ImplementationFactory != null)
            {
                target = _oiginServiceDescriptor.ImplementationFactory.Invoke(sp);
            }
            else
            {
                target = ActivatorUtilities.GetServiceOrCreateInstance(sp, _oiginServiceDescriptor.ImplementationType);
            }

            var interceptors = InterceptorProviders.Select(descriptor => descriptor.Get(sp)).ToArray();

            return _oiginServiceDescriptor.ServiceType.IsInterface ?
                proxyGenerator.CreateInterfaceProxyWithTarget(_oiginServiceDescriptor.ServiceType, target, GenerationOptions, interceptors) :
                proxyGenerator.CreateClassProxyWithTarget(_oiginServiceDescriptor.ServiceType, target, GenerationOptions, interceptors);

        }
    }
}

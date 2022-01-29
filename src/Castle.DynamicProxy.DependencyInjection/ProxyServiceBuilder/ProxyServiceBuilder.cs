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
            if (_oiginServiceDescriptor.ServiceType.IsInterface)
            {
                services.Add(ServiceDescriptor.Describe(_oiginServiceDescriptor.ServiceType, ProxyServiceImplementationFactoryForInterface, _oiginServiceDescriptor.Lifetime));
            }
            else
            {
                services.Add(ServiceDescriptor.Describe(_oiginServiceDescriptor.ServiceType, ProxyServiceImplementationFactoryForClass, _oiginServiceDescriptor.Lifetime));
            }
        }

        private object ProxyServiceImplementationFactoryForInterface(IServiceProvider sp)
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
                target = ConstructUtils.GetConstructInfo(_oiginServiceDescriptor.ImplementationType).Factory.Invoke(sp).Instance;
            }

            var interceptors = InterceptorProviders.Select(descriptor => descriptor.Get(sp)).ToArray();

            return proxyGenerator.CreateInterfaceProxyWithTarget(_oiginServiceDescriptor.ServiceType, target, GenerationOptions, interceptors);

        }

        private object ProxyServiceImplementationFactoryForClass(IServiceProvider sp)
        {
            var proxyGenerator = sp.GetRequiredService<IProxyGenerator>();

            object[] constructorArguments;

            object target;

            if (_oiginServiceDescriptor.ImplementationInstance != null)
            {
                target = _oiginServiceDescriptor.ImplementationInstance;
                var constructorInfo = ConstructUtils.GetConstructInfo(target.GetType());
                constructorArguments = constructorInfo.SearchOrCreateArguments(sp, target);
            }
            else if (_oiginServiceDescriptor.ImplementationFactory != null)
            {
                target = _oiginServiceDescriptor.ImplementationFactory.Invoke(sp);
                var constructorInfo = ConstructUtils.GetConstructInfo(target.GetType());
                constructorArguments = constructorInfo.SearchOrCreateArguments(sp, target);
            }
            else
            {
                var constructorInfo = ConstructUtils.GetConstructInfo(_oiginServiceDescriptor.ImplementationType ?? _oiginServiceDescriptor.ServiceType);
                var instanceAndArguments = constructorInfo.Factory.Invoke(sp);
                target = instanceAndArguments.Instance;
                constructorArguments = instanceAndArguments.Arguments;
            }

            var interceptors = InterceptorProviders.Select(descriptor => descriptor.Get(sp)).ToArray();

            return proxyGenerator.CreateClassProxyWithTarget(_oiginServiceDescriptor.ServiceType, target, GenerationOptions, constructorArguments, interceptors);
        }
    }
}

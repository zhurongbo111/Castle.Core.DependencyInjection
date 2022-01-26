using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    public class ProxyServiceBuilder
    {
        private readonly ServiceDescriptor _oiginServiceDescriptor;

        private ProxyGenerationOptions _generationOptions = ProxyGenerationOptions.Default;

        public ProxyServiceBuilder(ServiceDescriptor oiginServiceDescriptor)
        {
            _oiginServiceDescriptor = oiginServiceDescriptor;
        }

        public ProxyServiceBuilder WithProxyGenerationOptions(ProxyGenerationOptions proxyGenerationOptions)
        {
            _generationOptions = proxyGenerationOptions;

            return this;
        }

        public IServiceCollection Services { get; internal set; }

        internal Type ProxyType { get; set; }

        internal ServiceLifetime? ProxyLifetime { get; set; }

        internal List<InterceptorDescriptor> InterceptorDescriptors { get; set; } = new List<InterceptorDescriptor>();

        internal IServiceCollection Build()
        {
            var services = this.Services;

            if (_oiginServiceDescriptor != null)
            {
                services.Remove(_oiginServiceDescriptor);
                services.Add(ServiceDescriptor.Describe(_oiginServiceDescriptor.ServiceType, ProxyServiceImplementationFactory, _oiginServiceDescriptor.Lifetime));
            }
            else
            {
                services.Add(ServiceDescriptor.Describe(ProxyType, ProxyServiceImplementationFactory, ProxyLifetime.Value));
            }

            return services;
        }

        private object ProxyServiceImplementationFactory(IServiceProvider sp)
        {
            var proxyGenerator = sp.GetRequiredService<IProxyGenerator>();

            if (_oiginServiceDescriptor != null)
            {
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

                var interceptors = InterceptorDescriptors.Select(descriptor => descriptor.Generate(sp)).ToArray();

                return _oiginServiceDescriptor.ServiceType.IsInterface ?
                    proxyGenerator.CreateInterfaceProxyWithTarget(_oiginServiceDescriptor.ServiceType, target, _generationOptions, interceptors) :
                    proxyGenerator.CreateClassProxyWithTarget(_oiginServiceDescriptor.ServiceType, target, _generationOptions, interceptors);
            }
            else
            {
                var interceptors = InterceptorDescriptors.Select(descriptor => descriptor.Generate(sp)).ToArray();

                return ProxyType.IsInterface ?
                    proxyGenerator.CreateInterfaceProxyWithoutTarget(ProxyType, _generationOptions, interceptors) :
                    proxyGenerator.CreateClassProxy(ProxyType, _generationOptions, interceptors);
            }
        }
    }
}

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

        internal ProxyType ProxyType { get; set; }

        internal List<Type> InterceptorTypes { get; set; } = new List<Type>();

        internal List<IInterceptor> InterceptorInstances { get; set; } = new List<IInterceptor>();

        internal List<Func<IServiceProvider, IInterceptor>> InterceptorFactory { get; set; } = new List<Func<IServiceProvider, IInterceptor>>();

        internal List<Func<IServiceProvider, IAsyncInterceptor>> AsyncInterceptorFactory { get; set; } = new List<Func<IServiceProvider, IAsyncInterceptor>>();

        internal IServiceCollection Build()
        {
            var services = this.Services;

            services.Remove(_oiginServiceDescriptor);

            services.Add(ServiceDescriptor.Describe(_oiginServiceDescriptor.ServiceType, ProxyServiceImplementationFactory, _oiginServiceDescriptor.Lifetime));

            return services;
        }

        private object ProxyServiceImplementationFactory(IServiceProvider sp)
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

            List<IInterceptor> interceptors = new List<IInterceptor>();
            foreach (var interceptorType in this.InterceptorTypes)
            {
                var interceptorInstance = ActivatorUtilities.GetServiceOrCreateInstance(sp, interceptorType);
                if (interceptorInstance is IAsyncInterceptor asyncInterceptorInstance)
                {
                    interceptors.Add(asyncInterceptorInstance.ToInterceptor());
                }
                else
                {
                    interceptors.Add((IInterceptor)interceptorInstance);
                }
            }

            if (InterceptorInstances.Any())
            {
                interceptors.AddRange(InterceptorInstances);
            }

            if (AsyncInterceptorFactory.Any())
            {
                interceptors.AddRange(AsyncInterceptorFactory.Select(factory => factory.Invoke(sp).ToInterceptor()));
            }

            if (InterceptorFactory.Any())
            {
                interceptors.AddRange(InterceptorFactory.Select(factory => factory.Invoke(sp)));
            }

            var proxyGenerator = sp.GetRequiredService<IProxyGenerator>();
            switch (ProxyType)
            {
                case ProxyType.InterfaceWithTarget:
                    return proxyGenerator.CreateInterfaceProxyWithTarget(_oiginServiceDescriptor.ServiceType, target, _generationOptions, interceptors.ToArray());
                case ProxyType.InterfaceWithoutTarget:
                    return proxyGenerator.CreateInterfaceProxyWithoutTarget(_oiginServiceDescriptor.ServiceType, _generationOptions, interceptors.ToArray());
                case ProxyType.ClassWithTarget:
                    return proxyGenerator.CreateClassProxyWithTarget(_oiginServiceDescriptor.ServiceType, target, _generationOptions, interceptors.ToArray());
                case ProxyType.ClassWithoutTarget:
                    return proxyGenerator.CreateClassProxy(_oiginServiceDescriptor.ServiceType, _generationOptions, interceptors.ToArray());
                default:
                    throw new Exception($"Unkonw proxy type: {ProxyType}");
            }
        }
    }
}

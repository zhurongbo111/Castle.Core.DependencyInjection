using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    public abstract class AbstractProxyServiceBuilder : IProxyServiceBuilder
    {
        public AbstractProxyServiceBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public ProxyGenerationOptions GenerationOptions { get; set; } = new ProxyGenerationOptions();

        public List<IInterceptorProvider> InterceptorProviders { get; } = new List<IInterceptorProvider>();

        public IServiceCollection Services { get; }

        public abstract void Build();
    }
}

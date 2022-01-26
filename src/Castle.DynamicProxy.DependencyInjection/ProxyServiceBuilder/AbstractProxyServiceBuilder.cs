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

        public List<IInterceptorProvider> InterceptorProviders { get; } = new List<IInterceptorProvider>();

        protected ProxyGenerationOptions GenerationOptions { get; private set; } = ProxyGenerationOptions.Default;

        public IServiceCollection Services { get; }

        public abstract void Build();

        public void WithProxyGenerationOptions(ProxyGenerationOptions proxyGenerationOptions)
        {
            GenerationOptions = proxyGenerationOptions;
        }
    }
}

using System;
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

        public IServiceCollection Services { get; }
        public Func<IServiceProvider, ProxyGenerationOptions> ProxyOptionCreator { get; set; }

        protected virtual ProxyGenerationOptions GetProxyGenerationOptions(IServiceProvider sp)
        {
            return ProxyOptionCreator == null ? ProxyGenerationOptions.Default : ProxyOptionCreator.Invoke(sp);
        }

        public abstract void Build();
    }
}

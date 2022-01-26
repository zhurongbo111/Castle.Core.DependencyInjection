using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    public interface IProxyServiceBuilder
    {
        IServiceCollection Services { get; }

        void WithProxyGenerationOptions(ProxyGenerationOptions proxyGenerationOptions);

        List<IInterceptorProvider> InterceptorProviders { get; }

        void Build();
    }
}

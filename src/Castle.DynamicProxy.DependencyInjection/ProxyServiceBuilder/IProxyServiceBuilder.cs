using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    public interface IProxyServiceBuilder
    {
        IServiceCollection Services { get; }

        Func<IServiceProvider, ProxyGenerationOptions> ProxyOptionCreator { get; set; }

        List<IInterceptorProvider> InterceptorProviders { get; }

        void Build();
    }
}

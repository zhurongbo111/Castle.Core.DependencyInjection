using System;
using System.Collections.Generic;

using Castle.DynamicProxy;

using Microsoft.Extensions.DependencyInjection;

namespace CastleDynamicProxy.DependencyInjection
{
    public interface IProxyServiceBuilder
    {
        IServiceCollection Services { get; }

        Func<IServiceProvider, ProxyGenerationOptions> ProxyOptionCreator { get; set; }

        List<IInterceptorProvider> InterceptorProviders { get; }

        void Build();
    }
}

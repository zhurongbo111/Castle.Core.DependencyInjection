using System;

using Castle.DynamicProxy;

namespace CastleDynamicProxy.DependencyInjection
{
    public interface IInterceptorProvider
    {
        IInterceptor Get(IServiceProvider sp);
    }
}

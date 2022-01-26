using System;

namespace Castle.DynamicProxy.DependencyInjection
{
    public interface IInterceptorProvider
    {
        IInterceptor Get(IServiceProvider sp);
    }
}

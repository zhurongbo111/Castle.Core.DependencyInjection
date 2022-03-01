using System;

using Castle.DynamicProxy;

using Microsoft.Extensions.DependencyInjection;

namespace CastleDynamicProxy.DependencyInjection
{
    public class AsyncTypeInterceptorProvider : IInterceptorProvider
    {
        private readonly Type _asyncInterceptorType;

        public AsyncTypeInterceptorProvider(Type interceptor)
        {
            _asyncInterceptorType = interceptor;
        }

        public IInterceptor Get(IServiceProvider sp)
        {
            return ((IAsyncInterceptor)ActivatorUtilities.GetServiceOrCreateInstance(sp, _asyncInterceptorType)).ToInterceptor();
        }
    }
}

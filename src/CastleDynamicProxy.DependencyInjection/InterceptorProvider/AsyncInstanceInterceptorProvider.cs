using System;

using Castle.DynamicProxy;

namespace CastleDynamicProxy.DependencyInjection
{
    public class AsyncInstanceInterceptorProvider : IInterceptorProvider
    {
        private readonly IAsyncInterceptor _interceptor;

        public AsyncInstanceInterceptorProvider(IAsyncInterceptor interceptor)
        {
            _interceptor = interceptor;
        }

        public IInterceptor Get(IServiceProvider sp)
        {
            return _interceptor.ToInterceptor();
        }
    }
}

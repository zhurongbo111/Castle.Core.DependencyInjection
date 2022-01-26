using System;

namespace Castle.DynamicProxy.DependencyInjection
{
    public class AsyncFactoryInterceptorProvider : IInterceptorProvider
    {
        private readonly Func<IServiceProvider, IAsyncInterceptor> _interceptorFactory;

        public AsyncFactoryInterceptorProvider(Func<IServiceProvider, IAsyncInterceptor> interceptorFactory)
        {
            _interceptorFactory = interceptorFactory;
        }

        public IInterceptor Get(IServiceProvider sp)
        {
            return _interceptorFactory(sp).ToInterceptor();
        }
    }
}

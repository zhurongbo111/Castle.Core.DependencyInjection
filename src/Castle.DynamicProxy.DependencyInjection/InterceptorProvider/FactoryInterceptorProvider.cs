using System;

namespace Castle.DynamicProxy.DependencyInjection
{
    public class FactoryInterceptorProvider : IInterceptorProvider
    {
        private readonly Func<IServiceProvider, IInterceptor> _interceptorFactory;

        public FactoryInterceptorProvider(Func<IServiceProvider, IInterceptor> interceptorFactory)
        {
            _interceptorFactory = interceptorFactory;
        }

        public IInterceptor Get(IServiceProvider sp)
        {
            return _interceptorFactory(sp);
        }
    }
}

using System;

namespace Castle.DynamicProxy.DependencyInjection
{
    public class InstanceInterceptorProvider : IInterceptorProvider
    {
        private readonly IInterceptor _interceptor;

        public InstanceInterceptorProvider(IInterceptor interceptor)
        {
            _interceptor = interceptor;
        }

        public IInterceptor Get(IServiceProvider sp)
        {
            return _interceptor;
        }
    }
}

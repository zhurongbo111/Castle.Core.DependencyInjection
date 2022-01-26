using System;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    public class TypeInterceptorProvider : IInterceptorProvider
    {
        private readonly Type _interceptorType;

        public TypeInterceptorProvider(Type interceptor)
        {
            _interceptorType = interceptor;
        }

        public IInterceptor Get(IServiceProvider sp)
        {
            return (IInterceptor)ActivatorUtilities.GetServiceOrCreateInstance(sp, _interceptorType);
        }
    }
}

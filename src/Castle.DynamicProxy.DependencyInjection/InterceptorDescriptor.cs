using System;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    internal class InterceptorDescriptor
    {
        internal Type InterceptorType { get; set; }

        internal IInterceptor InterceptorInstance { get; set; }

        internal Func<IServiceProvider, IInterceptor> InterceptorFactory { get; set; }

        internal Func<IServiceProvider, IAsyncInterceptor> AsyncInterceptorFactory { get; set; }

        internal IInterceptor Generate(IServiceProvider sp)
        {
            if (InterceptorType != null)
            {
                var interceptorInstance = ActivatorUtilities.GetServiceOrCreateInstance(sp, InterceptorType);
                if (interceptorInstance is IAsyncInterceptor asyncInterceptorInstance)
                {
                    return asyncInterceptorInstance.ToInterceptor();
                }
                else
                {
                    return (IInterceptor)interceptorInstance;
                }
            }
            else if (InterceptorFactory != null)
            {
                return InterceptorFactory(sp);
            }
            else if (AsyncInterceptorFactory != null)
            {
                return AsyncInterceptorFactory(sp).ToInterceptor();
            }
            else
            {
                return InterceptorInstance;
            }
        }
    }
}

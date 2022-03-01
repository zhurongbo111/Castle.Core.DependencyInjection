
using System;
using System.Reflection;

using Castle.DynamicProxy;

namespace CastleDynamicProxy.DependencyInjection.Tests
{
    public class InterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            return null;
        }
    }
}

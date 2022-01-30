
using System;
using System.Reflection;

namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    public class InterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            return null;
        }
    }
}

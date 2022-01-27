using System;
using System.Threading.Tasks;

namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    internal class AddScopedProxyServiceInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
        }
    }

    internal class AddScopedProxyServiceAsyncInterceptor : AsyncInterceptorBase
    {
        protected override Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        {
            return proceed(invocation, proceedInfo);
        }

        protected override Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        {
            return proceed(invocation, proceedInfo);
        }
    }
}

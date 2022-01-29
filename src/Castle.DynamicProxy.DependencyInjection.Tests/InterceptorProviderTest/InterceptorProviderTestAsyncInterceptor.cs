using System;
using System.Threading.Tasks;

namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    internal class InterceptorProviderTestAsyncInterceptor : AsyncInterceptorBase
    {
        public static int CalledCount = 0;

        protected override Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        {
            CalledCount++;
            return proceed(invocation, proceedInfo);
        }

        protected override Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        {
            CalledCount++;
            return proceed(invocation, proceedInfo);
        }
    }
}

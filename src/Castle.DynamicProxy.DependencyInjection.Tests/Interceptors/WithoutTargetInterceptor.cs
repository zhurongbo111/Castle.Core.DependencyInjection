using System;
using System.Threading.Tasks;

namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    internal class WithoutTargetInterceptor : IInterceptor
    {
        public static int CalledCount = 0;

        public void Intercept(IInvocation invocation)
        {
            CalledCount++;
        }
    }

    internal class WithoutTargetInterceptorAsync : AsyncInterceptorBase
    {
        public static int CalledCount = 0;

        protected override Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        {
            CalledCount++;
            return Task.CompletedTask;
        }

        protected override Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        {
            CalledCount++;
            return Task.FromResult(default(TResult));
        }
    }
}

using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DynamicProxy.AspNetCore.Example
{
    public class SimpleAsyncInterceptor : AsyncInterceptorBase
    {
        private readonly ILogger<SimpleAsyncInterceptor> _logger;

        public SimpleAsyncInterceptor(ILogger<SimpleAsyncInterceptor> logger)
        {
            _logger = logger;
        }

        protected override async Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        {
            _logger.LogInformation("{interceptorName is called before method executing}", nameof(SimpleAsyncInterceptor));
            await proceed.Invoke(invocation, proceedInfo);
            _logger.LogInformation("{interceptorName is called after method executed}", nameof(SimpleAsyncInterceptor));
        }

        protected override async Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        {
            _logger.LogInformation("{interceptorName} is called before method executing", nameof(SimpleAsyncInterceptor));
            var result = await proceed.Invoke(invocation, proceedInfo);
            _logger.LogInformation("{interceptorName} is called after method executed", nameof(SimpleAsyncInterceptor));
            return result;
        }
    }
}

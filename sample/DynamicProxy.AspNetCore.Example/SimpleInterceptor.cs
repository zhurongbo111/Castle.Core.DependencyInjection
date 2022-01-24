using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;

namespace DynamicProxy.AspNetCore.Example
{
    public class SimpleInterceptor : IInterceptor
    {
        private readonly ILogger<SimpleInterceptor> _logger;

        public SimpleInterceptor(ILogger<SimpleInterceptor> logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            _logger.LogInformation("{interceptorName} is called before method executing", nameof(SimpleInterceptor));
            invocation.Proceed();
            _logger.LogInformation("{interceptorName} is called after method executed", nameof(SimpleInterceptor));
        }
    }
}

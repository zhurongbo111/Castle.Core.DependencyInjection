using System.Threading.Tasks;

namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    public class AddScopedProxyService : IAddScopedProxyService
    {
        private readonly IMethodInvokeCounter _methodInvokeCounter;

        public AddScopedProxyService(IMethodInvokeCounter methodInvokeCounter)
        {
            _methodInvokeCounter = methodInvokeCounter;
        }

        public void Say(string message)
        {
            _methodInvokeCounter.AddCount();
        }
    }

    public class AddScopedProxyAsyncService : IAddScopedProxyAsyncService
    {
        private readonly IMethodInvokeCounter _methodInvokeCounter;

        public AddScopedProxyAsyncService(IMethodInvokeCounter methodInvokeCounter)
        {
            _methodInvokeCounter = methodInvokeCounter;
        }

        public async Task SayAsync(string message)
        {
            await _methodInvokeCounter.AddCountAsync();
        }
    }
}
using System.Threading.Tasks;

namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    public class MethodInvokeCounter : IMethodInvokeCounter
    {
        public int InvokeCount { get; set; }

        public void AddCount()
        {
            InvokeCount++;
        }

        public async Task AddCountAsync()
        {
            InvokeCount++;
            await Task.Yield();
        }
    }
}
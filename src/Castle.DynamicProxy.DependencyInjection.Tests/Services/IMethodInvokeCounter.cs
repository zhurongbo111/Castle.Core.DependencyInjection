using System.Threading.Tasks;

namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    public interface IMethodInvokeCounter
    {
        int InvokeCount { get; set; }

        void AddCount();

        Task AddCountAsync();
    }
}
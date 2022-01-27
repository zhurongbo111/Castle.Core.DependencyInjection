using System.Threading.Tasks;

namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    public interface IAddScopedProxyService
    {
        void Say(string message);
    }

    public interface IAddScopedProxyAsyncService
    {
        Task SayAsync(string message);
    }
}
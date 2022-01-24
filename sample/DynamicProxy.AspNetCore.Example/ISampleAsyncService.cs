using System.Threading.Tasks;

namespace DynamicProxy.AspNetCore.Example
{
    public interface ISampleAsyncService
    {
        Task<string> SayAsync(string message);
    }
}

using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DynamicProxy.AspNetCore.Example
{
    public class SampleAsyncService : ISampleAsyncService
    {
        private readonly ILogger<SampleAsyncService> _logger;

        public SampleAsyncService(ILogger<SampleAsyncService> logger)
        {
            _logger = logger;
        }

        public async Task<string> SayAsync(string message)
        {
            await Task.Delay(1000);
            _logger.LogInformation("Say {message}", message);
            return $"Say {message}";
        }
    }
}

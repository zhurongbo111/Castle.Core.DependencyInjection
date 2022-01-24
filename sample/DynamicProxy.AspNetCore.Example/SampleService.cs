using Microsoft.Extensions.Logging;

namespace DynamicProxy.AspNetCore.Example
{
    public class SampleService : ISampleService
    {
        private readonly ILogger<SampleService> _logger;

        public SampleService(ILogger<SampleService> logger)
        {
            _logger = logger;
        }

        public void Say(string message)
        {
            _logger.LogInformation("Say: {message}", message);
        }
    }
}

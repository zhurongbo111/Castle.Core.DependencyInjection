namespace CastleDynamicProxy.DependencyInjection.Tests
{
    public class LifetimeTestService : ILifetimeTestService
    {
        public static int InstanceCount = 0;

        public LifetimeTestService()
        {
            InstanceCount++;
        }
    }
}
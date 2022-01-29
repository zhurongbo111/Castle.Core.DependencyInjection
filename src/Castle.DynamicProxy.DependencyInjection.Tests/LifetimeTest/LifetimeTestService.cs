using System.Diagnostics;

namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    public class LifetimeTestService : ILifetimeTestService
    {
        public static int InstanceCount = 0;

        public LifetimeTestService()
        {
            InstanceCount++;
        }

        public virtual void Say(string message)
        {
            Debug.Write(message);
        }
    }
}
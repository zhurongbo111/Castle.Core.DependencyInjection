using System;

namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    internal class LifetimeTestInterceptor : IInterceptor
    {
        public static int InstanceCount = 0;

        public LifetimeTestInterceptor()
        {
            InstanceCount++;
        }

        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"{nameof(LifetimeTestInterceptor)} is called before executing");
            invocation.Proceed();
            Console.WriteLine($"{nameof(LifetimeTestInterceptor)} is called after executing");
        }
    }
}

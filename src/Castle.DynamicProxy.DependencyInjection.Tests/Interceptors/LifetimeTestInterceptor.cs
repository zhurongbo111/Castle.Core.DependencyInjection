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
            invocation.Proceed();
        }
    }
}

namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    internal class AddProxyServiceWithoutTargetTestInterceptor : IInterceptor
    {
        public static int CalledCount = 0;

        public void Intercept(IInvocation invocation)
        {
            CalledCount++;
        }
    }
}

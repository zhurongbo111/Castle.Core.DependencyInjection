namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    public class InterceptorProviderTest : IInterceptorProviderTest
    {
        public void Say()
        {
            System.Console.WriteLine("Say...");
        }
    }
}

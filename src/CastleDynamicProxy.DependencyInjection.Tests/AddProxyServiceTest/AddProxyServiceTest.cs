namespace CastleDynamicProxy.DependencyInjection.Tests
{
    public class AddProxyServiceTest : IAddProxyServiceTest
    {
        public virtual void Say()
        {
            System.Console.WriteLine("Say is called");
        }
    }
}

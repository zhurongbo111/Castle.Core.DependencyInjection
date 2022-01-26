namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    public interface ILifetimeTestService
    {
        void Say(string message);
    }

    public interface IWithoutTargetService
    {
        void Say(string message);
    }
}
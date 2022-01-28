namespace Castle.DynamicProxy.DependencyInjection.Tests
{
    public interface IWithoutTargetService
    {
        void Say();
    }

    public class WithoutTargetService
    {
        public virtual void Say()
        {
            System.Console.WriteLine("Hello");
        }
    }
}
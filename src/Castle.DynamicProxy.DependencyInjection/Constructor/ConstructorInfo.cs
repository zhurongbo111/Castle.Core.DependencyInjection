using System;

namespace Castle.DynamicProxy.DependencyInjection
{
    internal class ConstructorInfo
    {
        internal Func<IServiceProvider, InstanceAndArguments> Factory { get; set; }

        internal Type[] ConstructArgumentTypes { get; set; }

        internal Func<IServiceProvider, object, object[]> SearchOrCreateArguments { get; set; }
    }
}

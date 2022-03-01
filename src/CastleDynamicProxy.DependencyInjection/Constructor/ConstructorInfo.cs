using System;

namespace CastleDynamicProxy.DependencyInjection
{
    internal class ConstructorInfo
    {
        internal Func<IServiceProvider, InstanceAndArguments> Factory { get; set; }

        internal Type[] ConstructArgumentTypes { get; set; }

        internal Func<IServiceProvider, object, object[]> SearchOrCreateArguments { get; set; }
    }
}

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace Castle.DynamicProxy.DependencyInjection
{
    internal static class ObjectFactoryUtils
    {
        private static readonly ConcurrentDictionary<Type, ConstructorInfo> _factoryCache = new ConcurrentDictionary<Type, ConstructorInfo>();

        internal static ConstructorInfo GetConstructInfo(Type type)
        {
            return _factoryCache.GetOrAdd(type, _ =>
            {
                var constructorInfos = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

                var macthedConstructInfo = constructorInfos.FirstOrDefault(c => c.IsDefined(typeof(ActivatorUtilitiesConstructorAttribute), inherit: false));
                if (macthedConstructInfo == null)
                {
                    macthedConstructInfo = constructorInfos.OrderByDescending(c => c.GetParameters().Length).First();
                }

                var parameterInfos = macthedConstructInfo.GetParameters();
                if (parameterInfos.Length == 0)
                {
                    return new ConstructorInfo
                    {
                        Factory = sp => new InstanceAndArguments
                        {
                            Instance = macthedConstructInfo.Invoke(new object[0]),
                            Arguments = new object[0]
                        },
                        ConstructArgumentTypes = new Type[0],
                        DefaultArguments = new object[0]
                    };
                }
                else
                {
                    Type[] argumentTypes = parameterInfos.Select(c => c.ParameterType).ToArray();
                    return new ConstructorInfo
                    {
                        Factory = sp =>
                        {
                            var arguments = argumentTypes.Select(argumentType => sp.GetRequiredService(argumentType)).ToArray();
                            return new InstanceAndArguments
                            {
                                Instance = macthedConstructInfo.Invoke(arguments),
                                Arguments = arguments
                            };
                        },
                        ConstructArgumentTypes = argumentTypes,
                        DefaultArguments = argumentTypes.Select(argumentType => argumentType.IsValueType ? Activator.CreateInstance(argumentType) : null).ToArray()
                    };
                }
            });
        }

        internal static object[] GetConstructArguments(Type type, IServiceProvider sp)
        {
            var types = GetConstructInfo(type).ConstructArgumentTypes;
            return types.Select(t => sp.GetRequiredService(t)).ToArray();
        }
    }

    internal class ConstructorInfo
    {
        internal Func<IServiceProvider, InstanceAndArguments> Factory { get; set; }

        internal object[] DefaultArguments { get; set; }

        internal Type[] ConstructArgumentTypes { get; set; }
    }

    internal class InstanceAndArguments
    {
        public object Instance { get; set; }

        public object[] Arguments { get; set; }
    }
}

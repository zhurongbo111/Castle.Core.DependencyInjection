using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace CastleDynamicProxy.DependencyInjection
{
    internal static class ConstructUtils
    {
        private static readonly ConcurrentDictionary<Type, ConstructorInfo> _constructInfoCache = new ConcurrentDictionary<Type, ConstructorInfo>();

        internal static ConstructorInfo GetConstructInfo(Type type)
        {
            return _constructInfoCache.GetOrAdd(type, _ =>
            {
                var constructorInfos = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

                var macthedConstructInfo = constructorInfos.FirstOrDefault(c => c.IsDefined(typeof(DynamicProxyConstructorAttribute), inherit: false));
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
                        SearchOrCreateArguments = SearchCorrespondingArgumentsFromTarget(type, new Type[0])
                    };
                }
                else
                {
                    Type[] argumentTypes = parameterInfos.Select(c => c.ParameterType).ToArray();
                    return new ConstructorInfo
                    {
                        Factory = sp =>
                        {
                            var arguments = argumentTypes.Select(argumentType => sp.GetService(argumentType) ?? throw new InvalidOperationException($"Unable to resolve service for type '{argumentType}' while attempting to activate '{type}'.")).ToArray();
                            return new InstanceAndArguments
                            {
                                Instance = macthedConstructInfo.Invoke(arguments),
                                Arguments = arguments
                            };
                        },
                        ConstructArgumentTypes = argumentTypes,
                        SearchOrCreateArguments = SearchCorrespondingArgumentsFromTarget(type, argumentTypes)
                    };
                }
            });
        }

        internal static object[] GetConstructArgumentsFromClass(Type type, IServiceProvider sp)
        {
            var types = GetConstructInfo(type).ConstructArgumentTypes;
            return types.Select(t => sp.GetService(t) ?? throw new InvalidOperationException($"Unable to resolve service for type '{t}' while attempting to activate '{type}'.")).ToArray();
        }

        private static Func<IServiceProvider, object, object[]> SearchCorrespondingArgumentsFromTarget(Type targetType, Type[] argumentTypes)
        {
            var fields = targetType.GetFields(BindingFlags.Instance);
            var properties = targetType.GetProperties(BindingFlags.Instance);
            return (sp, target) =>
            {
                return argumentTypes.Select(argumentType =>
                {
                    var matchedFields = fields.Where(field => field.FieldType == argumentType).ToArray();
                    if (matchedFields.Any())
                    {
                        return matchedFields[0].GetValue(target);
                    }

                    var matchedProperties = properties.Where(p => p.PropertyType == argumentType).ToArray();
                    if (matchedProperties.Any())
                    {
                        return matchedProperties[0].GetValue(target);
                    }

                    return sp.GetService(argumentType) ?? (argumentType.IsValueType ? Activator.CreateInstance(argumentType) : null);
                }).ToArray();
            };
        }
    }
}

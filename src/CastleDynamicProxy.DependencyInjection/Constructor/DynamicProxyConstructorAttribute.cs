using System;

namespace CastleDynamicProxy.DependencyInjection
{
    /// <summary>
    /// Marks the constructor to be used when activating type by DynamicProxy Service.
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
    public class DynamicProxyConstructorAttribute : Attribute
    {

    }
}

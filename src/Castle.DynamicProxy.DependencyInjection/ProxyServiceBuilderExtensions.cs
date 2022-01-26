using System;

namespace Castle.DynamicProxy.DependencyInjection
{
    public static partial class ProxyServiceBuilderExtensions
    {
        public static ProxyServiceBuilder WithInterceptor(this ProxyServiceBuilder proxyServiceBuilder, IInterceptor interceptor)
        {
            proxyServiceBuilder.InterceptorDescriptors.Add(new InterceptorDescriptor
            {
                InterceptorInstance = interceptor
            });

            return proxyServiceBuilder;
        }

        public static ProxyServiceBuilder WithInterceptor<TInterceptor>(this ProxyServiceBuilder proxyServiceBuilder)
            where TInterceptor : class, IInterceptor
        {
            return proxyServiceBuilder.WithInterceptor(typeof(TInterceptor));
        }

        public static ProxyServiceBuilder WithInterceptor(this ProxyServiceBuilder proxyServiceBuilder, Type interceptroType)
        {
            proxyServiceBuilder.InterceptorDescriptors.Add(new InterceptorDescriptor
            {
                InterceptorType = interceptroType
            });

            return proxyServiceBuilder;
        }

        public static ProxyServiceBuilder WithInterceptor(this ProxyServiceBuilder proxyServiceBuilder, Func<IServiceProvider, IInterceptor> interceptorFactory)
        {
            proxyServiceBuilder.InterceptorDescriptors.Add(new InterceptorDescriptor
            {
                InterceptorFactory = interceptorFactory
            });

            return proxyServiceBuilder;
        }
    }
}

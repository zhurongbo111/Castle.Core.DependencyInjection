using System;

namespace Castle.DynamicProxy.DependencyInjection
{
    public static partial class ProxyServiceBuilderExtensions
    {
        public static ProxyServiceBuilder WithAsyncInterceptor(this ProxyServiceBuilder proxyServiceBuilder, IAsyncInterceptor asyncInterceptor)
        {
            proxyServiceBuilder.InterceptorDescriptors.Add(new InterceptorDescriptor
            {
                InterceptorInstance = asyncInterceptor.ToInterceptor()
            });
            return proxyServiceBuilder;
        }

        public static ProxyServiceBuilder WithAsyncInterceptor<TAsyncInterceptor>(this ProxyServiceBuilder proxyServiceBuilder)
            where TAsyncInterceptor : class, IAsyncInterceptor
        {
            return proxyServiceBuilder.WithAsyncInterceptor(typeof(TAsyncInterceptor));
        }

        public static ProxyServiceBuilder WithAsyncInterceptor(this ProxyServiceBuilder proxyServiceBuilder, Type asyncInterceptorType)
        {
            proxyServiceBuilder.InterceptorDescriptors.Add(new InterceptorDescriptor
            {
                InterceptorType = asyncInterceptorType
            });

            return proxyServiceBuilder;
        }

        public static ProxyServiceBuilder WithAsyncInterceptor(this ProxyServiceBuilder proxyServiceBuilder, Func<IServiceProvider, IAsyncInterceptor> asyncInterceptorFactory)
        {
            proxyServiceBuilder.InterceptorDescriptors.Add(new InterceptorDescriptor
            {
                AsyncInterceptorFactory = asyncInterceptorFactory
            });

            return proxyServiceBuilder;
        }
    }
}

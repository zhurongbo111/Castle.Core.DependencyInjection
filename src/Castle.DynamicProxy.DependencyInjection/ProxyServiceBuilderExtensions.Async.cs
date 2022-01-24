using System;

namespace Castle.DynamicProxy.DependencyInjection
{
    public static class ProxyServiceBuilderAsyncExtensions
    {
        public static ProxyServiceBuilder WithAsyncInterceptor(this ProxyServiceBuilder proxyServiceBuilder, IAsyncInterceptor asyncInterceptor)
        {
            proxyServiceBuilder.InterceptorInstances.Add(asyncInterceptor.ToInterceptor());
            return proxyServiceBuilder;
        }

        public static ProxyServiceBuilder WithAsyncInterceptor<TAsyncInterceptor>(this ProxyServiceBuilder proxyServiceBuilder)
            where TAsyncInterceptor : class, IAsyncInterceptor
        {
            return proxyServiceBuilder.WithAsyncInterceptor(typeof(TAsyncInterceptor));
        }

        public static ProxyServiceBuilder WithAsyncInterceptor(this ProxyServiceBuilder proxyServiceBuilder, Type asyncInterceptorType)
        {
            proxyServiceBuilder.InterceptorTypes.Add(asyncInterceptorType);

            return proxyServiceBuilder;
        }

        public static ProxyServiceBuilder WithAsyncInterceptor(this ProxyServiceBuilder proxyServiceBuilder, Func<IServiceProvider, IAsyncInterceptor> asyncInterceptorFactory)
        {
            proxyServiceBuilder.AsyncInterceptorFactory.Add(asyncInterceptorFactory);
            return proxyServiceBuilder;
        }
    }
}

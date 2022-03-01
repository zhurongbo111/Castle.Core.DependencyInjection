using System;

using Castle.DynamicProxy;

namespace CastleDynamicProxy.DependencyInjection
{
    public static partial class ProxyServiceBuilderExtensions
    {
        public static IProxyServiceBuilder WithAsyncInterceptor(this IProxyServiceBuilder proxyServiceBuilder, IAsyncInterceptor asyncInterceptor)
        {
            proxyServiceBuilder.InterceptorProviders.Add(new AsyncInstanceInterceptorProvider(asyncInterceptor));
            return proxyServiceBuilder;
        }

        public static IProxyServiceBuilder WithAsyncInterceptor<TAsyncInterceptor>(this IProxyServiceBuilder proxyServiceBuilder)
            where TAsyncInterceptor : class, IAsyncInterceptor
        {
            return proxyServiceBuilder.WithAsyncInterceptor(typeof(TAsyncInterceptor));
        }

        public static IProxyServiceBuilder WithAsyncInterceptor(this IProxyServiceBuilder proxyServiceBuilder, Type asyncInterceptorType)
        {
            proxyServiceBuilder.InterceptorProviders.Add(new AsyncTypeInterceptorProvider(asyncInterceptorType));

            return proxyServiceBuilder;
        }

        public static IProxyServiceBuilder WithAsyncInterceptor(this IProxyServiceBuilder proxyServiceBuilder, Func<IServiceProvider, IAsyncInterceptor> asyncInterceptorFactory)
        {
            proxyServiceBuilder.InterceptorProviders.Add(new AsyncFactoryInterceptorProvider(asyncInterceptorFactory));

            return proxyServiceBuilder;
        }
    }
}

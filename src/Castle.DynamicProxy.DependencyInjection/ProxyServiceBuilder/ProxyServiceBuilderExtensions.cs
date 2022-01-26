using System;

namespace Castle.DynamicProxy.DependencyInjection
{
    public static partial class ProxyServiceBuilderExtensions
    {
        public static IProxyServiceBuilder WithInterceptor(this IProxyServiceBuilder proxyServiceBuilder, IInterceptor interceptor)
        {
            proxyServiceBuilder.InterceptorProviders.Add(new InstanceInterceptorProvider(interceptor));

            return proxyServiceBuilder;
        }

        public static IProxyServiceBuilder WithInterceptor<TInterceptor>(this IProxyServiceBuilder proxyServiceBuilder)
            where TInterceptor : class, IInterceptor
        {
            return proxyServiceBuilder.WithInterceptor(typeof(TInterceptor));
        }

        public static IProxyServiceBuilder WithInterceptor(this IProxyServiceBuilder proxyServiceBuilder, Type interceptroType)
        {
            proxyServiceBuilder.InterceptorProviders.Add(new TypeInterceptorProvider(interceptroType));

            return proxyServiceBuilder;
        }

        public static IProxyServiceBuilder WithInterceptor(this IProxyServiceBuilder proxyServiceBuilder, Func<IServiceProvider, IInterceptor> interceptorFactory)
        {
            proxyServiceBuilder.InterceptorProviders.Add(new FactoryInterceptorProvider(interceptorFactory));

            return proxyServiceBuilder;
        }
    }
}

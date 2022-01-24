using System;

namespace Castle.DynamicProxy.DependencyInjection
{
    public static class ProxyServiceBuilderExtensions
    {
        public static ProxyServiceBuilder WithInterceptor(this ProxyServiceBuilder proxyServiceBuilder, IInterceptor interceptor)
        {
            proxyServiceBuilder.InterceptorInstances.Add(interceptor);
            return proxyServiceBuilder;
        }

        public static ProxyServiceBuilder WithInterceptor<TInterceptor>(this ProxyServiceBuilder proxyServiceBuilder)
            where TInterceptor : class, IInterceptor
        {
            return proxyServiceBuilder.WithInterceptor(typeof(TInterceptor));
        }

        public static ProxyServiceBuilder WithInterceptor(this ProxyServiceBuilder proxyServiceBuilder, Type interceptroType)
        {
            proxyServiceBuilder.InterceptorTypes.Add(interceptroType);

            return proxyServiceBuilder;
        }

        public static ProxyServiceBuilder WithInterceptor(this ProxyServiceBuilder proxyServiceBuilder, Func<IServiceProvider, IInterceptor> interceptorFactory)
        {
            proxyServiceBuilder.InterceptorFactory.Add(interceptorFactory);
            return proxyServiceBuilder;
        }
    }
}

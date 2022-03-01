# CastleDynamicProxy.DependencyInjection

This project is an extension to Castle DynamicProxy, it benefits you from DependencyInjection.

Packages
--------

NuGet feed: https://www.nuget.org/packages/CastleDynamicProxy.DependencyInjection/

| Package                                                      | NuGet Stable                                                 | Downloads                                                    |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| [CastleDynamicProxy.DependencyInjection](https://www.nuget.org/packages/CastleDynamicProxy.DependencyInjection/) | [![CastleDynamicProxy.DependencyInjection](https://img.shields.io/nuget/vpre/CastleDynamicProxy.DependencyInjection.svg)](https://www.nuget.org/packages/CastleDynamicProxy.DependencyInjection/) | [![CastleDynamicProxy.DependencyInjection](https://img.shields.io/nuget/dt/CastleDynamicProxy.DependencyInjection.svg)](https://www.nuget.org/packages/CastleDynamicProxy.DependencyInjection/) |

## What is Castle DynamicProxy?
DynamicProxy generates proxies for your objects that you can use to transparently add or alter behavior to them, provide pre/post processing and many other things. For detailed info, please refer to [Dynamic Proxy](https://github.com/castleproject/Core/blob/master/docs/dynamicproxy.md)

## What is CastleDynamicProxy.DependencyInjection?
It's an extension to Castle DynamicProxy, it benefits you from DependencyInjection.

## How to use CastleDynamicProxy.DependencyInjection
Before use this library, it's probably safe to assume you are familiar with Castle DynamicProxy and Microsoft DependencyInjection.

### Case 1: You want to add dynamic proxy for an interface
```csharp
    public interface IMyInterface
    {
        void Say();
    }

    public class MyImpl : IMyInterface
    {
        public void Say()
        {
            System.Console.WriteLine("Say is called");
        }
    }

    public class TestInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            System.Console.WriteLine("Called before method executing.");
            invocation.Proceed();
            System.Console.WriteLine("Called after method executed.");
        }
    }
```
When you add your interface to container, you can use following:
```csharp
    services.AddTransient<TestInterceptor>();
    services.AddTransientProxyService<IMyInterface, MyImpl>(proxybuilder =>
    {
        proxybuilder.WithInterceptor<TestInterceptor>();
    });
```
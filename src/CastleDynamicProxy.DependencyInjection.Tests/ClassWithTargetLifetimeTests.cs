using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CastleDynamicProxy.DependencyInjection.Tests
{
    [TestClass]
    public class ClassWithTargetLifetimeTests
    {
        [TestMethod]
        [DataRow(0, 0, DisplayName = "When class is registered as transient type, and interceptor is not registered but actived by type, the proxy service is created every time, and the interceptor is created every time.")]
        [DataRow(0, 1, DisplayName = "When class is registered as transient type, and interceptor is not registered but actived by factory, the proxy service is created every time, and the interceptor is created every time.")]
        [DataRow(1, 0, DisplayName = "When class is registered as transient factory, and interceptor is not registered but actived by type, the proxy service is created every time, and the interceptor is created every time.")]
        [DataRow(1, 1, DisplayName = "When class is registered as transient factory, and interceptor is not registered but actived by factory, the proxy service is created every time, and the interceptor is created every time.")]
        public void Class_Transient_Interceptor_NotRegister(int interfaceRegisterType, int interceptorRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            switch (interfaceRegisterType)
            {
                case 0: services.AddTransient<LifetimeTestService>(); break;
                case 1: services.AddTransient(sp => new LifetimeTestService()); break;
            }

            services.EnableInterceptor<LifetimeTestService>(builder =>
            {
                switch (interceptorRegisterType)
                {
                    case 0: builder.WithInterceptor<LifetimeTestInterceptor>(); break;
                    case 1: builder.WithInterceptor(sp => new LifetimeTestInterceptor()); break;
                }
            });

            ILifetimeTestService service1, service2;

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                Assert.AreEqual(2, LifetimeTestService.InstanceCount);

                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptor = scope.ServiceProvider.GetService<LifetimeTestInterceptor>();

                Assert.AreNotEqual(instanceA, instanceB);
                Assert.IsNull(interceptor);

                Assert.AreEqual(4, LifetimeTestService.InstanceCount);
                Assert.AreEqual(2, LifetimeTestInterceptor.InstanceCount);
                service1 = instanceA;
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                Assert.AreEqual(6, LifetimeTestService.InstanceCount);

                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptor = scope.ServiceProvider.GetService<LifetimeTestInterceptor>();

                Assert.AreNotEqual(instanceA, instanceB);
                Assert.IsNull(interceptor);

                Assert.AreEqual(8, LifetimeTestService.InstanceCount);
                Assert.AreEqual(4, LifetimeTestInterceptor.InstanceCount);

                service2 = instanceA;
            }

            Assert.AreNotEqual(service1, service2);
        }

        [TestMethod]
        [DataRow(0, 0, DisplayName = "When class is registered as transient type, and interceptor is registered as transient type, the proxy service is created every time, and the interceptor is created every time.")]
        [DataRow(0, 1, DisplayName = "When class is registered as transient type, and interceptor is registered as transient factory, the proxy service is created every time, and the interceptor is created every time.")]
        [DataRow(1, 0, DisplayName = "When class is registered as transient factory, and interceptor is registered as transient type, the proxy service is created every time, and the interceptor is created every time.")]
        [DataRow(1, 1, DisplayName = "When class is registered as transient factory, and interceptor is registered as transient factory, the proxy service is created every time, and the interceptor is created every time.")]
        public void Class_Transient_Interceptor_Transient(int interfaceRegisterType, int interceptorRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            switch (interfaceRegisterType)
            {
                case 0: services.AddTransient<LifetimeTestService>(); break;
                case 1: services.AddTransient(sp => new LifetimeTestService()); break;
            }

            switch (interceptorRegisterType)
            {
                case 0: services.AddTransient<LifetimeTestInterceptor>(); break;
                case 1: services.AddTransient(sp => new LifetimeTestInterceptor()); break;
            }

            services.EnableInterceptor<LifetimeTestService>(builder =>
            {
                builder.WithInterceptor<LifetimeTestInterceptor>();
            });

            ILifetimeTestService service1, service2;
            LifetimeTestInterceptor interceptor1, interceptor2;

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                Assert.AreEqual(2, LifetimeTestService.InstanceCount);

                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreNotEqual(instanceA, instanceB);
                Assert.AreNotEqual(interceptorA, interceptorB);

                Assert.AreEqual(4, LifetimeTestService.InstanceCount);
                Assert.AreEqual(4, LifetimeTestInterceptor.InstanceCount);
                service1 = instanceA;
                interceptor1 = interceptorA;
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                Assert.AreEqual(6, LifetimeTestService.InstanceCount);

                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreNotEqual(instanceA, instanceB);
                Assert.AreNotEqual(interceptorA, interceptorB);

                Assert.AreEqual(8, LifetimeTestService.InstanceCount);
                Assert.AreEqual(8, LifetimeTestInterceptor.InstanceCount);

                service2 = instanceA;
                interceptor2 = interceptorA;
            }

            Assert.AreNotEqual(service1, service2);
            Assert.AreNotEqual(interceptor1, interceptor2);
        }

        [TestMethod]
        [DataRow(0, 0, DisplayName = "When class is registered as transient type, and interceptor is registered as scoped type, the proxy service is created every time, but the interceptor is created every scope.")]
        [DataRow(0, 1, DisplayName = "When class is registered as transient type, and interceptor is registered as scoped factory, the proxy service is created every time, but the interceptor is created every scope.")]
        [DataRow(1, 0, DisplayName = "When class is registered as transient factory, and interceptor is registered as scoped type, the proxy service is created every time, but the interceptor is created every scope.")]
        [DataRow(1, 1, DisplayName = "When class is registered as transient factory, and interceptor is registered as scoped factory, the proxy service is created every time, but the interceptor is created every scope.")]
        public void Class_Transient_Interceptor_Scoped(int interfaceRegisterType, int interceptorRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            switch (interfaceRegisterType)
            {
                case 0: services.AddTransient<LifetimeTestService>(); break;
                case 1: services.AddTransient(sp => new LifetimeTestService()); break;
            }

            switch (interceptorRegisterType)
            {
                case 0: services.AddScoped<LifetimeTestInterceptor>(); break;
                case 1: services.AddScoped(sp => new LifetimeTestInterceptor()); break;
            }

            services.EnableInterceptor<LifetimeTestService>(builder =>
            {
                builder.WithInterceptor<LifetimeTestInterceptor>();
            });

            ILifetimeTestService service1, service2;
            LifetimeTestInterceptor interceptor1, interceptor2;

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                Assert.AreEqual(2, LifetimeTestService.InstanceCount);
                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreNotEqual(instanceA, instanceB);
                Assert.AreEqual(interceptorA, interceptorB);

                Assert.AreEqual(4, LifetimeTestService.InstanceCount);
                Assert.AreEqual(1, LifetimeTestInterceptor.InstanceCount);
                service1 = instanceA;
                interceptor1 = interceptorA;
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                Assert.AreEqual(6, LifetimeTestService.InstanceCount);

                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreNotEqual(instanceA, instanceB);
                Assert.AreEqual(interceptorA, interceptorB);

                Assert.AreEqual(8, LifetimeTestService.InstanceCount);
                Assert.AreEqual(2, LifetimeTestInterceptor.InstanceCount);

                service2 = instanceA;
                interceptor2 = interceptorA;
            }

            Assert.AreNotEqual(service1, service2);
            Assert.AreNotEqual(interceptor1, interceptor2);
        }

        [TestMethod]
        [DataRow(0, 0, DisplayName = "When class is registered as transient type, and interceptor is registered as singleton type, the proxy service is created every time, but the interceptor is created only one time.")]
        [DataRow(0, 1, DisplayName = "When class is registered as transient type, and interceptor is registered as singleton factory, the proxy service is created every time, but the interceptor is created only one time.")]
        [DataRow(0, 2, DisplayName = "When class is registered as transient type, and interceptor is registered as singleton instance, the proxy service is created every time, but the interceptor is created only one time.")]
        [DataRow(1, 0, DisplayName = "When class is registered as transient factory, and interceptor is registered as singleton type, the proxy service is created every time, but the interceptor is created only one time.")]
        [DataRow(1, 1, DisplayName = "When class is registered as transient factory, and interceptor is registered as singleton factory, the proxy service is created every time, but the interceptor is created only one time.")]
        [DataRow(1, 2, DisplayName = "When class is registered as transient factory, and interceptor is registered as singleton instance, the proxy service is created every time, but the interceptor is created only one time.")]
        public void Class_Transient_Interceptor_Singleton(int interfaceRegisterType, int interceptorRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            switch (interfaceRegisterType)
            {
                case 0: services.AddTransient<LifetimeTestService>(); break;
                case 1: services.AddTransient(sp => new LifetimeTestService()); break;
            }

            switch (interceptorRegisterType)
            {
                case 0: services.AddSingleton<LifetimeTestInterceptor>(); break;
                case 1: services.AddSingleton(sp => new LifetimeTestInterceptor()); break;
                case 2: services.AddSingleton(new LifetimeTestInterceptor()); break;
            }

            services.EnableInterceptor<LifetimeTestService>(builder =>
            {
                builder.WithInterceptor<LifetimeTestInterceptor>();
            });

            ILifetimeTestService service1, service2;
            LifetimeTestInterceptor interceptor1, interceptor2;

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                Assert.AreEqual(2, LifetimeTestService.InstanceCount);

                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreNotEqual(instanceA, instanceB);
                Assert.AreEqual(interceptorA, interceptorB);

                Assert.AreEqual(4, LifetimeTestService.InstanceCount);
                Assert.AreEqual(1, LifetimeTestInterceptor.InstanceCount);
                service1 = instanceA;
                interceptor1 = interceptorA;
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                Assert.AreEqual(6, LifetimeTestService.InstanceCount);

                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreNotEqual(instanceA, instanceB);
                Assert.AreEqual(interceptorA, interceptorB);

                Assert.AreEqual(8, LifetimeTestService.InstanceCount);
                Assert.AreEqual(1, LifetimeTestInterceptor.InstanceCount);

                service2 = instanceA;
                interceptor2 = interceptorA;
            }

            Assert.AreNotEqual(service1, service2);
            Assert.AreEqual(interceptor1, interceptor2);
        }

        [TestMethod]
        [DataRow(0, 0, DisplayName = "When class is registered as scoped type, and interceptor is not registered but actived by type, the proxy service is created every scope, and the interceptor is created every scope.")]
        [DataRow(0, 1, DisplayName = "When class is registered as scoped type, and interceptor is not registered but actived by factory, the proxy service is created every scope, and the interceptor is created every scope.")]
        [DataRow(1, 0, DisplayName = "When class is registered as scoped factory, and interceptor is not registered but actived by type, the proxy service is created every scope, and the interceptor is created every scope.")]
        [DataRow(1, 1, DisplayName = "When class is registered as scoped factory, and interceptor is not registered but actived by factory, the proxy service is created every scope, and the interceptor is created every scope.")]
        public void Class_Scoped_Interceptor_NotRegister(int interfaceRegisterType, int interceptorRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            switch (interfaceRegisterType)
            {
                case 0: services.AddScoped<LifetimeTestService>(); break;
                case 1: services.AddScoped(sp => new LifetimeTestService()); break;
            }

            services.EnableInterceptor<LifetimeTestService>(builder =>
            {
                switch (interceptorRegisterType)
                {
                    case 0: builder.WithInterceptor<LifetimeTestInterceptor>(); break;
                    case 1: builder.WithInterceptor(sp => new LifetimeTestInterceptor()); break;
                }
            });

            ILifetimeTestService service1, service2;

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptor = scope.ServiceProvider.GetService<LifetimeTestInterceptor>();

                Assert.AreEqual(instanceA, instanceB);
                Assert.IsNull(interceptor);

                Assert.AreEqual(2, LifetimeTestService.InstanceCount);
                Assert.AreEqual(1, LifetimeTestInterceptor.InstanceCount);
                service1 = instanceA;
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptor = scope.ServiceProvider.GetService<LifetimeTestInterceptor>();

                Assert.AreEqual(instanceA, instanceB);
                Assert.IsNull(interceptor);

                Assert.AreEqual(4, LifetimeTestService.InstanceCount);
                Assert.AreEqual(2, LifetimeTestInterceptor.InstanceCount);

                service2 = instanceA;
            }

            Assert.AreNotEqual(service1, service2);
        }

        [TestMethod]
        [DataRow(0, 0, DisplayName = "When class is registered as scoped type, and interceptor is registered as transient type, the proxy service is created every scope, and the interceptor is created every time.")]
        [DataRow(0, 1, DisplayName = "When class is registered as scoped type, and interceptor is registered as transient factory, the proxy service is created every scope, and the interceptor is created every time.")]
        [DataRow(1, 0, DisplayName = "When class is registered as scoped factory, and interceptor is registered as transient type, the proxy service is created every scope, and the interceptor is created every time.")]
        [DataRow(1, 1, DisplayName = "When class is registered as scoped factory, and interceptor is registered as transient factory, the proxy service is created every scope, and the interceptor is created every time.")]
        public void Class_Scoped_Interceptor_Transient(int interfaceRegisterType, int interceptorRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            switch (interfaceRegisterType)
            {
                case 0: services.AddScoped<LifetimeTestService>(); break;
                case 1: services.AddScoped(sp => new LifetimeTestService()); break;
            }

            switch (interceptorRegisterType)
            {
                case 0: services.AddTransient<LifetimeTestInterceptor>(); break;
                case 1: services.AddTransient(sp => new LifetimeTestInterceptor()); break;
            }

            services.EnableInterceptor<LifetimeTestService>(builder =>
            {
                builder.WithInterceptor<LifetimeTestInterceptor>();
            });

            ILifetimeTestService service1, service2;
            LifetimeTestInterceptor interceptor1, interceptor2;

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreEqual(instanceA, instanceB);
                Assert.AreNotEqual(interceptorA, interceptorB);

                Assert.AreEqual(2, LifetimeTestService.InstanceCount);
                Assert.AreEqual(3, LifetimeTestInterceptor.InstanceCount);
                service1 = instanceA;
                interceptor1 = interceptorA;
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreEqual(instanceA, instanceB);
                Assert.AreNotEqual(interceptorA, interceptorB);

                Assert.AreEqual(4, LifetimeTestService.InstanceCount);
                Assert.AreEqual(6, LifetimeTestInterceptor.InstanceCount);

                service2 = instanceA;
                interceptor2 = interceptorA;
            }

            Assert.AreNotEqual(service1, service2);
            Assert.AreNotEqual(interceptor1, interceptor2);
        }

        [TestMethod]
        [DataRow(0, 0, DisplayName = "When class is registered as scoped type, and interceptor is registered as scoped type, the proxy service is created every scope, and the interceptor is created every scope.")]
        [DataRow(0, 1, DisplayName = "When class is registered as scoped type, and interceptor is registered as scoped factory, the proxy service is created every scope, and the interceptor is created every scope.")]
        [DataRow(1, 0, DisplayName = "When class is registered as scoped factory, and interceptor is registered as scoped type, the proxy service is created every scope, and the interceptor is created every scope.")]
        [DataRow(1, 1, DisplayName = "When class is registered as scoped factory, and interceptor is registered as scoped factory, the proxy service is created every scope, and the interceptor is created every scope.")]
        public void Class_Scoped_Interceptor_Scoped(int interfaceRegisterType, int interceptorRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            switch (interfaceRegisterType)
            {
                case 0: services.AddScoped<LifetimeTestService>(); break;
                case 1: services.AddScoped(sp => new LifetimeTestService()); break;
            }

            switch (interceptorRegisterType)
            {
                case 0: services.AddScoped<LifetimeTestInterceptor>(); break;
                case 1: services.AddScoped(sp => new LifetimeTestInterceptor()); break;
            }

            services.EnableInterceptor<LifetimeTestService>(builder =>
            {
                builder.WithInterceptor<LifetimeTestInterceptor>();
            });

            ILifetimeTestService service1, service2;
            LifetimeTestInterceptor interceptor1, interceptor2;

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreEqual(instanceA, instanceB);
                Assert.AreEqual(interceptorA, interceptorB);

                Assert.AreEqual(2, LifetimeTestService.InstanceCount);
                Assert.AreEqual(1, LifetimeTestInterceptor.InstanceCount);
                service1 = instanceA;
                interceptor1 = interceptorA;
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreEqual(instanceA, instanceB);
                Assert.AreEqual(interceptorA, interceptorB);

                Assert.AreEqual(4, LifetimeTestService.InstanceCount);
                Assert.AreEqual(2, LifetimeTestInterceptor.InstanceCount);

                service2 = instanceA;
                interceptor2 = interceptorA;
            }

            Assert.AreNotEqual(service1, service2);
            Assert.AreNotEqual(interceptor1, interceptor2);
        }

        [TestMethod]
        [DataRow(0, 0, DisplayName = "When class is registered as scoped type, and interceptor is registered as singleton type, the proxy service is created every scope, and the interceptor is created only one time.")]
        [DataRow(0, 1, DisplayName = "When class is registered as scoped type, and interceptor is registered as singleton factory, the proxy service is created every scope, and the interceptor is created only one time.")]
        [DataRow(0, 2, DisplayName = "When class is registered as scoped type, and interceptor is registered as singleton instance, the proxy service is created every scope, and the interceptor is created only one time.")]
        [DataRow(1, 0, DisplayName = "When class is registered as scoped factory, and interceptor is registered as singleton type, the proxy service is created every scope, and the interceptor is created only one time.")]
        [DataRow(1, 1, DisplayName = "When class is registered as scoped factory, and interceptor is registered as singleton factory, the proxy service is created every scope, and the interceptor is created only one time.")]
        [DataRow(1, 2, DisplayName = "When class is registered as scoped factory, and interceptor is registered as singleton instance, the proxy service is created every scope, and the interceptor is created only one time.")]
        public void Class_Scoped_Interceptor_Singleton(int interfaceRegisterType, int interceptorRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            switch (interfaceRegisterType)
            {
                case 0: services.AddScoped<LifetimeTestService>(); break;
                case 1: services.AddScoped(sp => new LifetimeTestService()); break;
            }

            switch (interceptorRegisterType)
            {
                case 0: services.AddSingleton<LifetimeTestInterceptor>(); break;
                case 1: services.AddSingleton(sp => new LifetimeTestInterceptor()); break;
                case 2: services.AddSingleton(new LifetimeTestInterceptor()); break;
            }

            services.EnableInterceptor<LifetimeTestService>(builder =>
            {
                builder.WithInterceptor<LifetimeTestInterceptor>();
            });

            ILifetimeTestService service1, service2;
            LifetimeTestInterceptor interceptor1, interceptor2;

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreEqual(instanceA, instanceB);
                Assert.AreEqual(interceptorA, interceptorB);

                Assert.AreEqual(2, LifetimeTestService.InstanceCount);
                Assert.AreEqual(1, LifetimeTestInterceptor.InstanceCount);
                service1 = instanceA;
                interceptor1 = interceptorA;
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreEqual(instanceA, instanceB);
                Assert.AreEqual(interceptorA, interceptorB);

                Assert.AreEqual(4, LifetimeTestService.InstanceCount);
                Assert.AreEqual(1, LifetimeTestInterceptor.InstanceCount);

                service2 = instanceA;
                interceptor2 = interceptorA;
            }

            Assert.AreNotEqual(service1, service2);
            Assert.AreEqual(interceptor1, interceptor2);
        }

        [TestMethod]
        [DataRow(0, 0, DisplayName = "When class is registered as singleton type, and interceptor is not registered but actived by type, the proxy service is created only one time, and the interceptor is created only one time.")]
        [DataRow(0, 1, DisplayName = "When class is registered as singleton type, and interceptor is not registered but actived by factory, the proxy service is created only one time, and the interceptor is created only one time.")]
        [DataRow(1, 0, DisplayName = "When class is registered as singleton factory, and interceptor is not registered but actived by type, the proxy service is created only one time, and the interceptor is created only one time.")]
        [DataRow(1, 1, DisplayName = "When class is registered as singleton factory, and interceptor is not registered but actived by factory, the proxy service is created only one time, and the interceptor is created only one time.")]
        [DataRow(2, 0, DisplayName = "When class is registered as singleton instance, and interceptor is not registered but actived by type, the proxy service is created only one time, and the interceptor is created only one time.")]
        [DataRow(2, 1, DisplayName = "When class is registered as singleton instance, and interceptor is not registered but actived by factory, the proxy service is created only one time, and the interceptor is created only one time.")]
        public void Class_Singleton_Interceptor_NotRegister(int interfaceRegisterType, int interceptorRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            switch (interfaceRegisterType)
            {
                case 0: services.AddSingleton<LifetimeTestService>(); break;
                case 1: services.AddSingleton(sp => new LifetimeTestService()); break;
                case 2: services.AddSingleton(new LifetimeTestService()); break;
            }

            services.EnableInterceptor<LifetimeTestService>(builder =>
            {
                switch (interceptorRegisterType)
                {
                    case 0: builder.WithInterceptor<LifetimeTestInterceptor>(); break;
                    case 1: builder.WithInterceptor(sp => new LifetimeTestInterceptor()); break;
                }
            });

            ILifetimeTestService service1, service2;

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptor = scope.ServiceProvider.GetService<LifetimeTestInterceptor>();

                Assert.AreEqual(instanceA, instanceB);
                Assert.IsNull(interceptor);

                Assert.AreEqual(2, LifetimeTestService.InstanceCount);
                Assert.AreEqual(1, LifetimeTestInterceptor.InstanceCount);
                service1 = instanceA;
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptor = scope.ServiceProvider.GetService<LifetimeTestInterceptor>();

                Assert.AreEqual(instanceA, instanceB);
                Assert.IsNull(interceptor);

                Assert.AreEqual(2, LifetimeTestService.InstanceCount);
                Assert.AreEqual(1, LifetimeTestInterceptor.InstanceCount);

                service2 = instanceA;
            }

            Assert.AreEqual(service1, service2);
        }

        [TestMethod]
        [DataRow(0, 0, DisplayName = "When class is registered as singleton type, and interceptor is registered as transient type, the proxy service is created only one time, and the interceptor is created every time.")]
        [DataRow(0, 1, DisplayName = "When class is registered as singleton type, and interceptor is registered as transient factory, the proxy service is created only one time, and the interceptor is created every time.")]
        [DataRow(1, 0, DisplayName = "When class is registered as singleton factory, and interceptor is registered as transient type, the proxy service is created only one time, and the interceptor is created every time.")]
        [DataRow(1, 1, DisplayName = "When class is registered as singleton factory, and interceptor is registered as transient factory, the proxy service is created only one time, and the interceptor is created every time.")]
        [DataRow(2, 0, DisplayName = "When class is registered as singleton instance, and interceptor is registered as transient type, the proxy service is created only one time, and the interceptor is created every time.")]
        [DataRow(2, 1, DisplayName = "When class is registered as singleton instance, and interceptor is registered as transient factory, the proxy service is created only one time, and the interceptor is created every time.")]
        public void Class_Singleton_Interceptor_Transient(int interfaceRegisterType, int interceptorRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            switch (interfaceRegisterType)
            {
                case 0: services.AddSingleton<LifetimeTestService>(); break;
                case 1: services.AddSingleton(sp => new LifetimeTestService()); break;
                case 2: services.AddSingleton(new LifetimeTestService()); break;
            }

            switch (interceptorRegisterType)
            {
                case 0: services.AddTransient<LifetimeTestInterceptor>(); break;
                case 1: services.AddTransient(sp => new LifetimeTestInterceptor()); break;
            }

            services.EnableInterceptor<LifetimeTestService>(builder =>
            {
                builder.WithInterceptor<LifetimeTestInterceptor>();
            });

            ILifetimeTestService service1, service2;
            LifetimeTestInterceptor interceptor1, interceptor2;

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreEqual(instanceA, instanceB);
                Assert.AreNotEqual(interceptorA, interceptorB);

                Assert.AreEqual(2, LifetimeTestService.InstanceCount);
                Assert.AreEqual(3, LifetimeTestInterceptor.InstanceCount);
                service1 = instanceA;
                interceptor1 = interceptorA;
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreEqual(instanceA, instanceB);
                Assert.AreNotEqual(interceptorA, interceptorB);

                Assert.AreEqual(2, LifetimeTestService.InstanceCount);
                Assert.AreEqual(5, LifetimeTestInterceptor.InstanceCount);

                service2 = instanceA;
                interceptor2 = interceptorA;
            }

            Assert.AreEqual(service1, service2);
            Assert.AreNotEqual(interceptor1, interceptor2);
        }

        [TestMethod]
        [DataRow(0, 0, DisplayName = "When class is registered as singleton type, and interceptor is registered as scoped type, the proxy service is created only one time, and the interceptor is created every scope.")]
        [DataRow(0, 1, DisplayName = "When class is registered as singleton type, and interceptor is registered as scoped factory, the proxy service is created only one time, and the interceptor is created every scope.")]
        [DataRow(1, 0, DisplayName = "When class is registered as singleton factory, and interceptor is registered as scoped type, the proxy service is created only one time, and the interceptor is created every scope.")]
        [DataRow(1, 1, DisplayName = "When class is registered as singleton factory, and interceptor is registered as scoped factory, the proxy service is created only one time, and the interceptor is created every scope.")]
        [DataRow(2, 0, DisplayName = "When class is registered as singleton instance, and interceptor is registered as scoped type, the proxy service is created only one time, and the interceptor is created every scope.")]
        [DataRow(2, 1, DisplayName = "When class is registered as singleton instance, and interceptor is registered as scoped factory, the proxy service is created only one time, and the interceptor is created every scope.")]
        public void Class_Singleton_Interceptor_Scoped(int interfaceRegisterType, int interceptorRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            switch (interfaceRegisterType)
            {
                case 0: services.AddSingleton<LifetimeTestService>(); break;
                case 1: services.AddSingleton(sp => new LifetimeTestService()); break;
                case 2: services.AddSingleton(new LifetimeTestService()); break;
            }

            switch (interceptorRegisterType)
            {
                case 0: services.AddScoped<LifetimeTestInterceptor>(); break;
                case 1: services.AddScoped(sp => new LifetimeTestInterceptor()); break;
            }

            services.EnableInterceptor<LifetimeTestService>(builder =>
            {
                builder.WithInterceptor<LifetimeTestInterceptor>();
            });

            ILifetimeTestService service1, service2;
            LifetimeTestInterceptor interceptor1, interceptor2;

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreEqual(instanceA, instanceB);
                Assert.AreEqual(interceptorA, interceptorB);

                Assert.AreEqual(2, LifetimeTestService.InstanceCount);
                Assert.AreEqual(2, LifetimeTestInterceptor.InstanceCount);
                service1 = instanceA;
                interceptor1 = interceptorA;
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreEqual(instanceA, instanceB);
                Assert.AreEqual(interceptorA, interceptorB);

                Assert.AreEqual(2, LifetimeTestService.InstanceCount);
                Assert.AreEqual(3, LifetimeTestInterceptor.InstanceCount);

                service2 = instanceA;
                interceptor2 = interceptorA;
            }

            Assert.AreEqual(service1, service2);
            Assert.AreNotEqual(interceptor1, interceptor2);
        }

        [TestMethod]
        [DataRow(0, 0, DisplayName = "When class is registered as singleton type, and interceptor is registered as singleton type, the proxy service is created only one time, and the interceptor is created only one time.")]
        [DataRow(0, 1, DisplayName = "When class is registered as singleton type, and interceptor is registered as singleton factory, the proxy service is created only one time, and the interceptor is created only one time.")]
        [DataRow(0, 2, DisplayName = "When class is registered as singleton type, and interceptor is registered as singleton instance, the proxy service is created only one time, and the interceptor is created only one time.")]
        [DataRow(1, 0, DisplayName = "When class is registered as singleton factory, and interceptor is registered as singleton type, the proxy service is created only one time, and the interceptor is created only one time.")]
        [DataRow(1, 1, DisplayName = "When class is registered as singleton factory, and interceptor is registered as singleton factory, the proxy service is created only one time, and the interceptor is created only one time.")]
        [DataRow(1, 2, DisplayName = "When class is registered as singleton factory, and interceptor is registered as singleton instance, the proxy service is created only one time, and the interceptor is created only one time.")]
        [DataRow(2, 0, DisplayName = "When class is registered as singleton instance, and interceptor is registered as singleton type, the proxy service is created only one time, and the interceptor is created only one time.")]
        [DataRow(2, 1, DisplayName = "When class is registered as singleton instance, and interceptor is registered as singleton factory, the proxy service is created only one time, and the interceptor is created only one time.")]
        [DataRow(2, 2, DisplayName = "When class is registered as singleton instance, and interceptor is registered as singleton instance, the proxy service is created only one time, and the interceptor is created only one time.")]
        public void Class_Singleton_Interceptor_Singleton(int interfaceRegisterType, int interceptorRegisterType)
        {
            IServiceCollection services = new ServiceCollection();
            switch (interfaceRegisterType)
            {
                case 0: services.AddSingleton<LifetimeTestService>(); break;
                case 1: services.AddSingleton(sp => new LifetimeTestService()); break;
                case 2: services.AddSingleton(new LifetimeTestService()); break;
            }

            switch (interceptorRegisterType)
            {
                case 0: services.AddSingleton<LifetimeTestInterceptor>(); break;
                case 1: services.AddSingleton(sp => new LifetimeTestInterceptor()); break;
                case 2: services.AddSingleton(new LifetimeTestInterceptor()); break;
            }

            services.EnableInterceptor<LifetimeTestService>(builder =>
            {
                builder.WithInterceptor<LifetimeTestInterceptor>();
            });

            ILifetimeTestService service1, service2;
            LifetimeTestInterceptor interceptor1, interceptor2;

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreEqual(instanceA, instanceB);
                Assert.AreEqual(interceptorA, interceptorB);

                Assert.AreEqual(2, LifetimeTestService.InstanceCount);
                Assert.AreEqual(1, LifetimeTestInterceptor.InstanceCount);
                service1 = instanceA;
                interceptor1 = interceptorA;
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var instanceA = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var instanceB = scope.ServiceProvider.GetRequiredService<LifetimeTestService>();
                var interceptorA = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();
                var interceptorB = scope.ServiceProvider.GetRequiredService<LifetimeTestInterceptor>();

                Assert.AreEqual(instanceA, instanceB);
                Assert.AreEqual(interceptorA, interceptorB);

                Assert.AreEqual(2, LifetimeTestService.InstanceCount);
                Assert.AreEqual(1, LifetimeTestInterceptor.InstanceCount);

                service2 = instanceA;
                interceptor2 = interceptorA;
            }

            Assert.AreEqual(service1, service2);
            Assert.AreEqual(interceptor1, interceptor2);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            LifetimeTestInterceptor.InstanceCount = 0;
            LifetimeTestService.InstanceCount = 0;
        }
    }
}
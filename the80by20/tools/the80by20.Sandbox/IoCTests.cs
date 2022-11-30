using Microsoft.Extensions.DependencyInjection;

namespace the80by20.Sandbox;

public class IoCTests
{
    [Fact]
    public void SingletonTest()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<Service>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        var service1 = serviceProvider.GetService<Service>();
        var service2 = serviceProvider.GetService<Service>();
        
        Assert.Same(service2, service1);
    }
    
    [Fact]
    public void TransientTest()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<Service>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        var service1 = serviceProvider.GetService<Service>();
        var service2 = serviceProvider.GetService<Service>();
        
        Assert.NotSame(service2, service1);
    }
    
    [Fact]
    public void ScopedTest1()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<Service>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var service1 = serviceProvider.GetService<Service>();
        var service2 = serviceProvider.GetService<Service>();
        
        Assert.Same(service2, service1);
    }
    
    [Fact]
    public void ScopedTest2()
    {
        // INFO ServiceCollection
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<Service>();
    
        // INFO ServiceProvider
        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        // INFO IServiceScope
        using (var scope1 = serviceProvider.CreateScope())
        {
            var service1 = scope1.ServiceProvider.GetService<Service>();
            var service2 = scope1.ServiceProvider.GetService<Service>();
            Assert.Same(service2, service1);
        }
        
        using (var scope2 = serviceProvider.CreateScope())
        {
            var service1 = scope2.ServiceProvider.GetService<Service>();
            var service2 = scope2.ServiceProvider.GetService<Service>();
            Assert.Same(service2, service1);
        }
    }

    public class Service
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
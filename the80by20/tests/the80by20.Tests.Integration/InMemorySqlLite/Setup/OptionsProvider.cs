using Microsoft.Extensions.Configuration;

namespace the80by20.Tests.Integration.InMemorySqlLite.Setup;

public sealed class OptionsProvider
{
    private readonly IConfigurationRoot _configuration;

    public OptionsProvider()
    {
        _configuration = GetConfigurationRoot();
    }

    public T Get<T>(string sectionName) where T : class, new()
    {
        var options = new T();
        _configuration.GetSection(sectionName).Bind(options);
        return options;
    }

    private static IConfigurationRoot GetConfigurationRoot()
        => new ConfigurationBuilder()
            .AddJsonFile("appsettings.automatictests.json", true)
            .AddEnvironmentVariables()
            .Build();
}
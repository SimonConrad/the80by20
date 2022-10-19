﻿using Microsoft.Extensions.Configuration;
using the80by20.Shared.Infrastucture.Configuration;

namespace the80by20.Tests.Integration.Setup;

public sealed class OptionsProvider
{
    private readonly IConfigurationRoot _configuration;

    public OptionsProvider()
    {
        _configuration = GetConfigurationRoot();
    }

    public T Get<T>(string sectionName) where T : class, new() => _configuration.GetOptions<T>(sectionName);

    private static IConfigurationRoot GetConfigurationRoot()
        => new ConfigurationBuilder()
            .AddJsonFile("appsettings.automatictests.json", true)
            .AddEnvironmentVariables()
            .Build();
}
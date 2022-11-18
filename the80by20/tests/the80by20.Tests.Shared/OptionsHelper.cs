using Microsoft.Extensions.Configuration;

namespace the80by20.Tests.Shared
{
    public static class OptionsHelper
    {
        private const string AppSettings = "appsettings.automatictests.json";

        public static TOptions GetOptions<TOptions>(string sectionName) where TOptions : class, new()
        {
            var options = new TOptions();
            var configuration = GetConfigurationRoot();
            var section = configuration.GetSection(sectionName);
            section.Bind(options);

            return options;
        }

        public static IConfigurationRoot GetConfigurationRoot()
            => new ConfigurationBuilder()
                .AddJsonFile(AppSettings)
                .AddEnvironmentVariables()
                .Build();
    }
}
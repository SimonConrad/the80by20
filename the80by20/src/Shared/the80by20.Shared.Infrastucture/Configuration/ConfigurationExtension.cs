using Microsoft.Extensions.Configuration;

namespace the80by20.Shared.Infrastucture.Configuration
{
    public static class ConfigurationExtension
    {
        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            var section = configuration.GetRequiredSection(sectionName);
            section.Bind(options);

            return options;
        }
    }
}

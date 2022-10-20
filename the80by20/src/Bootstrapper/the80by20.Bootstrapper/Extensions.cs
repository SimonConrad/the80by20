namespace the80by20.Bootstrapper
{
    internal static class Extensions
    {
        public static IServiceCollection AddBootstrapper(this IServiceCollection services)
        {
            services.AddHostedService<DatabaseInitializer>();

            return services;
        }
    }
}

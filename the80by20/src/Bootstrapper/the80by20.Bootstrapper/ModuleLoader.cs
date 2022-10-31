using System.Reflection;
using the80by20.Shared.Abstractions.Modules;

namespace the80by20.Bootstrapper
{
    internal static class ModuleLoader
    {
        public static IList<Assembly> LoadAssemblies(IConfiguration configuration)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                 .Where(x =>( x.Contains("Masterdata") || x.Contains("Solution") || x.Contains("Users")) // todo fix this hack, without <RuntimeIdentifier>win-x64</RuntimeIdentifier> in bootstrapper csproj this code is not needed as far more less files
                 && !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
                 .ToList();

            files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

            return assemblies;
        }

        public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
           => assemblies
               .SelectMany(x => x.GetTypes())
               .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
               .OrderBy(x => x.Name)
               .Select(Activator.CreateInstance)
               .Cast<IModule>()
               .ToList();
    }
}

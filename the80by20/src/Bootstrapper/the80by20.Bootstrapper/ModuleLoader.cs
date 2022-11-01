using Serilog;
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
                 .Where(x =>
                  !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
                 .ToList();

            //var filesdlls = $"Files: {string.Join(Environment.NewLine, files.Select(x => x))}";
            //Log.Logger.Information($"Filesdlls: {filesdlls}");

            AddIfAssmebly(assemblies, files);

            return assemblies;
        }

        private static void AddIfAssmebly(List<Assembly> assemblies, List<string> files)
        {
            foreach (var file in files)
            {
                try
                {
                    var assemblyName = AssemblyName.GetAssemblyName(file);
                    var assembly = AppDomain.CurrentDomain.Load(assemblyName);
                    assemblies.Add(assembly);
                }
                catch (BadImageFormatException) // INFO https://learn.microsoft.com/en-us/dotnet/standard/assembly/identify
                {
                    continue;
                }
            }
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

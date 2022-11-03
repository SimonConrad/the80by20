using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using the80by20.Shared.Abstractions.Modules;

namespace the80by20.Bootstrapper
{
    internal static class ModuleLoader
    {
        const string modulePart = "the80by20.Modules.";

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

            FilterToAssemeblies(files);

            FilterToEnabledModules(configuration, files);

            foreach (var file in files)
            {
                var assemblyName = AssemblyName.GetAssemblyName(file);
                var assembly = AppDomain.CurrentDomain.Load(assemblyName);
                assemblies.Add(assembly);
            }

            return assemblies;
        }

        private static void FilterToEnabledModules(IConfiguration configuration, List<string> files)
        {
            var disabledModules = new List<string>();
            foreach (var file in files)
            {
                if (!file.Contains(modulePart))
                {
                    continue;
                }

                var moduleName = file.Split(modulePart)[1].Split(".")[0].ToLowerInvariant();
                var enabled = configuration.GetValue<bool>($"{moduleName}:module:enabled");
                if (!enabled)
                {
                    disabledModules.Add(file);
                }
            }

            foreach (var disabledModule in disabledModules)
            {
                files.Remove(disabledModule);
            }
        }

        private static void FilterToAssemeblies(List<string> files)
        {
            var notAssemblyFiles = new List<string>();
            foreach (var file in files)
            {
                if (!IsAssembly(file))
                {
                    notAssemblyFiles.Add(file);
                }
            }

            foreach (var notAssembly in notAssemblyFiles)
            {
                files.Remove(notAssembly);
            }
        }

        static bool IsAssembly(string path)
        {
            // INFO https://learn.microsoft.com/en-us/dotnet/standard/assembly/identify
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            // Try to read CLI metadata from the PE file.
            using var peReader = new PEReader(fs);

            if (!peReader.HasMetadata)
            {
                return false; // File does not have CLI metadata.
            }

            // Check that file has an assembly manifest.
            MetadataReader reader = peReader.GetMetadataReader();
            return reader.IsAssembly;
        }

        public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
           => assemblies
               .SelectMany(x => x.GetTypes())
               .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
               .OrderBy(x => x.Name)
               .Select(Activator.CreateInstance)
               .Cast<IModule>()
               .ToList();

        private static void AddIfAssmebly(List<Assembly> assemblies, List<string> files)
        {
            // INFO https://learn.microsoft.com/en-us/dotnet/standard/assembly/identify
            foreach (var file in files)
            {
                try
                {
                    var assemblyName = AssemblyName.GetAssemblyName(file);
                    var assembly = AppDomain.CurrentDomain.Load(assemblyName);
                    assemblies.Add(assembly);
                }
                catch (BadImageFormatException)
                {
                    continue;
                }
            }
        }
    }
}

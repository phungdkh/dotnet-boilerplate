using System.Reflection;
using SampleProject.Shared.Common.Constants;

namespace SampleProject.Shared.Helpers
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// Get all properties from type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<PropertyInfo> GetAllPropertiesOfType(Type type)
        {
            return [.. type.GetProperties()];
        }

        /// <summary>
        /// Get all property name from type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<string> GetAllPropertyNamesOfType(Type type)
        {
            var properties = type.GetProperties();
            return properties.Select(p => p.Name).ToList();
        }

        /// <summary>
        /// Get  assemblies
        /// </summary>
        /// <returns></returns>
        public static Assembly GetAssembly(string assemblyName)
        {
            var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assembly = currentAssemblies.FirstOrDefault(a => a.FullName != null && a.FullName.Contains(assemblyName, StringComparison.OrdinalIgnoreCase));

            return assembly ?? throw new InvalidOperationException($"Couldn't load the assembly: {assemblyName}");
        }

        /// <summary>
        /// Get  assemblies
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName != null
                && a.FullName.Contains(GlobalConstants.ASSEMBLY_NAME, StringComparison.OrdinalIgnoreCase)).ToArray();
        }

        public static TValue? GetAttributeValue<TAttribute, TValue>(this PropertyInfo prop, Func<TAttribute, TValue> value) where TAttribute : Attribute
        {
            if (prop.GetCustomAttributes(
                    typeof(TAttribute), true
                ).FirstOrDefault() is TAttribute att)
            {
                return value(att);
            }
            return default;
        }

        public static string LoadEmbeddedResource(string resourceName, string assemblyName)
        {
            var assembly = GetAssembly(assemblyName);

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream is null)
                return string.Empty;

            using var reader = new StreamReader(stream);
            var result = reader.ReadToEnd();
            return result;
        }

        public static IEnumerable<string> LoadEmbeddedResources(string assemblyName, string folder)
        {
            var contents = new List<string>();
            var assembly = GetAssembly(assemblyName);
            var resourcePaths = assembly.GetManifestResourceNames().Where(s => s.Contains(folder, StringComparison.OrdinalIgnoreCase));
            foreach (var resourcePath in resourcePaths)
            {
                contents.Add(LoadEmbeddedResource(resourcePath, assemblyName));
            }
            return contents;
        }
    }
}

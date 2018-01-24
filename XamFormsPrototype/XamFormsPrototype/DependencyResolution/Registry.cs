using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XamFormsPrototype.Contracts;

namespace XamFormsPrototype.DependencyResolution
{
    public abstract class Registry
    {
        public abstract IEnumerable<RegistryEntry> Entries { get; }

        public IEnumerable<RegistryEntry> AddViewModels(Assembly assembly = null)
        {
            var viewModels = (assembly != null ? assembly : Assembly.GetAssembly(GetType()))
                .GetTypes()
                .Where(_ => typeof(IViewModel).IsAssignableFrom(_) && !_.IsInterface);

            foreach (var vm in viewModels)
            {
                yield return new RegistryEntry
                {
                    IsSingleton = true,
                    Interface = vm,
                    Implementation = vm
                };
            }
        }
    }
}

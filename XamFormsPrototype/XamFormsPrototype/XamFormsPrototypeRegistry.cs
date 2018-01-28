using System.Collections.Generic;
using XamFormsPrototype.Contracts;
using XamFormsPrototype.DependencyResolution;

namespace XamFormsPrototype
{
    public class XamFormsPrototypeRegistry : Registry
    {
        private List<RegistryEntry> _entries;

        public XamFormsPrototypeRegistry()
        {
            _entries = new List<RegistryEntry>
                {
                    new RegistryEntry
                    {
                        Implementation = typeof(Repository.Repository),
                        Interface = typeof(IRepository)
                    }
                };
            _entries.AddRange(AddViewModels());
        }

        public override IEnumerable<RegistryEntry> Entries => _entries;
    }
}

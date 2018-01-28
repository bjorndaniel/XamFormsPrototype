using System.Collections.Generic;
using System.Reflection;
using XamFormsPrototype.Contracts;
using XamFormsPrototype.DependencyResolution;

namespace XamFormsPrototype.Tests.Helpers
{
    public class TestRegistry : Registry
    {
        private List<RegistryEntry> _entries;

        public TestRegistry()
        {
            _entries = new List<RegistryEntry>
                {
                    new RegistryEntry
                    {
                        Implementation = typeof(TestClass),
                        Interface = typeof(ITestClass)
                    },
                    new RegistryEntry
                    {
                        Implementation = typeof(TestClass2),
                        Interface = typeof(ITestClass2),
                    },
                    new RegistryEntry
                    {
                        Implementation = typeof(TestFileHelper),
                        Interface = typeof(IFileHelper),
                    },
                    new RegistryEntry
                    {
                        Implementation = typeof(Repository.Repository),
                        Interface = typeof(IRepository),
                    }
                };
            _entries.AddRange(AddViewModels(Assembly.GetAssembly(typeof(IViewModel))));
        }

        public override IEnumerable<RegistryEntry> Entries => _entries;

    }

}

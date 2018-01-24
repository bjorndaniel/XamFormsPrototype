using System.Collections.Generic;

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
                        ConstructorParameter = ("name", "I am the implementation of ITestClass with constructor parameter")
                    },
                    new RegistryEntry
                    {
                        Implementation = typeof(TestFileHelper),
                        Interface = typeof(IFileHelper),
                    },
                    new RegistryEntry
                    {
                        Implementation = typeof(xSonicRepository),
                        Interface = typeof(IRepository),
                    }
                };
            _entries.AddRange(AddViewModels(Assembly.GetAssembly(typeof(IViewModel))));
        }

        public override IEnumerable<RegistryEntry> Entries => _entries;

    }

}

using System;

namespace XamFormsPrototype.DependencyResolution
{
    public class RegistryEntry
    {
        public bool IsSingleton { get; set; }

        public Type Interface { get; set; }

        public Type Implementation { get; set; }

        public (string name, object value) ConstructorParameter { get; set; }

        public bool HasConstructorParameter => ConstructorParameter.value != null;
    }
}

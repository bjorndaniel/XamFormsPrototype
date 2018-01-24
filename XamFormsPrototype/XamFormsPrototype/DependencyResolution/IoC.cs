using System;
using TinyIoC;

namespace XamFormsPrototype.DependencyResolution
{
    public class IoC
    {
        private TinyIoCContainer _container = TinyIoCContainer.Current;

        public void Init(Registry registry)
        {

            foreach (var entry in registry.Entries)
            {
                if (entry.IsSingleton)
                {
                    _container.Register(Activator.CreateInstance(entry.Implementation));
                }
                else
                {
                    _container.Register(entry.Interface, entry.Implementation);
                }
            }

        }

        public T Resolve<T>() where T : class =>
            _container.Resolve<T>();

        public void Register(Type iface, Type implementation)
        {
            _container.Register(iface, implementation);
        }
    }
}

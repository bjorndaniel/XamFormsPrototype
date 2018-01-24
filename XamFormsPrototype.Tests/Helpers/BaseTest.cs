using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamFormsPrototype.Tests.Helpers
{
    public abstract class BaseTest
    {
        public T Given<T>(Func<T> action) => action();

        public void Given(Action action) => action();

        public T When<T>(Func<T> action) => action();

        public void When(Action action) => action();

        public T Then<T>(Func<T> action) => action();

        public void Then(Action action) => action();

    }
}

using XamFormsPrototype.Tests.Helpers;
using XamFormsPrototype.UI.ViewModels;
using Xunit;

namespace XamFormsPrototype.Tests
{
    public class IoCTests : BaseTest
    {
        private DependencyResolution.IoC _container;

        [Fact]
        public void Can_Resolve_Dependency()
        {
            Given_a_valid_implementation_and_interface();
            var result = When_resolving<ITestClass>();
            Then_result_should_be_the_implementation(result);
        }

        [Fact]
        public void Can_Resolve_Dependency_With_Constructor_Parameters()
        {
            Given_a_valid_implementation_and_interface();
            var result = When_resolving<ITestClass2>();
            Then_result_should_be_the_implementation_with_constructor(result);
        }

        [Fact]
        public void Can_reflect_viewmodels()
        {
            Given_a_valid_implementation_and_interface();
            Then_viewmodels_should_be_registered();
        }

        private void Then_viewmodels_should_be_registered()
        {
            Assert.NotNull(_container.Resolve<LogInPageViewModel>());
            Assert.NotNull(_container.Resolve<SidebarPageViewModel>());
        }

        private void Then_result_should_be_the_implementation_with_constructor(object result)
        {
            Assert.True(result.GetType() == typeof(TestClass2));
            Assert.Equal("I am the implementation of ITestClass with constructor parameter", ((ITestClass2)result).GetName());
        }

        private void Then_result_should_be_the_implementation(object result)
        {
            Assert.True(result.GetType() == typeof(TestClass));
            Assert.Equal("I am the implementation of ITestClass", ((ITestClass)result).GetName());
        }

        private object When_resolving<T>() =>
            _container.Resolve<T>();

        private void Given_a_valid_implementation_and_interface()
        {
            _container = new IoC();
            _container.Init(new TestRegistry());
        }
    }
}

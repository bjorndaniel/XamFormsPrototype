using XamFormsPrototype.DependencyResolution;
using XamFormsPrototype.Tests.Helpers;
using XamFormsPrototype.UI.ViewModels;
using Xunit;

namespace XamFormsPrototype.Tests
{
    public class IoCTests : BaseTest
    {
        private IoC _container = new IoC();

        [Fact]
        public void Can_Resolve_Dependency()
        {
            Given(() => _container.Init(new TestRegistry()));
            var result = When(() => { return _container.Resolve<ITestClass>(); });
            Then_result_should_be_the_implementation(result);
        }

        [Fact]
        public void Can_reflect_viewmodels()
        {
            Given(() => _container.Init(new TestRegistry()));
            Then_viewmodels_should_be_registered();
        }

        private void Then_viewmodels_should_be_registered()
        {
            Assert.NotNull(_container.Resolve<LogInPageViewModel>());
            Assert.NotNull(_container.Resolve<SidebarPageViewModel>());
        }

        private void Then_result_should_be_the_implementation(object result)
        {
            Assert.True(result.GetType() == typeof(TestClass));
            Assert.Equal("I am the implementation of ITestClass", ((ITestClass)result).GetName());
        }
    }
}

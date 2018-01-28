using System.Linq;
using XamFormsPrototype.Contracts;
using XamFormsPrototype.Tests.Helpers;
using XamFormsPrototype.UI.ViewModels;
using Xunit;
using XamFormsPrototype.Helpers;

namespace XamFormsPrototype.Tests
{
    public class ViewModelTests : BaseTest
    {
        [Fact]
        public void Can_Validate_LoginViewModel()
        {
            var model = Given(AValidViewModel);
            var result = When(model.Validate);
            Then(() =>
            {
                Assert.True(model.Validate());
            });

            Given(() =>
            {
                model.Email.Value = "a@b";
                model.Age.Value = 10;
                model.Username.Value = string.Empty;
            });
            Then(() =>
            {
                Assert.False(model.Validate());
                Assert.Equal(3, model.GetErrorMessages().Count());
                Assert.Contains(model.GetErrorMessages(), _ => _.Equals("Not a valid email address"));
                Assert.Contains(model.GetErrorMessages(), _ => _.Equals("Username is required"));
                Assert.Contains(model.GetErrorMessages(), _ => _.Equals("Age must be over 15"));
            });
        }

        [Fact]
        public void Can_Get_Properties_To_Validate()
        {
            var result = Given(typeof(LogInPageViewModel).GetPropertiesWithInterface<IValidity>);
            Then(() =>
            {
                Assert.Equal(3, result.Count());
            });
        }

        [Fact]
        public void Can_Validate_Single_Property()
        {
            var model = Given(AValidViewModel);
            model.Username.Value = "";
            When(() => model.ValidateUsernameCommand.Execute(this));
            Then(() =>
            {
                Assert.False(model.Username.IsValid);
            });
        }

        public LogInPageViewModel AValidViewModel()
        {
            var model = new LogInPageViewModel();
            model.Username.Value = "TestUser";
            model.Age.Value = 25;
            model.Email.Value = "a@b.com";
            return model;
        }
    }
}

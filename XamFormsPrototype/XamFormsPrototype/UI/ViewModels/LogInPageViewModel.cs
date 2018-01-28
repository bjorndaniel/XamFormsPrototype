using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamFormsPrototype.Contracts;
using XamFormsPrototype.Enumerators;
using XamFormsPrototype.Helpers.Messages;
using XamFormsPrototype.UI.Validation;
using XamFormsPrototype.UI.Validation.Rules;

namespace XamFormsPrototype.UI.ViewModels
{
    public class LogInPageViewModel : ValidatableViewModel, IViewModel
    {
        private ValidatableObject<int?> _age = new ValidatableObject<int?>();
        private ValidatableObject<string> _email = new ValidatableObject<string>();
        private ValidatableObject<string> _username = new ValidatableObject<string>();

        public LogInPageViewModel()
        {
            Init();
        }

        public ValidatableObject<string> Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
            
        public ValidatableObject<string> Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public ValidatableObject<int?> Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value); }
        }

        public ICommand LogInCommand => new Command(() => LogIn());

        public ICommand ValidateUsernameCommand => new Command(() => Validate<LogInPageViewModel>(_ => _.Username));

        public ICommand ValidateEmailCommand => new Command(() => Validate<LogInPageViewModel>(_ => _.Email));

        public ICommand ValidateAgeCommand => new Command(() => Validate<LogInPageViewModel>(_ => _.Age));

        public async Task<bool> Initialize()
        {
            //TODO: Add settings to check if a saved userid exists
            return await Task.FromResult(true);
        }

        private void LogIn()
        {
            MessagingCenter.Send(new NavigationMessage { Page = Pages.Main }, string.Empty);
        }

        private void Init()
        {
            _username.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Username is required" });
            _email.Validations.Add(new IsValidEmailRule<string> { ValidationMessage = "Not a valid email address" });
            _age.Validations.Add(new IsValidAgeRule<int?> { ValidationMessage = "Age must be over 15" });
        }
    }
}

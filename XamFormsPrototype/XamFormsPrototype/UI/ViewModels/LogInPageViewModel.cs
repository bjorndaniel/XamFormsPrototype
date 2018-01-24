using MvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;
using XamFormsPrototype.Enumerators;
using XamFormsPrototype.Helpers.Messages;

namespace XamFormsPrototype.UI.ViewModels
{
    public class LogInPageViewModel : BaseViewModel
    {
        private int _age;
        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        public int Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value); }
        }

        public ICommand LogInCommand => new Command(() => LogIn());

        public void LogIn()
        {
            MessagingCenter.Send(new NavigationMessage { Page = Pages.Main}, string.Empty);
        }
    }
}

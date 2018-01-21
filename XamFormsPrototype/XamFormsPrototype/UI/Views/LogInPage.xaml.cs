using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamFormsPrototype.Helpers.Messages;
using XamFormsPrototype.UI.ViewModels;

namespace XamFormsPrototype.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogInPage : ContentPage
	{
		public LogInPage ()
		{
			InitializeComponent ();
            BindingContext = new LogInPageViewModel();
            MessagingCenter.Subscribe<AlertMessage>(this, string.Empty, (m) => DisplayAlert("Message from model", m.Message, "Ok"));
		}
	}
}
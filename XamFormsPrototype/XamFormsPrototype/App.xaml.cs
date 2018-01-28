using Xamarin.Forms;
using XamFormsPrototype.DependencyResolution;
using XamFormsPrototype.Enumerators;
using XamFormsPrototype.Helpers.Messages;
using XamFormsPrototype.UI.Views;

namespace XamFormsPrototype
{
    public partial class App : Application
	{
        public static IoC Container = new IoC();

        public App ()
		{
			InitializeComponent();
            Container.Init(new XamFormsPrototypeRegistry());
            MainPage = new LogInPage();
            MessagingCenter.Subscribe<NavigationMessage>(this, string.Empty, (m) =>
            {
                if (m.Page == Pages.Main)
                {
                    MainPage = new MainPage();
                }
            });
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

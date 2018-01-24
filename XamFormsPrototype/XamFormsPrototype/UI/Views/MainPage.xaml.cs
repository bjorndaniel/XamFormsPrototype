using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamFormsPrototype.Enumerators;
using XamFormsPrototype.Helpers.Messages;

namespace XamFormsPrototype.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<NavigationMessage>(this, string.Empty, (m) =>
            {
                Navigate(m);
            });
        }

        private void Navigate(NavigationMessage m)
        {
            Page page;
            switch (m.Page)
            {
                case Pages.Albums:
                    page = (Page)Activator.CreateInstance(typeof(AlbumsPage));
                    break;
                default:
                    page = (Page)Activator.CreateInstance(typeof(UsersPage));
                    break;
            }
            Detail = new NavigationPage(page);
            IsPresented = false;
        }
    }
}
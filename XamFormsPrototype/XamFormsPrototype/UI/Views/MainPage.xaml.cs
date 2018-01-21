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
                Page page;
                switch (m.Page)
                {
                    case Pages.Page2:
                        page = (Page)Activator.CreateInstance(typeof(Page2));
                        break;
                    default:
                        page = (Page)Activator.CreateInstance(typeof(Page1));
                        break;
                }
                Detail = new NavigationPage(page);
                IsPresented = false;
            });
        }
    }
}
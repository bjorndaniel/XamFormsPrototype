using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamFormsPrototype.UI.ViewModels;

namespace XamFormsPrototype.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SidebarPage : ContentPage
    {
        private SidebarPageViewModel _model;
        
        public SidebarPage()
        {
            InitializeComponent();
            _model = new SidebarPageViewModel();
            BindingContext = _model;
        }

        protected override async void OnAppearing()
        {
            await _model.Initialize();
            base.OnAppearing();
        }
    }
}
using ScanApp.ViewModels;
using Xamarin.Forms;

namespace ScanApp.Views
{
    public partial class ScanView : ContentPage
    {
        public ScanView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new ScanViewModel();
        }
    }
}
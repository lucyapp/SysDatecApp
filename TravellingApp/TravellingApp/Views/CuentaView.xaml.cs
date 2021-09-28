using ScanApp.ViewModels;
using Xamarin.Forms;

namespace ScanApp.Views
{
    public partial class CuentaView : ContentPage
    {
        public CuentaView()
        {
            InitializeComponent();
            BindingContext = new CuentaViewModel();
        }
    }
}
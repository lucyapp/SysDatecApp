using SysDatecScanApp.ViewModels;
using Xamarin.Forms;

namespace SysDatecScanApp.Views
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
using SysDatecScanApp.ViewModels;
using Xamarin.Forms;

namespace SysDatecScanApp.Views
{
    public partial class PagosView : ContentPage
    {
        public PagosView()
        {
            InitializeComponent();
            BindingContext = new PagosViewModel();
        }
    }
}
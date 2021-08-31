using SysDatecScanApp.ViewModels;
using Xamarin.Forms;

namespace SysDatecScanApp.Views
{
    public partial class ScanView : ContentPage
    {
        public ScanView()
        {
            InitializeComponent();
            BindingContext = new ScanViewModel();
        }
    }
}
using SysDatecScanApp.ViewModels;
using Xamarin.Forms;

namespace SysDatecScanApp.Views
{
    public partial class AyudaView : ContentPage
    {
        public AyudaView()
        {
            InitializeComponent();
            BindingContext = new AyudaViewModel();
        }
    }
}
using ScanApp.ViewModels;
using Xamarin.Forms;

namespace ScanApp.Views
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
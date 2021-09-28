using ScanApp.ViewModels;
using Xamarin.Forms;

namespace ScanApp.Views
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
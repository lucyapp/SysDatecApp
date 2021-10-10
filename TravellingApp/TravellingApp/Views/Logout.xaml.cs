using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScanApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Logout : ContentPage
    {
        public Logout()
        {
            InitializeComponent();
        }
        public void ActivarLoading()
        {
            popupLoadingView.IsVisible = true;
            activityIndicator.IsRunning = true;
        }

        public void DesactivarLoading()
        {
            popupLoadingView.IsVisible = false;
            activityIndicator.IsRunning = false;
        }

        protected override void OnAppearing()
        {

            if (Application.Current.Properties["IsLoggedIn"].Equals(true))
            {

                Device.BeginInvokeOnMainThread(async () =>
                 {

                     ActivarLoading();
                     await Task.Delay(8000).ConfigureAwait(true);
                     DesactivarLoading();
                     Application.Current.Properties.Clear();
                     Application.Current.Properties["IsLoggedIn"] = false;
                     Application.Current.MainPage = new LoginPage();

                 });

            }

            base.OnAppearing();
        }
    }
}
using SysDatecScanApp.Views;
using Xamarin.Forms;

namespace SysDatecScanApp
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }
        public App()
        {
            InitializeComponent();
            // MainPage = new AppShell();
            //MainPage = new LoginPage();


            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new AppShell());
            }




        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
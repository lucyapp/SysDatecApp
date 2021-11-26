using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using ScanApp.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ScanApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            Plugin.Media.CrossMedia.Current.Initialize();
            MainPage = new NavigationPage(new LoginPage());
                  

        }

        protected override void OnStart()
        {
            // Handle when your app starts


            var status = DevEnvExe_LocalStorage.PCLHelper.VerificarPermisos();

            if (status.Equals(true))
            {
                Console.WriteLine("");
            } else
            {
                Console.WriteLine("");

            }
            VersionTracking.Track();
           
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
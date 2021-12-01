using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using ScanApp.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using PermissionStatus = Xamarin.Essentials.PermissionStatus;

namespace ScanApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            _ = CheckAndRequestLocationPermission();                                                                                                                    
            Plugin.Media.CrossMedia.Current.Initialize();
            MainPage = new NavigationPage(new LoginPage());
                  

        }

        public async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                return status;
            }

            if (Permissions.ShouldShowRationale<Permissions.StorageWrite>())
            {
                // Prompt the user with additional information as to why the permission is needed
            }

            status = await Permissions.RequestAsync<Permissions.StorageWrite>();

            return status;
        }

        protected override void OnStart()
        {
            // Handle when your app starts


            var status = CheckAndRequestLocationPermission();

            Task.Delay(3000);
            //status = DevEnvExe_LocalStorage.PCLHelper.VerificarPermisos();
            if (status.Equals(true))
            {
                Console.WriteLine("");
                VersionTracking.Track();
            } else
            {
                Console.WriteLine(""); Task.Delay(3000);
                //new NavigationPage(new LoginPage());
                var status1 = CheckAndRequestLocationPermission();

                if (status1.Equals(true))
                { 
                }
                else {
                    //MainPage = new NavigationPage(new LoginPage());
                }

              
            }
           
           
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
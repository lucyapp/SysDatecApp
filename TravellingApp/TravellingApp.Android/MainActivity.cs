using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Octane.Xamarin.Forms.VideoPlayer.Android;
using SysDatecScanApp;
using Plugin.Permissions;
using Android.Support.V7.App;
using Android.Util;
using Android.Content;

namespace ScanApp.Droid
{
    [Activity(Label = "LucyApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
           
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
           
           
        }

       
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }


}
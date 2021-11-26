using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Octane.Xamarin.Forms.VideoPlayer.Android;
using Plugin.CurrentActivity;
using Plugin.Permissions;

namespace ScanApp.Droid
{
    [Activity(Label = "LucyApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");

            var p1= new (string, bool)[] { (Manifest.Permission.WriteExternalStorage, true) };
            var p2= new (string, bool)[] { (Manifest.Permission.ReadExternalStorage, true) };
            var p3= new (string, bool)[] { (Manifest.Permission.MediaContentControl, true) };
            
            var permissions = new string[] { 
                Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage 
            };
           
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            FormsVideoPlayer.Init();
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            LoadApplication(new App());


        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            RequestPermissions(permissions, 77);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }


}
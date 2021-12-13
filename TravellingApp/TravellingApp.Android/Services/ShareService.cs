using System;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Support.V4.Content;
using Plugin.CurrentActivity;
using TestAppSharePlugin.Droid.Services;
using TestAppSharePlugin.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.Dependency(typeof(ShareService))]
namespace TestAppSharePlugin.Droid.Services
{
    public class ShareService : Activity, IShareService
    {
        [Obsolete]
        public async void Share(string subject, string message, ImageSource imageSource)
        {
            Intent intent = new Intent(Intent.ActionSend);
            intent.PutExtra(Intent.ExtraSubject, subject);
            intent.PutExtra(Intent.ExtraText, message);

            intent.SetType("image/jpeg");

            IImageSourceHandler handler;
         
            if (imageSource is FileImageSource)
            {
                handler = new FileImageSourceHandler();
            }
            else if (imageSource is StreamImageSource)
            {
                handler = new StreamImagesourceHandler();
            }
            else if (imageSource is UriImageSource)
            {
                handler = new ImageLoaderSourceHandler();
            }
            else
            {
                throw new NotImplementedException();
            }


            var bitmap = await handler.LoadImageAsync(imageSource, CrossCurrentActivity.Current.Activity);

            Java.IO.File path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads
                + Java.IO.File.Separator + "hqdefault.jpg");

            using (System.IO.FileStream os = new System.IO.FileStream(path.AbsolutePath, System.IO.FileMode.Create))
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, os);
            }

            intent.AddFlags(ActivityFlags.GrantReadUriPermission);
            intent.AddFlags(ActivityFlags.GrantWriteUriPermission);
            try
            {
                //intent.PutExtra(Intent.ExtraStream, Uri.FromFile(path));
                intent.PutExtra(Intent.ExtraStream, FileProvider.GetUriForFile(CrossCurrentActivity.Current.Activity, "com.plugin.mediatest.fileprovider", path));
            }
            catch (Exception ex)
            {


            }
            CrossCurrentActivity.Current.Activity.StartActivity(Intent.CreateChooser(intent, "Share Image"));
        }
    }
}
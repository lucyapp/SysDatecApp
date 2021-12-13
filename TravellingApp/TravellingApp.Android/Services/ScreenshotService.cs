using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ScanApp.Droid;
using TestAppSharePlugin.Interfaces;
[assembly: Xamarin.Forms.Dependency(typeof(TestAppSharePlugin.Droid.Services.ScreenshotService))]

namespace TestAppSharePlugin.Droid.Services
{
    public class ScreenshotService : IScreenshotService
    {
        private Activity _currentActivity;
        public void SetActivity(Activity activity) => _currentActivity = activity;

        [Obsolete]
        public byte[] Capture()
        {
            //var rootView = _currentActivity.Window.DecorView.RootView;
            var activity = Xamarin.Forms.Forms.Context as MainActivity;
            if (activity == null)
            {
                return null;
            }
            var rootView = activity.Window.DecorView.RootView;

            using (var screenshot = Bitmap.CreateBitmap(
                                    rootView.Width,
                                    rootView.Height,
                                    Bitmap.Config.Argb8888))
            {
                var canvas = new Canvas(screenshot);
                rootView.Draw(canvas);

                using (var stream = new MemoryStream())
                {
                    screenshot.Compress(Bitmap.CompressFormat.Png, 90, stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
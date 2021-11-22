using Android.Content;
using OpenPdf;
using OpenPdf.Droid;
using System;
using System.IO;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace OpenPdf.Droid
{
    public class CustomWebViewRenderer : WebViewRenderer
    {

        public CustomWebViewRenderer(Context context) : base(context)
        {

        }
        private static string ForceHttps(string requestUrl)
        {
            var uri = new UriBuilder(requestUrl);

            var hadDefaultPort = uri.Uri.IsDefaultPort;
            uri.Scheme = Uri.UriSchemeHttps;
            uri.Port = hadDefaultPort ? -1 : uri.Port;

            return uri.ToString();
        }

        [Obsolete]
        protected override async void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);


            if (e.NewElement != null)
            {
                var customWebView = Element as CustomWebView;

                DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory);
                di.Attributes = FileAttributes.Normal;


                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                if (!customWebView.IsPdf)
                    Control.LoadUrl("file:///" + customWebView.Uri);
                else
                {
                    //Control.LoadUrl("file:///android_asset/pdfjs/web/viewer.html?file=" + customWebView.Uri);
                    Uri uri = new Uri(string.Format($"file:///android_asset/pdfjs/web/viewer.html?file={0}",
                                      string.Format(customWebView.Uri, WebUtility.UrlEncode(customWebView.Uri))));
                    // Control.LoadUrl(uri);
                    //uri.AbsolutePath = customWebView.Uri.ToString();
                    Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", customWebView.Uri));
                    //var AbrirArchivo = new UriBuilder() { Scheme = Uri.UriSchemeFile, Host = "", Path = "/data/user/0/com.plugin.mediatest/files/.local/share/2021-08-11.pdf" }.Uri.AbsoluteUri;

                    Control.LoadUrl(customWebView.Uri);
                    //Device.OpenUri(new Uri(customWebView.Uri));
                }
            }
        }
    }
}

using System.ComponentModel;

namespace OpenPdf
{
    public class WebViewPageViewModel : WebViewPageViewModelBase, INotifyPropertyChanged
    {

        public bool IsPdf { get; set; }
        public string Uri { get; set; }

        public WebViewPageViewModel(string uri, bool isPdf)
        {
            Uri = uri;
            IsPdf = isPdf;
        }
    }
}

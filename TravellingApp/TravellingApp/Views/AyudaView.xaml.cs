using ScanApp.ViewModels;
using System;
using Xamarin.Forms;

namespace ScanApp.Views
{
    public partial class AyudaView : ContentPage
    {
        public AyudaView()
        {
            InitializeComponent();
            BindingContext = new AyudaViewModel();
        }
        private void btnPopupButton_Clicked(object sender, EventArgs e)
        {
            //popupLoadingView.IsVisible = true;
           // activityIndicator.IsRunning = true;

        }

        
    }
}
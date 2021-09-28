using ScanApp.Data;
using ScanApp.Models;
using System;
using Xamarin.Forms;

namespace ScanApp.Views
{
    public partial class CarpetaListPage : ContentPage
    {
        public CarpetaListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            CarpetaItemDatabase database = await CarpetaItemDatabase.Instance;
            listView.ItemsSource = await database.GetItemsAsync();
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CarpetaItemPage
            {
                BindingContext = new CarpetaItemPage()
            });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new CarpetaItemPage
                {
                    BindingContext = e.SelectedItem as CarpetaItem
                });
            }
        }
    }
}

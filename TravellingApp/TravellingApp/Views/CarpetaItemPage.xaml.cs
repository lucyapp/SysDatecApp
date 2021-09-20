using System;
using SysDatecScanApp.Data;
using SysDatecScanApp.Models;
using Xamarin.Forms;

namespace SysDatecScanApp.Views
{
    public partial class CarpetaItemPage : ContentPage
    {
        public CarpetaItemPage()
        {
            InitializeComponent();
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var todoItem = (CarpetaItem)BindingContext;
            CarpetaItemDatabase database = await CarpetaItemDatabase.Instance;
            await database.SaveItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (CarpetaItem)BindingContext;
            CarpetaItemDatabase database = await CarpetaItemDatabase.Instance;
            await database.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

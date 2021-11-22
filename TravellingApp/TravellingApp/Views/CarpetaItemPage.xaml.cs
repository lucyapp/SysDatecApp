using PCLStorage;
using ScanApp.Data;
using ScanApp.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ScanApp.Views
{
    public partial class CarpetaItemPage : ContentPage
    {
        public CarpetaItemPage()
        {
            InitializeComponent();
        }

        public async Task<IFolder> CrearCarpetasEnDispositivo(string folderPath, string carpetaNueva)
        {

            IFolder folder = await FileSystem.Current.GetFolderFromPathAsync(folderPath);
            folder = await folder.CreateFolderAsync(carpetaNueva, CreationCollisionOption.ReplaceExisting);
            return folder;
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {

            var todoItem = new CarpetaItem();
            
            todoItem.Name = Nombre.Text;
            todoItem.Notes = Descripcion.Text;
            todoItem.Done = Publico.IsToggled;

            BindingContext = todoItem;
            CarpetaItemDatabase database = await CarpetaItemDatabase.Instance;
            Device.BeginInvokeOnMainThread(() =>
            {
                var seis = CrearCarpetasEnDispositivo("/storage/emulated/0/Pictures/SysDatec",Nombre.Text.Trim().ToString());
            });
            await database.SaveItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (CarpetaItem)BindingContext;
            CarpetaItemDatabase database = await CarpetaItemDatabase.Instance;
            string action = await DisplayActionSheet("Eliminar carpeta: Esta seguro?", "Cancelar", "Aceptar", todoItem.Name);
            if (action == "Aceptar")
            {
                string file = todoItem.Name;
                var elimando = await (_ = DevEnvExe_LocalStorage.PCLHelper.DeleteDirectory(file.Trim(), await FileSystem.Current.GetFolderFromPathAsync("/storage/emulated/0/Pictures/SysDatec/"))).ConfigureAwait(false);
                if (elimando == true)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Task.Delay(1000).ConfigureAwait(false);
                        _ = DisplayAlert("Eliminación", "El carpeta seleccionada ha sido eliminado", "Ok").ConfigureAwait(false);
                       
                    });

                    await database.DeleteItemAsync(todoItem);
                    await Navigation.PopAsync();

                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Task.Delay(1000).ConfigureAwait(false);
                        _ = DisplayAlert("Eliminación", "La carpeta no se pudo eliminar", "Ok").ConfigureAwait(false);
                    });
                }
            }
 }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

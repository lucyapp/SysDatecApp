using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Label = System.Reflection.Emit.Label;

using Xamarin.Forms.Xaml;
using PCLStorage;

namespace ScanApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformacionPersonal : ContentPage
    {
        //private string _myCity;
        //public List<City> CitiesList { get; set; }
        //public string MyCity
        //{
        //    get { return _myCity; }
        //    set
        //    {
        //        if (_myCity != value)
        //        {
        //            _myCity = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
        //private City _selectedCity { get; set; }
        //public City SelectedCity
        //{
        //    get { return _selectedCity; }
        //    set
        //    {
        //        if (_selectedCity != value)
        //        {
        //            _selectedCity = value;
        //            // Do whatever functionality you want...When a selectedItem is changed..
        //            // write code here..
        //            MyCity = "Selected City : " + _selectedCity.Value;
        //        }
        //    }
        //}
        public bool tieneSeleccionado { get; set; }
        public int cantidadDatos { get; set; }
        public InformacionPersonal()
        {
            InitializeComponent();
            //BindingContext = new InformacionPersonal();
            //tieneSeleccionado = false;
            //CitiesList = GetCities().OrderBy(t => t.Value).ToList();
            //MyCity = "Seleccione una ciudad";
        }
        public List<City> GetCities()
        {
            var cities = new List<City>()
                    {
                        new City(){Key =  1, Value= "Cali"},
                        new City(){Key =  2, Value= "Buenaventura"},
                        new City(){Key =  3, Value= "Bogota"},
                        new City(){Key =  4, Value= "Pasto"},
                        new City(){Key =  5, Value= "Medellin"},
                        new City(){Key =  6, Value= "Armenia"},
                        new City(){Key =  7, Value= "Cartago"}
                    };

            return cities;
        }
        public class City
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }

        public void Entrada6_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ValidarDatos_MostrarBoton();
            var ck = (CheckBox)sender;
            if (ck.IsChecked)
            {

                lbl7.IsVisible = true;
                Entrada7.IsVisible = true;
            }
            else
            {
                lbl7.IsVisible = false;
                Entrada7.IsVisible = false;
            }
            cantidadDatos++;
        }

        private void Entrada8_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ValidarDatos_MostrarBoton();
            var ck = (CheckBox)sender;
            if (ck.IsChecked)
            {

                lbl8.IsVisible = true;
                Entrada9.IsVisible = true;
            }
            else
            {
                lbl8.IsVisible = false;
                Entrada9.IsVisible = false;
            }
            cantidadDatos++;
        }

        private void Entrada10_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ValidarDatos_MostrarBoton();
            var ck = (CheckBox)sender;
            if (ck.IsChecked)
            {

                lbl9.IsVisible = true;
                Entrada11.IsVisible = true;
            }
            else
            {
                lbl9.IsVisible = false;
                Entrada11.IsVisible = false;
            }
            cantidadDatos++;
        }

        private void Entrada12_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ValidarDatos_MostrarBoton();
            var ck = (CheckBox)sender;
            if (ck.IsChecked)
            {

                lbl10.IsVisible = true;
                Entrada13.IsVisible = true;
            }
            else
            {
                lbl10.IsVisible = false;
                Entrada13.IsVisible = false;
            }
            cantidadDatos++;
        }

        private void Entrada14_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ValidarDatos_MostrarBoton();
            var ck = (CheckBox)sender;
            if (ck.IsChecked)
            {

                lbl11.IsVisible = true;
                Entrada15.IsVisible = true;
            }
            else
            {
                lbl11.IsVisible = false;
                Entrada15.IsVisible = false;
            }
            cantidadDatos++;
        }

        private void Entrada16_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ValidarDatos_MostrarBoton();
            var ck = (CheckBox)sender;
            if (ck.IsChecked)
            {

                lbl12.IsVisible = true;
                Entrada17.IsVisible = true;
            }
            else
            {
                lbl12.IsVisible = false;
                Entrada17.IsVisible = false;
            }
            cantidadDatos++;
        }

        private void Entrada18_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ValidarDatos_MostrarBoton();
            var ck = (CheckBox)sender;
            if (ck.IsChecked)
            {

                lbl13.IsVisible = true;
                Entrada19.IsVisible = true;
            }
            else
            {
                lbl13.IsVisible = false;
                Entrada19.IsVisible = false;
            }
            cantidadDatos++;
        }

        private void Entrada20_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ValidarDatos_MostrarBoton();
            var ck = (CheckBox)sender;
            if (ck.IsChecked)
            {
                lbl14.IsVisible = true;
                Entrada21.IsVisible = true;
            }
            else
            {
                lbl14.IsVisible = false;
                Entrada21.IsVisible = false;
            }
            cantidadDatos++;
        }

        private void Entrada22_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ValidarDatos_MostrarBoton();
            var ck = (CheckBox)sender;
            if (ck.IsChecked)
            {
                lbl15.IsVisible = true;
                Entrada23.IsVisible = true;
            }
            else
            {
                lbl15.IsVisible = false;
                Entrada23.IsVisible = false;
            }
            cantidadDatos++;
        }

        private void Entrada1_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Application.Current.Properties["Nombre"] = ConvertirPrimeraLetraMayusculaString(Entrada1.Text.ToUpper());
            }
            catch (Exception)
            {
            }
        }

        private void Entrada2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Application.Current.Properties["Apellido"] = ConvertirPrimeraLetraMayusculaString(Entrada2.Text.ToUpper());
            }
            catch (Exception)
            {
            }
        }

        private void Entrada3_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Application.Current.Properties["Edad"] = ConvertirPrimeraLetraMayusculaString(Entrada3.Text.ToUpper());
            }
            catch (Exception)
            {
            }
        }

        private void Entrada4_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Application.Current.Properties["Direccion"] = ConvertirPrimeraLetraMayusculaString(Entrada4.Text.ToUpper());
            }
            catch (Exception)
            {

            }
        }
        public List<string> Convertir_StringSplit_ToList(string Lista)
        {
            List<string> stringList = Lista.Split(',').ToList();
            return stringList;
        }

        public String ConvertirPrimeraLetraMayusculaString(string Cadena)
        {
            var AuxCadena = Cadena.Substring(0, 1).ToUpper() + Cadena.Substring(1).ToLower();
            return AuxCadena;
        }

        private void Entrada5_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Application.Current.Properties["Email"] = ConvertirPrimeraLetraMayusculaString(Entrada5.Text.ToUpper());
            }
            catch (Exception)
            {
            }
        }

      
        public async Task<IFolder> CrearCarpetasEnDispositivo(string folderPath, string carpetaNueva )
        {

            //IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(folderPath );
            //IFolder folder = await rootFolder.CreateFolderAsync("/storage/emulated/0/Pictures/SysDatec/"+carpetaNueva, CreationCollisionOption.OpenIfExists); 
            //IFile file = await folder.CreateFileAsync("archivopdf.pdf", CreationCollisionOption.ReplaceExisting);
            //IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(folderPath );
            IFolder folder = await FileSystem .Current.GetFolderFromPathAsync(folderPath);
            folder = await folder.CreateFolderAsync(carpetaNueva, CreationCollisionOption.ReplaceExisting);
            return folder;
        } 

        private void Entrada11_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var Result = Convertir_StringSplit_ToList(Entrada11.Text);
                Application.Current.Properties["PagaServicios"] = Result;
            }
            catch (Exception)
            {
            }
        }

        private void Entrada13_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var Result = Convertir_StringSplit_ToList(Entrada13.Text);
                Application.Current.Properties["CreditoViviendaArriendo"] = Result;
            }
            catch (Exception)
            {
            }
        }

        private void Entrada23_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var Result = Convertir_StringSplit_ToList(Entrada23.Text);
                Application.Current.Properties["PoseeVehiculo"] = Result;
            }
            catch (Exception)
            {
            }
        }

        private void Entrada15_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var Result = Convertir_StringSplit_ToList(Entrada15.Text);
                Application.Current.Properties["CreditoVehicular"] = Result;
            }
            catch (Exception)
            {
            }
        }

        private void Entrada17_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var Result = Convertir_StringSplit_ToList(Entrada17.Text);
                Application.Current.Properties["EntidadesFinancieras"] = Result;
            }
            catch (Exception)
            {
            }
        }

        private void Entrada7_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var Result = Convertir_StringSplit_ToList(Entrada7.Text);
                Application.Current.Properties["TieneHijos"] = Result;
            }
            catch (Exception)
            {
            }
        }

        private void Entrada9_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var Result = Convertir_StringSplit_ToList(Entrada9.Text);
                Application.Current.Properties["TieneMascotas"] = Result;
            }
            catch (Exception)
            {
            }
        }

        private void Entrada19_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var Result = Convertir_StringSplit_ToList(Entrada19.Text);
                Application.Current.Properties["EducacionFormal"] = Result;
            }
            catch (Exception)
            {
            }
        }

        private void Entrada21_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var Result = Convertir_StringSplit_ToList(Entrada21.Text);
                Application.Current.Properties["Labora"] = Result;
            }
            catch (Exception)
            {
            }
        }

        public async Task<IFolder> ListaGuardarDispositivoAsync(List<string> Lista, string opcion, string path)
        {

            int count = Lista.Count();
            string carpeta = "";
            for (int j = 0; j < count; j++)
            {
                carpeta = Lista[j];

                return await CrearCarpetasEnDispositivo(path, carpeta);
            }
            return null;
        }



        private void btnGuardarInformacionPersonal_Clicked(object sender, EventArgs e)
        {
            if (Application.Current.Properties["PagaServicios"].ToString() != null)
            {
                var uno = CrearCarpetasEnDispositivo("/storage/emulated/0/Pictures/SysDatec", "Servicios");
                var Result = Convertir_StringSplit_ToList(Entrada11.Text);
                _ = ListaGuardarDispositivoAsync(Result, "Servicios", "/storage/emulated/0/Pictures/SysDatec/Servicios");
            }
            
            if (Application.Current.Properties["CreditoViviendaArriendo"].ToString() != null)
            {
                var dos = CrearCarpetasEnDispositivo("/storage/emulated/0/Pictures/SysDatec", "CDT-Vivienda");
                var Result = Convertir_StringSplit_ToList(Entrada13.Text);
                _ = ListaGuardarDispositivoAsync(Result, "CDT-Vivienda", "/storage/emulated/0/Pictures/SysDatec/CDT-Vivienda");
            }
            
            if (Application.Current.Properties["PoseeVehiculo"].ToString() != null)
            {
                var tres = CrearCarpetasEnDispositivo("/storage/emulated/0/Pictures/SysDatec", "Vehiculos");
                var Result = Convertir_StringSplit_ToList(Entrada23.Text);
                _ = ListaGuardarDispositivoAsync(Result, "Vehiculos", "/storage/emulated/0/Pictures/SysDatec/Vehiculos");
            }
            
            if (Application.Current.Properties["CreditoVehicular"].ToString() != null)
            {
                var cuatro = CrearCarpetasEnDispositivo("/storage/emulated/0/Pictures/SysDatec/Vehiculos", "CDT-Vehiculos");
                var Result = Convertir_StringSplit_ToList(Entrada15.Text);
                _ = ListaGuardarDispositivoAsync(Result, "CDT-Vehiculos", "/storage/emulated/0/Pictures/SysDatec/Vehiculos/CDT-Vehiculos");
            }
            
            if (Application.Current.Properties["EntidadesFinancieras"].ToString() != null)
            {
                var cinco = CrearCarpetasEnDispositivo("/storage/emulated/0/Pictures/SysDatec", "ENT-Financieras");
                var Result = Convertir_StringSplit_ToList(Entrada17.Text);
                _ = ListaGuardarDispositivoAsync(Result, "ENT-Financieras", "/storage/emulated/0/Pictures/SysDatec/ENT-Financieras");
            }
            
            if (Application.Current.Properties["TieneHijos"].ToString() != null)
            {
                var seis = CrearCarpetasEnDispositivo("/storage/emulated/0/Pictures/SysDatec", "Hijos");
                var Result = Convertir_StringSplit_ToList(Entrada7.Text);
                _ = ListaGuardarDispositivoAsync(Result, "Hijos", "/storage/emulated/0/Pictures/SysDatec/Hijos");
            }
            
            if (Application.Current.Properties["TieneMascotas"].ToString() != null)
            {
                var siete = CrearCarpetasEnDispositivo("/storage/emulated/0/Pictures/SysDatec", "Mascotas");
                var Result = Convertir_StringSplit_ToList(Entrada9.Text);
                _ = ListaGuardarDispositivoAsync(Result, "Mascotas", "/storage/emulated/0/Pictures/SysDatec/Mascotas");
            }
            
            if (Application.Current.Properties["EducacionFormal"].ToString() != null)
            {
                var ocho = CrearCarpetasEnDispositivo("/storage/emulated/0/Pictures/SysDatec", "ACA-Educacion");
                var Result = Convertir_StringSplit_ToList(Entrada19.Text);
                _ = ListaGuardarDispositivoAsync(Result, "ACA-Educacion", "/storage/emulated/0/Pictures/SysDatec/ACA-Educacion");
            }
            
            if (Application.Current.Properties["Labora"].ToString() != null)
            {
                var nueve = CrearCarpetasEnDispositivo("/storage/emulated/0/Pictures/SysDatec", "SIT-Laboral");
                var Result = Convertir_StringSplit_ToList(Entrada21.Text);
                _ = ListaGuardarDispositivoAsync(Result, "SIT-Laboral", "/storage/emulated/0/Pictures/SysDatec/SIT-Laboral");

            }

            try {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _ = DisplayAlert("Infomacion", "El usuario '" + Application.Current.Properties["Nombre"] + " " + Application.Current.Properties["Apellido"] + "' ya guardo sus datos.", "Ok").ConfigureAwait(false);
                        await Task.Delay(1000).ConfigureAwait(true);
                       

                    });
                } catch (Exception) 
                {
                    _ = DisplayAlert("Error", "Ha ocurrido un error al procesar la informacion, repita nuevamente!" , "Ok").ConfigureAwait(false);
                    Application.Current.MainPage = new InformacionPersonal();
                }
          
        }


        private void ValidarDatos_MostrarBoton()
        {
            if (cantidadDatos >= 9)
            {
                btnGuardarInformacionPersonal.IsVisible = true;
                
            }
           
        }
    }
}
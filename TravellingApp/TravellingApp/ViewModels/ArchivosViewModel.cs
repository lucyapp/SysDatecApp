using ScanApp.Models;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ScanApp.ViewModels
{
    public class ArchivosViewModel : BindableObject
    {
        public ObservableCollection<ArchivosRecientes> ArchivosRecientes { get; set; }
        public ObservableCollection<NombresCarpetas> NombresCarpetas { get; set; }
        public string Nombre { get; }

        public ArchivosViewModel()
        {

            Nombre = Application.Current.Properties["name"].ToString();


            NombresCarpetas = new ObservableCollection<NombresCarpetas>
                {
                   new NombresCarpetas { Name="SysDatec", FechaCreacion=DateTime.Now, Picture="carpeta", CantidadArchivos=4 },
                   new NombresCarpetas { Name="Hijos", FechaCreacion=DateTime.Now, Picture="carpeta", CantidadArchivos=3 },
                   new NombresCarpetas { Name="Mascotas", FechaCreacion=DateTime.Now, Picture="carpeta", CantidadArchivos=2 },
                   new NombresCarpetas { Name="Servicios", FechaCreacion=DateTime.Now, Picture="carpeta", CantidadArchivos=2 }
                };

            ArchivosRecientes = new ObservableCollection<ArchivosRecientes>
            {
                new ArchivosRecientes { Name="Empopasto", Description="Agua", Picture="file", Fecha=DateTime.Now },
                new ArchivosRecientes { Name="Energia del valle", Description="Luz", Picture="file" , Fecha=DateTime.Now }
            };
        }
    }
}
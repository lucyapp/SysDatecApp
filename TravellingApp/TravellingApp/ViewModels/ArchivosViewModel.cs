using SysDatecScanApp.Models;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace SysDatecScanApp.ViewModels
{
    public class ArchivosViewModel : BindableObject
    {
        public ObservableCollection<ArchivosRecientes> ArchivosRecientes { get; set; }
        public ObservableCollection<NombresCarpetas> NombresCarpetas { get; set; }
        public ArchivosViewModel()
        {
            NombresCarpetas = new ObservableCollection<NombresCarpetas>
                {
                   new NombresCarpetas { Name="NombreCarpeta0", FechaCreacion=DateTime.Now, Picture="Model1", CantidadArchivos=4 },
                   new NombresCarpetas { Name="NombreCarpeta1", FechaCreacion=DateTime.Now, Picture="Model2", CantidadArchivos=3 },
                   new NombresCarpetas { Name="NombreCarpeta2", FechaCreacion=DateTime.Now, Picture="Model3", CantidadArchivos=2 }
                };

            ArchivosRecientes = new ObservableCollection<ArchivosRecientes>
            {
                new ArchivosRecientes { Name="NombreArchivo3", Description="Agua", Picture="Model2", Fecha=DateTime.Now },
                new ArchivosRecientes { Name="NombreArchivo4", Description="Luz", Picture="Model3" , Fecha=DateTime.Now }
            };
        }
    }
}
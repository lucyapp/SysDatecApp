using SysDatecScanApp.Models;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using System.IO;


namespace SysDatecScanApp.ViewModels
{
    public class ArchivosViewModel : BindableObject
    {
        public ObservableCollection<ArchivosRecientes> ArchivosRecientes { get; set; }
        public ObservableCollection<NombresCarpetas> NombresCarpetas { get; set; }


        public ArchivosViewModel()
{
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)+"/Pictures"; // Documents folder  
            var path = Path.Combine(documentsPath, "/SysDatec/");

            if (!System.IO.Directory.Exists(documentsPath+path))
            {
                Console.WriteLine("\n carpeta ya Sysdatec ya existe: "+ path);
                //crear carpetas las otras
                //listar las carpetas dentro de sysdatec
                //mirar los archivos que existen
            }
            else {
               //aqui arrojar mensaje de que no existe y que se va crear
               //verificar si en la nube hay datos de usuario de documentos y carpetas 

               Console.WriteLine("\ncarpeta no existente SysDatec : " + path);
                
                
            }
            

            NombresCarpetas = new ObservableCollection<NombresCarpetas>
                {
                   new NombresCarpetas { Name="NombreCarpeta0", FechaCreacion=DateTime.Now, Picture="carpeta", CantidadArchivos=4 },
                   new NombresCarpetas { Name="NombreCarpeta1", FechaCreacion=DateTime.Now, Picture="carpeta", CantidadArchivos=3 },
                   new NombresCarpetas { Name="NombreCarpeta2", FechaCreacion=DateTime.Now, Picture="carpeta", CantidadArchivos=2 }
                };

    ArchivosRecientes = new ObservableCollection<ArchivosRecientes>
            {
                new ArchivosRecientes { Name="NombreArchivo3", Description="Agua", Picture="file", Fecha=DateTime.Now },
                new ArchivosRecientes { Name="NombreArchivo4", Description="Luz", Picture="file2" , Fecha=DateTime.Now }
            };
}
    }
}
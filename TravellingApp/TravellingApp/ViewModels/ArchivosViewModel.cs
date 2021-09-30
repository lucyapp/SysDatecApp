using ScanApp.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;

namespace ScanApp.ViewModels
{
    public class ArchivosViewModel : BindableObject
    {
        public ObservableCollection<ArchivosRecientes> ArchivosRecientes { get; set; }
        public ObservableCollection<NombresCarpetas> NombresCarpetas { get; set; }

        ObservableCollection<ArchivosRecientes> ListaArchivosSysdatec = new ObservableCollection<ArchivosRecientes>();
        ObservableCollection<NombresCarpetas> ListaCarpetasSysdatec = new ObservableCollection<NombresCarpetas>();

        public string Nombre { get; }
        public string FileImage { get; }
        public DirectoryInfo[] dirs { get; }
        public ArchivosViewModel()
        {

            Nombre = Application.Current.Properties["name"].ToString();

            DirectoryInfo di = new DirectoryInfo("/storage/emulated/0/Pictures/SysDatec/");
            Console.WriteLine("No search pattern returns:");
            
           foreach (var fi in di.GetFiles())
            {
                if (fi.Extension.Contains("jpg"))
                {
                    FileImage = "jpg";
                }
                else if (fi.Extension.Contains("png"))
                {
                    FileImage = "png";
                }
                else if (fi.Extension.Contains("pdf"))
                {
                    FileImage = "pdf";
                }
                else {
                    FileImage = "file";
                }
               
                ListaArchivosSysdatec.Add(new ArchivosRecientes() { Name = fi.Name, Fecha= fi.CreationTime, Picture= FileImage, Description = fi.Extension });
                FileImage = "";
                Console.WriteLine(fi.Name);
            }

            try
            {
                dirs = di.GetDirectories();
                Console.WriteLine("el numero de directorios encontrados es de {0}.", dirs.Length);

                foreach (DirectoryInfo fi in dirs)
                {
                    ListaCarpetasSysdatec.Add(new NombresCarpetas() { Name = fi.Name, FechaCreacion = fi.CreationTime, Picture = "carpeta", CantidadArchivos = fi.GetFiles().Length });
                    Console.WriteLine(fi.Name);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("El proceso fallo y no hay carpetas en SysDatec directorio, se mostrara el Directorio raiz {0}", e.ToString());
                

            }

            //Console.WriteLine("realizar filtros de los archivos");
            //foreach (var fi in di.GetFiles("*", SearchOption.AllDirectories))
            //{
            //    Console.WriteLine(fi.Name);
            //}

            if (dirs.Length<=0) 
            {
                ListaCarpetasSysdatec.Add(new NombresCarpetas() { Name = "SysDatec", FechaCreacion = di.CreationTime, Picture = "carpeta", CantidadArchivos = di.GetFiles().Length });
            }
           
            NombresCarpetas = new ObservableCollection<NombresCarpetas>(ListaCarpetasSysdatec);
            ArchivosRecientes = new ObservableCollection<ArchivosRecientes>(ListaArchivosSysdatec);
            
        }
    }
}
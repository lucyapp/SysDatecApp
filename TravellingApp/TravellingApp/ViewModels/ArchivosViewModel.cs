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

        public ArchivosViewModel()
        {

            Nombre = Application.Current.Properties["name"].ToString();

            DirectoryInfo di = new DirectoryInfo("/storage/emulated/0/Pictures/SysDatec/");
            Console.WriteLine("No search pattern returns:");

            foreach (var fi in di.GetFiles())
            {
               
                ListaArchivosSysdatec.Add(new ArchivosRecientes() { Name = fi.Name, Fecha= fi.CreationTime, Picture= "file", Description = fi.Extension });
                Console.WriteLine(fi.Name);
            }

            try
            {
                DirectoryInfo[] dirs = di.GetDirectories();
                Console.WriteLine("The number of directories containing the letter p is {0}.", dirs.Length);

                foreach (DirectoryInfo fi in dirs)
                {
                    ListaCarpetasSysdatec.Add(new NombresCarpetas() { Name = fi.Name, FechaCreacion = fi.CreationTime, Picture = "carpeta", CantidadArchivos = fi.GetFiles().Length });
                    Console.WriteLine(fi.Name);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            //Console.WriteLine("realizar filtros de los archivos");
            //foreach (var fi in di.GetFiles("*", SearchOption.AllDirectories))
            //{
            //    Console.WriteLine(fi.Name);
            //}

            NombresCarpetas = new ObservableCollection<NombresCarpetas>(ListaCarpetasSysdatec);
            ArchivosRecientes = new ObservableCollection<ArchivosRecientes>(ListaArchivosSysdatec);
            
        }
    }
}
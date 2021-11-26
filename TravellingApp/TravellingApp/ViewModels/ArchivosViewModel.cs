using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using ScanApp.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;

namespace ScanApp.ViewModels
{
    public class ArchivosViewModel : BindableObject, INavigationAware
    {
        //navegacion
        private readonly IPageDialogService _dialogService;
        private readonly IEventAggregator _ea;
        private readonly IDialogService _PopUpService;
        private readonly INavigationService _navigationService;

        public static ArchivosViewModel Instance;
        public DelegateCommand ImageSeleccionCommand { get; set; }
        //fin navegacion

        public ObservableCollection<ArchivosRecientes> ArchivosRecientes { get; set; }
        public ObservableCollection<NombresCarpetas> NombresCarpetas { get; set; }

        ObservableCollection<ArchivosRecientes> ListaArchivosSysdatec = new ObservableCollection<ArchivosRecientes>();
        ObservableCollection<NombresCarpetas> ListaCarpetasSysdatec = new ObservableCollection<NombresCarpetas>();

        public string Nombre { get; }
        public string FileImage { get; }
        public string NameImage { get; }
        public DirectoryInfo[] dirs { get; }

        public ArchivosViewModel(INavigationService navigationService, IPageDialogService dialogService, IEventAggregator ea, IDialogService PopUpService)
        {
            _ea = ea;
            _navigationService = navigationService;
            _dialogService = dialogService;
            _PopUpService = PopUpService;
            Instance = this;
            ImageSeleccionCommand = new DelegateCommand(OnSelectActionImage);
        }

        private void OnSelectActionImage()
        {
            Console.WriteLine("");
        }

        [Obsolete]
        public ArchivosViewModel()
        {

            Nombre = Application.Current.Properties["name"].ToString();
            var p1 = CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            var r1 = CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);

            DirectoryInfo di = new DirectoryInfo("/storage/emulated/0/Pictures/SysDatec/");
            Console.WriteLine("No search pattern returns:");
            try
            {
                dirs = di.GetDirectories();
                Console.WriteLine("el numero de directorios encontrados es de {0}.", dirs.Length);

            }
            catch (Exception e)
            {

                System.IO.Directory.CreateDirectory("/storage/emulated/0/Pictures/SysDatec/");
                Console.WriteLine("El proceso fallo y no hay carpetas en SysDatec directorio, se mostrara el Directorio raiz {0}", e.ToString());
            }



            foreach (var fi in di.GetFiles())
            {
                if (fi.Extension.Contains("jpg"))
                {
                    FileImage = "jpg";
                    NameImage = fi.Name.Replace(".jpg", " ");
                }
                else if (fi.Extension.Contains("png"))
                {
                    FileImage = "png";
                    NameImage = fi.Name.Replace(".png", " ");

                }
                else if (fi.Extension.Contains("pdf"))
                {
                    FileImage = "pdf";
                    NameImage = fi.Name.Replace(".pdf", " ");
                }
                else if (fi.Extension.Contains("txt"))
                {
                    FileImage = "file";
                    NameImage = fi.Name.Replace(".txt", " ");
                }

                ListaArchivosSysdatec.Add(new ArchivosRecientes() { Name = NameImage, Fecha = fi.CreationTime, Picture = FileImage, Description = fi.Extension });
                //FileImage = "";
                //NameImage = "";
                Console.WriteLine(fi.Name);
            }

            try
            {
                dirs = di.GetDirectories();
                Console.WriteLine("el numero de directorios encontrados es de {0}.", dirs.Length);

                foreach (DirectoryInfo fi in dirs)
                {
                    ListaCarpetasSysdatec.Add(new NombresCarpetas() { Name = fi.Name, FechaCreacion = fi.CreationTime, Picture = "carpeta", CantidadArchivos = fi.GetFiles().Length.ToString() + " Archivos" });
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

            if (dirs.Length <= 0)
            {
                ListaCarpetasSysdatec.Add(new NombresCarpetas() { Name = "SysDatec", FechaCreacion = di.CreationTime, Picture = "carpeta", CantidadArchivos = di.GetFiles().Length.ToString() + " Archivos" });
            }

            NombresCarpetas = new ObservableCollection<NombresCarpetas>(ListaCarpetasSysdatec);
            ArchivosRecientes = new ObservableCollection<ArchivosRecientes>(ListaArchivosSysdatec);

        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }
    }
}
using DarkIce.Toolkit.Core.Utilities;
using OpenPdf;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Navigation;
using ScanApp.Models;
using ScanApp.ViewModels;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace ScanApp.Views
{
    public partial class ArchivosView : ContentPage, INavigatedAware
    {
        public static ArchivosView Instance;

        public async void SeleccionarImagen(string location,Image img)
        {
            var memoryStream = new MemoryStream();


            using (var source = System.IO.File.OpenRead(location))
            {
                await source.CopyToAsync(memoryStream);
                try
                {
                    image.Source = ImageSource.FromStream(() => memoryStream);
                }
                catch (Exception e)
                {
                }
            }
        }



        public ArchivosView()
        {
            InitializeComponent();
            CrossMedia.Current.Initialize();
            Nombre.Text = Application.Current.Properties["name"].ToString();
            NavigationPage.SetBackButtonTitle(this, "");
            Instance = this;

            takePhoto.Clicked += async (sender, args) =>
           {

               if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
               {
                   _ = DisplayAlert("No hay Camara", "No hay camara disponible.", "OK");
                   return;
               }



               var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
               {
                   Directory = "SysDatec",
                   SaveToAlbum = true,
                   CompressionQuality = 75,
                   CustomPhotoSize = 50,
                   PhotoSize = PhotoSize.MaxWidthHeight,
                   MaxWidthHeight = 2000,
                   DefaultCamera = CameraDevice.Rear
               });

               if (file == null)
                   return;

               //DirectoryInfo di = new DirectoryInfo("/storage/emulated/0/Android/data/XamarinCamera.XamarinCamera/files/Pictures/sample/");
               //Console.WriteLine("No search pattern returns:");
               //foreach (var fi in di.GetFiles())
               //{
               //    Console.WriteLine(fi.Name);
               //}
               BindingContext = new ArchivosViewModel();
               _ = DisplayAlert("Archivo guardado", file.Path, "OK");



               image.Source = ImageSource.FromStream(() =>
               {
                   var stream = file.GetStream();
                   file.Dispose();
                   return stream;
               });
           };

            pickPhoto.Clicked += async (sender, args) =>
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    _ = DisplayAlert("Fotos no soportada", "No hay permisos garantizados para fotos.", "OK");
                    return;
                }
                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,

                });


                if (file == null)
                    return;

                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            };

            /*takeVideo.Clicked += async (sender, args) =>
            {
              if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
              {
                DisplayAlert("No hay camara", "No hay camara disponible.", "OK");
                return;
              }

              var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
              {
                Name = "video.mp4",
                Directory = "DefaultVideos",
              });

              if (file == null)
                return;

              DisplayAlert("Video grabando", "Localizacion: " + file.Path, "OK");

              file.Dispose();
            };*/

            /*pickVideo.Clicked += async (sender, args) =>
            {
              if (!CrossMedia.Current.IsPickVideoSupported)
              {
                DisplayAlert("Videos no soportados", ":( Permiso no gartizado para videos.", "OK");
                return;
              }
              var file = await CrossMedia.Current.PickVideoAsync();

              if (file == null)
                return;

              DisplayAlert("Video Seleccionado", "Localizacion: " + file.Path, "OK");
              file.Dispose();
            };*/



            //byte[] bytearray = null;
            //using (var ms = new MemoryStream())
            //{
            //    if (image.Source != null)
            //    {
            //        var wbitmp = new WriteableBitmap(image.Source);

            //        wbitmp.SaveJpeg(ms, 46, 38, 0, 100);
            //        bytearray = ms.ToArray();
            //    }
            //}
            //if (bytearray != null)
            //{
            //    // image base64 here
            //    string btmStr = Convert.ToBase64String(bytearray);
            //}

            //var byteArray = Convert.FromBase64String("IMG_20210922_200031.jpg");
            //stream = new MemoryStream(byteArray);
            //var imageSource = ImageSource.FromStream(() => stream);                                           



            //_ = SaveCountAsync(1);
            //var xx = PCLStorageSample();
            //var filePath = Path.Combine(DEFAULTPATH, "SharingImage.png");
            //if (!File.Exists(filePath)) {
            //    File.Delete(filePath);
            //    Console.WriteLine("No Archivo >"+ filePath);
            //} else {
            //    Console.WriteLine("Si Archivo >" + filePath);
            //}


            BindingContext = new ArchivosViewModel();
        }



        private async void Button_Clicked(object sender, EventArgs e)
        {
            image.IsVisible = true;
            image1.IsVisible = false;
            try
            {

                await Sheet.OpenSheet();
            }
            catch (Exception ex)
            {
                //image.Source = null;
                ex.Log();
            }
        }

        public void LoadImagenCopy(string nombreArchivo) {
         
         string FileImage="";
         string NameImage="";
        
        DirectoryInfo di = new DirectoryInfo("/storage/emulated/0/Pictures/SysDatec/");
        Console.WriteLine("No search pattern returns:");

            foreach (var fi in di.GetFiles())
            {
                if (nombreArchivo.Contains(fi.Name))
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
                    else
                    {
                       NameImage = fi.Name;
                       FileImage = "file";
                    }

                    //aqui realizar el copiado de un archivo a temporal

                    //var imagenTemp = new Image
                    //{
                    //    Source = ImageSource.FromUri(new Uri("/storage/emulated/0/Pictures/SysDatec/" + nombreArchivo))
                    //};

                    Uri OrigenFile = new Uri("/storage/emulated/0/Pictures/SysDatec/" + nombreArchivo, UriKind.RelativeOrAbsolute);
                    
                    Image assignImageFromFile = new Image
                    {
                        Source = (Device.RuntimePlatform == Device.Android) ? ImageSource.FromFile(OrigenFile.LocalPath) : ImageSource.FromFile(OrigenFile.LocalPath),
                        Aspect = Aspect.AspectFit
                    };
                    image.IsVisible = false;
                    image1.IsVisible = true;
                    image1.Source = OrigenFile;

                    Application.Current.MainPage = new NavigationPage(new WebViewPage(OrigenFile.LocalPath, false));


                    //image.HeightRequest = 500;
                    //image.WidthRequest = 300;
                    // image.Source = OrigenFile;

                    //image.Source = new UriImageSource
                    //{
                    //    Uri = OrigenFile,
                    //    CachingEnabled = false,
                    //    CacheValidity = new TimeSpan(5, 0, 0, 0)
                    //};
                    //var fileName = System.IO.Path.GetFileName(fi.Name);
                    //Image embeddedImage = new Image
                    //{  
                    //    Source = ImageSource.FromResource(nombreArchivo, typeof(ArchivosView).GetTypeInfo().Assembly)
                    //};

                    //var  destFile = System.IO.Path.Combine("/storage/emulated/0/Pictures/SysDatec/", fileName);
                    //System.IO.File.Copy(nombreArchivo,"/storage/emulated/0/Pictures/SysDatec/", true);
                }
               
            }
            if (FileImage.Length == 0)
            {
                Console.WriteLine("El archivo no se encontro : " + nombreArchivo);
            }
            else {
                Console.WriteLine("Archivo encontrado : " + nombreArchivo);

            }
    }  


        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            //aqui se coloca cuando le da en el frame

            ArchivosRecientes tmpData = (ArchivosRecientes)((TappedEventArgs)e).Parameter;
            LoadImagenCopy(tmpData.Name.Trim()+ tmpData.Description.Trim());

            //ScanApp.Views.ArchivosView.Instance.SeleccionarImagen("/storage/emulated/0/Pictures/SysDatec/" + NameImage.Trim() + "." + FileImage);
            //Uri resourceUri = new Uri("/storage/emulated/0/Pictures/SysDatec/" + tmpData.Name.Trim() + "." + tmpData.Description, UriKind.Relative);

            //ScanApp.ViewModels.ArchivosViewModel.Instance= 
            //image.Source = tmpData.Name.Trim() + tmpData.Description;
            try
            {
                Sheet.HeightRequest = 700;
                await Sheet.OpenSheet();
            }
            catch (Exception ex)
            {
                image.IsVisible = false;
                ex.Log();
            }



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
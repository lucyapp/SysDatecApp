using DarkIce.Toolkit.Core.Utilities;
using OpenPdf;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Navigation;
using ScanApp.Models;
using ScanApp.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace ScanApp.Views
{
    public partial class ArchivosView : ContentPage, INavigatedAware
    {
        public static ArchivosView Instance;
        private Stream stream { get; set; }
        public string sourcePath = @"/storage/emulated/0/Pictures/SysDatec/";
        public string targetPath = @"/storage/emulated/0/Android/data/com.plugin.mediatest/files/";
        public async void SeleccionarImagen(string location)
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

        [Obsolete]
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
                var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,


                });


                if (file == null)
                    return;

                image.Source = ImageSource.FromStream(() =>
                {
                    stream = file.GetStream();
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
            try
            {
                await Sheet.OpenSheet();
            }
            catch (Exception ex)
            {
                ex.Log();
            }
        }

        public void CopyFile(string sourceFilePath, string destinationFilePath)
        {
            var fileBytes = File.ReadAllBytes(sourceFilePath);
            File.WriteAllBytes(destinationFilePath, fileBytes);
            File.Delete(sourcePath);
        }

        public async Task DeployDatabaseFromAssetsAsync()
        {
            var databaseName = "database.db3";

            // Android application default folder.
            var appFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbFile = Path.Combine(appFolder, databaseName);

            // Check if the file already exists.
            if (!File.Exists(dbFile))
            {
                using (FileStream writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    // Assets is comming from the current context.
                   // await writeStream.Open(databaseName).CopyToAsync(writeStream);
                }
            }
        }


        static string DEFAULTPATH = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public static void SaveBytes(string fileName, byte[] data)
        {
            var filePath = Path.Combine(DEFAULTPATH, fileName);
            if (!File.Exists(filePath))
                File.Delete(filePath);
            File.WriteAllBytes(filePath, data);
        }





        public async void LoadImagenCopy(string nombreArchivo)
        {

            string FileImage = "";
            string NameImage = "";

            DirectoryInfo di = new DirectoryInfo("/storage/emulated/0/Pictures/SysDatec/");
            Console.WriteLine("No search pattern returns:");

            foreach (var fi in di.GetFiles())
            {
                if (nombreArchivo.Contains(fi.Name))
                {
                    if (fi.Extension.Contains("jpg"))
                    {
                        FileImage = "jpg";
                        NameImage = fi.Name.Replace(".jpg", " ").Trim();
                    }
                    else if (fi.Extension.Contains("png"))
                    {
                        FileImage = "png";
                        NameImage = fi.Name.Replace(".png", " ").Trim();
                    }
                    else if (fi.Extension.Contains("pdf"))
                    {
                        FileImage = "pdf";
                        NameImage = fi.Name.Replace(".pdf", " ").Trim();

                    }
                  
                }

            }
            if (FileImage.Length == 0)
            {
                Console.WriteLine("El archivo no se encontro : " + nombreArchivo);
            }
            else
            {
                Console.WriteLine("Archivo encontrado : " + nombreArchivo);

            }

           
            Uri OrigenFile = new Uri("/storage/emulated/0/Pictures/SysDatec/" + nombreArchivo, UriKind.RelativeOrAbsolute);
            image.Source = OrigenFile;
            string sourceFile = System.IO.Path.Combine(sourcePath, OrigenFile.OriginalString);
            string destFile = System.IO.Path.Combine(sourcePath, targetPath+nombreArchivo);
       
            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);
                foreach (string s in files)
                {   
                    if(s== OrigenFile.OriginalString) 
                    {  
                        CopyFile(OrigenFile.LocalPath, @destFile);
                    }
                }
            }
            else
            {
                Console.WriteLine("Source path does not exist!");
            }

            string nn = destFile;
            await Navigation.PushModalAsync(new WebViewPage(nn, true));

        }


        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ArchivosRecientes tmpData = (ArchivosRecientes)((TappedEventArgs)e).Parameter;
            LoadImagenCopy(tmpData.Name.Trim() + tmpData.Description.Trim());

            try
            {
                //await SeleccionarImagen("/storage/emulated/0/Pictures/SysDatec/" + tmpData.Name.Trim() + tmpData.Description);
                await Sheet.OpenSheet();
            }
            catch (Exception ex)
            {

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
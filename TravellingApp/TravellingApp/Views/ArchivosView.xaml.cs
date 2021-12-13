using DarkIce.Toolkit.Core.Utilities;
using PCLStorage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;
using Prism.Navigation;
using ScanApp.CustomControl;
using ScanApp.Data;
using ScanApp.Models;
using ScanApp.ViewModels;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using FileSystem = PCLStorage.FileSystem;
using TestAppSharePlugin.Interfaces;

namespace ScanApp.Views
{
    public partial class ArchivosView : ContentPage, INavigatedAware
    {
        public static ArchivosView Instance;

        private object ImagenGuardada { get; set; }
        private object ImagenSource { get; set; }

        private Stream stream { get; set; }
        public string sourcePathInicial { get; set; } = @"/storage/emulated/0/Pictures/SysDatec/";
        public string sourcePath { get; set; } = @"/storage/emulated/0/Pictures/SysDatec/";
        public string targetPath { get; set; } = @"/storage/emulated/0/Android/data/com.plugin.mediatest/files/";
        public string targetPath2 { get; set; } = @"/storage/emulated/0/Android/data/com.plugin.mediatest/files/Pictures/SysDatec";
        public int contador { get; set; } = 0;

        string DEFAULTPATH { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public async Task SeleccionarImagen(string location)
        {
            string cadena = location;
            string[] Separado = cadena.Split('/');
            string Final = Separado[Separado.Length - 1];
            var memoryStream = new MemoryStream();

            var source = System.IO.File.OpenRead(targetPath + Final);
            await source.CopyToAsync(memoryStream);
            try
            {
                image.Source = ImageSource.FromStream(() => memoryStream);
            }
            catch (Exception e)
            {
            }
        }
        public List<string> Convertir_StringSplit_ToList(string Lista)
        {
            List<string> stringList = Lista.Split(',').ToList();
            return stringList;
        }
        public ArchivosView ContentPage { get; private set; }




        public string GetFileNameFromUrl(string url)
        {
            Uri SomeBaseUri = new Uri(url);
            Uri uri;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
                uri = new Uri(SomeBaseUri, url);

            return Path.GetFileName(uri.LocalPath);
        }

        [Obsolete]
        public ArchivosView()
        {
            InitializeComponent();
            CrossMedia.Current.Initialize();
            try
            {
                Nombre.Text = Application.Current.Properties["Nombre"].ToString() + " " + Application.Current.Properties["Apellido"].ToString();

            }
            catch (Exception)
            {
                Nombre.Text = Application.Current.Properties["name"].ToString();

            }
            NavigationPage.SetBackButtonTitle(this, "");
            Instance = this;

            takePhoto.Clicked += async (sender, args) =>
            {
                
               
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    _ = DisplayAlert("No hay Camara", "No hay camara disponible.", "OK");
                    return;
                }

                var Lista = DevEnvExe_LocalStorage.PCLHelper.ListaDirectorios();
                ObservableCollection<ListadosMedia> Lista1 = DevEnvExe_LocalStorage.PCLHelper.ListaDirectoriosAll();

                string CarpetaRaiz = "";


                string action = await DisplayActionSheet("Guardar en Carpeta?", "Cancel", null, Lista.Select(device => device.ToString()).ToArray());


                if (action != null && action != "Cancel")
                {
                    foreach (var lc in Lista1)
                    {

                        if (lc.Carpetas.Count >= 0)
                        {

                            for (int i = 0; i < lc.Carpetas.Count(); i++)
                            {
                                if (lc.Carpetas[i].Name.Equals(action))
                                {
                                    //significa que solo son carpetas y no subcarpetas
                                    CarpetaRaiz = sourcePath + action;
                                }


                            }

                        }
                    }

                    if (CarpetaRaiz == "")
                    {

                        foreach (var lsc in Lista1)
                        {

                            if (lsc.Subcarpetas.Count() >= 0)
                            {
                                for (int i = 0; i < lsc.Subcarpetas.Count(); i++)
                                {
                                    if (lsc.Subcarpetas[i].Name.Equals(action))
                                    {
                                        //significa que solo son carpetas y no subcarpetas
                                        CarpetaRaiz = @sourcePath + lsc.Subcarpetas[i].DirectorioPadre + "/" + lsc.Subcarpetas[i].Name;
                                    }


                                }

                            }

                        }
                    }
                }
                else
                {
                    CarpetaRaiz = "SysDatec";
                }

              

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "SysDatec",
                    SaveToAlbum = true,
                    CompressionQuality = 75,
                    CustomPhotoSize = 50,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 2000,
                    DefaultCamera = CameraDevice.Rear,

                });

                if (file == null)
                {
                    return;

                }
                else
                {

                    Application.Current.Properties["ImagenFileString"] = file.Path;
                }
               
                var respuesta = await DisplayActionSheet("carpeta Destino", CarpetaRaiz, null);
              
                File.WriteAllText(targetPath + "File.txt", file.Path);
             

                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });

                if (image.Source != null)
                {

                    Application.Current.Properties["ImagenSource"] = image.Source;
                    String[] Card = System.IO.File.ReadAllLines(targetPath + "File.txt");
 
                    string lastPart = Card[0].Split('/').Last();

                    if (respuesta != null)
                    {
                        sourcePath = CarpetaRaiz;

                        bool Estadocopiado = CopiarFile(lastPart);
                        if (CarpetaRaiz != "SysDatec")
                        {
                            if (Estadocopiado == false)
                            {
                                 await DisplayActionSheet("Error", "Se produjo un error al intentar guardar el archivo", "OK");
                            }
                            else
                            {
                                await DisplayActionSheet("Exito al guardar", CarpetaRaiz + "/" + lastPart, "OK");
                            }
                        }
                        var imageByte = System.IO.File.ReadAllBytes(Card[0].ToString());
                        ShareVentana(imageByte);
                    }
                  
                        
                        


                  
                    //GuardarImagenDatabase((string)Application.Current.Properties["ImagenFileString"], Application.Current.Properties["ImagenSource"]);
                }
                
                BindingContext = new ArchivosViewModel();
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





                //Application.Current.Properties["ImagenFile"] = file.Path;
                //Application.Current.Properties["ImagenSource"] = image.Source;
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

        public async void GuardarImagenDatabase(string Nombre, object ImageSource)
        {

            var todoItem = new ImagenesModel();

            todoItem.FilePath = Nombre;
            todoItem.ImageSource = (byte[])ImageSource;

            //BindingContext = todoItem;
            ImagenesDatabase database = await ImagenesDatabase.Instance;

            await database.SaveItemAsync(todoItem);
            await Navigation.PopAsync();
        }
        public static async Task CopiarFile_GuardarAsync(string file, string carpetaInicial, string carpetafinal, byte[] bytefile)
        {
            string fileName = carpetaInicial;
            string sourcePath = @carpetaInicial;
            string targetPath = @carpetafinal;
            bool v = await CrossMedia.Current.Initialize();
            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName);

            // To copy a folder's contents to a new location:
            // Create a new target folder.
            // If the directory already exists, this method does not create a new directory.

            Plugin.Permissions.Abstractions.PermissionStatus permisos = await (_ = DevEnvExe_LocalStorage.PCLHelper.VerificarPermisos()).ConfigureAwait(false);

            DirectoryInfo dir = System.IO.Directory.CreateDirectory(carpetaInicial + "temp");


            string StringByte = DevEnvExe_LocalStorage.PCLHelper.BytesToStringConverted(bytefile);
            await (_ = DevEnvExe_LocalStorage.PCLHelper.SaveImage(bytefile, file)).ConfigureAwait(false);
            //System.IO.File.Copy(sourceFile,targetPath, true);

            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);

                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    if (s == file)
                    {
                        fileName = System.IO.Path.GetFileName(s);
                        destFile = System.IO.Path.Combine(targetPath, fileName);
                        System.IO.File.Copy(s, destFile, true);
                    }
                }
            }
            else
            {
                Console.WriteLine("Archivo ene la ruta no existe!");
            }


        }

        public ICommand LongPressCommand
        {
            get
            {
                return new Command(() =>
                {
                    App.Current.MainPage.DisplayAlert("LongPress Command", "This is long press command", "OK");
                });
            }
        }
        public ICommand TappCommand
        {
            get
            {
                return new Command(() =>
                {
                    App.Current.MainPage.DisplayAlert("Tapp Command", "This is Tap command", "OK");
                });
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            //image.Source = (ImageSource)Application.Current.Properties["ImagenSource"];
            try
            {
                await Sheet.OpenSheet();
            }
            catch (Exception ex)
            {
                ex.Log();
            }
        }

        public async Task CopyFileAsync(string sourceFilePath, string destinationFilePath)
        {
            var fileBytes = File.ReadAllBytes(sourceFilePath);

            File.WriteAllBytes(destinationFilePath, fileBytes);
            SaveBytes(sourceFilePath, fileBytes);

            var f1 = PCLStorage.FileSystem.Current.GetFileFromPathAsync(destinationFilePath);
            var d1 = PCLStorage.FileSystem.Current.LocalStorage;

            //var tempFolder = PCLStorage.FileSystem.Current.LocalStorage;
            var rootFolder1 = Xamarin.Essentials.FileSystem.AppDataDirectory;
            IFolder rootFolder = await PCLStorage.FileSystem.Current.GetFolderFromPathAsync("/storage/emulated/0/Pictures/SysDatec");

            var file = await rootFolder.CreateFileAsync("/storage/emulated/0/Android/data/com.plugin.mediatest/files/2021-08-11.pdf", PCLStorage.CreationCollisionOption.ReplaceExisting);
            var copiedFile = await rootFolder.CreateFileAsync("/storage/emulated/0/Android/data/com.plugin.mediatest/files", PCLStorage.CreationCollisionOption.ReplaceExisting);
            File.Delete(sourcePath);


        }

        public void SaveBytes(string fileName, byte[] data)
        {
            var filePath = Path.Combine(targetPath, fileName);
            if (!File.Exists(filePath))
                File.Delete(filePath);
            File.WriteAllBytes(filePath, data);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static byte[] ConvertirStringToByte(string cadena)
        {
            byte[] l_Data = Convert.FromBase64String(cadena);
            return l_Data;
        }
        public bool CopiarFile(string filename)
        {
            string sourceFile  = Path.Combine(targetPath2, filename);
            string destFile = Path.Combine(sourcePath, filename);
          
            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            try
            {
                File.Copy(sourceFile , destFile, true);
            }
            catch (Exception e)
            {
                return false;
            }
            sourcePath = sourcePathInicial;
            File.Delete(sourcePath+filename);
            return true;
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
                    else
                    {
                        FileImage = "txt";
                        NameImage = fi.Name.Replace(".txt", " ").Trim();
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

            var Imagen64 = DevEnvExe_LocalStorage.PCLHelper.ImageToBase64(nombreArchivo);
            if (Imagen64 != null)
            {
                var ImagenCargada = await DevEnvExe_LocalStorage.PCLHelper.GetFileStreamAsync(nombreArchivo).ConfigureAwait(false);
                byte[] myBinary = new byte[ImagenCargada.Length];
                string cadenaString = System.Text.Encoding.UTF8.GetString(myBinary, 0, myBinary.Length);
                string resBase = Base64Encode(cadenaString);
                File.WriteAllText(targetPath + "Base64.txt", resBase);
                var lect = ImagenCargada.Read(myBinary, 0, (int)ImagenCargada.Length);
                var LoadImage = await (_ = DevEnvExe_LocalStorage.PCLHelper.LoadImage(myBinary, nombreArchivo, await PCLStorage.FileSystem.Current.GetFolderFromPathAsync("storage/emulated/0/Pictures/SysDatec/"))).ConfigureAwait(false);
                //SaveBytes(targetPath, myBinary);
                var stream1 = new MemoryStream(myBinary);
                image.Source = ImageSource.FromStream(() =>
                {
                    var imagen = new MemoryStream(myBinary);
                    return imagen;
                });
            }
            //string sourceFile = System.IO.Path.Combine(sourcePath, OrigenFile.OriginalString);
            //string destFile = System.IO.Path.Combine(sourcePath, targetPath + nombreArchivo);

            //if (System.IO.Directory.Exists(sourcePath))
            //{
            //    string[] files = System.IO.Directory.GetFiles(sourcePath);
            //    foreach (string s in files)
            //    {   
            //        if(s== OrigenFile.OriginalString) 
            //        {
            //           _= CopyFileAsync(OrigenFile.LocalPath, @destFile);
            //        }
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Source path does not exist!");
            //}

            //string nn = destFile;
            //await Navigation.PushModalAsync(new WebViewPage(nn, true));

        }

        private async void TapGestureRecognizer_TappedAsync(object sender, EventArgs e)
        {
            ArchivosRecientes tmpData = (ArchivosRecientes)((TappedEventArgs)e).Parameter;

            var LoadImage = DevEnvExe_LocalStorage.PCLHelper.GetFilePathFromRoot(DEFAULTPATH + "/" + tmpData.Name.Trim() + tmpData.Description.Trim());
            Console.WriteLine(LoadImage);

            try
            {
                _ = SeleccionarImagen(DEFAULTPATH + tmpData.Name.Trim() + tmpData.Description).ConfigureAwait(false);
                _ = Sheet.OpenSheet().ConfigureAwait(false);
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

        [Obsolete]
        public async void ImageButton_Pressed(object sender, EventArgs e)
        {
            var Archivo = (CustomImage)sender;
            Console.WriteLine("Presionado");
            string action = await DisplayActionSheet("Eliminar  Archivo: Esta seguro?", "Cancelar", "Aceptar", Archivo.CommandParameter.ToString());
            if (action == "Aceptar")
            {
                string file = Archivo.CommandParameter.ToString().Trim() + ".jpg";
                var elimando = await (_ = DevEnvExe_LocalStorage.PCLHelper.DeleteFile(file.Trim(), await FileSystem.Current.GetFolderFromPathAsync("/storage/emulated/0/Pictures/SysDatec/"))).ConfigureAwait(false);
                if (elimando == true)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Task.Delay(1000);
                        _ = DisplayAlert("Eliminación", "El archivo seleccionado ha sido eliminado", "Ok").ConfigureAwait(false);
                        BindingContext = new ArchivosViewModel();
                    });


                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _ = Task.Delay(1000);
                        _ = DisplayAlert("Eliminación", "El archivo no se logro eliminar", "Ok");

                    });


                }

            }


        }

        [Obsolete]
        private void PageNavigationSource(ArchivosViewModel archivosViewModel)
        {
            BindingContext = archivosViewModel;
        }

        [Obsolete]
        public async void CustomImage_LongPressed(object sender, EventArgs e)
        {
            var carpeta = (CustomImage)sender;
            Console.WriteLine("Presionado");
            string action = await DisplayActionSheet("Eliminar carpeta: Esta seguro?", "Cancelar", "Aceptar", carpeta.CommandParameter.ToString());

            if (action == "Aceptar")
            {
                string file = carpeta.CommandParameter.ToString().Trim();
                var elimando = await (_ = DevEnvExe_LocalStorage.PCLHelper.DeleteDirectory(file.Trim(), await FileSystem.Current.GetFolderFromPathAsync("/storage/emulated/0/Pictures/SysDatec/"))).ConfigureAwait(false);
                if (elimando == true)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Task.Delay(1000).ConfigureAwait(false);
                        _ = DisplayAlert("Eliminación", "El carpeta seleccionada ha sido eliminado", "Ok").ConfigureAwait(false);
                        BindingContext = new ArchivosViewModel();
                    });



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


        private bool isOpen = false;
        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {

            isOpen = true;

          

            if (isOpen == false)
            {
              

            }
            else
            {
              

               
            }

        }


        public void ShareVentana(byte[] image) {

            //var screenshot = DependencyService.Get<IScreenshotService>().Capture();
            MemoryStream ms = new MemoryStream(image);
            ImageSource imageSource = ImageSource.FromStream(() => { return ms; });

            DependencyService.Get<IShareService>().Share("ApaBot", "Imagen Share - SysDatec", imageSource);
        }


       
    }

}
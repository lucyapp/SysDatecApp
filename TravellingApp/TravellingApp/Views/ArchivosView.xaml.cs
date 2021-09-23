﻿using EnvDTE;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SysDatecScanApp.ViewModels;
using System;
using System.Buffers.Text;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;

namespace SysDatecScanApp.Views
{
    public partial class ArchivosView : ContentPage
    {
        public string Name { get; private set; }
        static string DEFAULTPATH = "storage/emulated/0/Android/data/com.plugin.mediatest/files/Pictures/SysDatec/";
        private Stream stream;

        public ArchivosView()
        {
            InitializeComponent();
            Name = Application.Current.Properties["name"].ToString();
            Nombre.Text = Name;
            takePhoto.Clicked += async (sender, args) =>
            {

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    _ = DisplayAlert("No hay Camara", "No hay camara disponible.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "SysDatec",
                    SaveToAlbum = true,
                    CompressionQuality = 75,
                    CustomPhotoSize = 50,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 2000,
                    DefaultCamera = CameraDevice.Front
                });

                if (file == null)
                    return;

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
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

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



           SaveCountAsync(1); 


            //var filePath = Path.Combine(DEFAULTPATH, "SharingImage.png");
            //if (!File.Exists(filePath)) {
            //    File.Delete(filePath);
            //    Console.WriteLine("No Archivo >"+ filePath);
            //} else {
            //    Console.WriteLine("Si Archivo >" + filePath);
            //}
                
           
            BindingContext = new ArchivosViewModel();
        }

        public async Task SaveCountAsync(int count)
        {
            var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures), "count.txt");
         
            using (var writer = File.CreateText(backingFile))
            {
                await writer.WriteLineAsync(count.ToString());
            }

             backingFile = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "count1.txt");
            using (var writer = File.CreateText(backingFile))
            {
                await writer.WriteLineAsync(count.ToString());
            }
        }

        private void SaveBytes(string fileName, byte[] data)
        {
            var filePath = Path.Combine(DEFAULTPATH, fileName);
            if (!File.Exists(filePath))
                File.Delete(filePath);
            File.WriteAllBytes(filePath, data);
        }
       
       
        public byte[] ImageSourceToBytes(ImageSource imageSource)
        {
            StreamImageSource streamImageSource = (StreamImageSource)imageSource;
            System.Threading.CancellationToken cancellationToken =
            System.Threading.CancellationToken.None;
            System.Threading.Tasks.Task<Stream> task = streamImageSource.Stream(cancellationToken);
            Stream stream = task.Result;
            byte[] bytesAvailable = new byte[stream.Length];
            stream.Read(bytesAvailable, 0, bytesAvailable.Length);
            return bytesAvailable;
        }

    }
}
using PCLStorage;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using ScanApp;
using ScanApp.Models;
using ScanApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using FileAccess = PCLStorage.FileAccess;

namespace DevEnvExe_LocalStorage
{
    public static class PCLHelper
    {
        private const int BUFFER_SIZE = 0x4096;
       

        public async static Task<bool> IsFileExistAsync(this string fileName, IFolder rootFolder = null)
        {
            // get hold of the file system
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            ExistenceCheckResult folderexist = await folder.CheckExistsAsync(fileName);
            // already run at least once, don't overwrite what's there
            if (folderexist == ExistenceCheckResult.FileExists)
            {
                return true;

            }
            return false;
        }

        public async static Task<bool> IsFolderExistAsync(this string folderName, IFolder rootFolder = null)
        {
            // get hold of the file system
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            ExistenceCheckResult folderexist = await folder.CheckExistsAsync(folderName);
            // already run at least once, don't overwrite what's there
            if (folderexist == ExistenceCheckResult.FolderExists)
            {
                return true;

            }
            return false;
        }

        public async static Task<IFolder> CreateFolder(this string folderName, IFolder rootFolder)
        {
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);
            return folder;
        }

        public async static Task<IFile> CreateFile(this string filename, IFolder rootFolder = null)
        {
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            return file;
        }

        public static string BytesToStringConverted(byte[] bytes)
        {

            MemoryStream stream;
            StreamReader streamReader;
            using (stream = new MemoryStream(bytes))
            {
                using (streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }

            }

        }

        public async static Task<bool> WriteTextAllAsync(this string filename, string content = "", IFolder rootFolder = null)
        {


            IFile file = await filename.CreateFile(rootFolder);
            await file.WriteAllTextAsync(content);
            return true;
        }

        public async static Task<string> ReadAllTextAsync(this string fileName, IFolder rootFolder = null)
        {
            string content = "";
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            bool exist = await fileName.IsFileExistAsync(folder);
            if (exist == true)
            {
                IFile file = await folder.GetFileAsync(fileName);
                content = await file.ReadAllTextAsync();
            }
            return content;
        }

        public async static Task<bool> DeleteFile(this string fileName, IFolder rootFolder = null)
        {


            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;

            bool exist = await fileName.IsFileExistAsync(folder);
            if (exist == true)
            {
                IFile file = await folder.GetFileAsync(fileName);
                await file.DeleteAsync();
                return true;
            }
            return false;
        }

        public async static Task<bool> DeleteDirectory(this string fileName, IFolder rootFolder = null)
        {


            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;

            bool exist = await fileName.IsFolderExistAsync(folder);
            if (exist == true)
            {
                folder = await folder.GetFolderAsync(fileName);
                await folder.DeleteAsync();
                return true;
            }
            return false;
        }

        public async static Task SaveImage(this byte[] image, String fileName, IFolder rootFolder = null)
        {
            // get hold of the file system
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;

            // create a file, overwriting any existing file
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            // populate the file with image data
            using Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite);
            stream.Write(image, 0, image.Length);
        }

        public async static Task<byte[]> LoadImage(this byte[] image, String fileName, IFolder rootFolder = null)
        {

            // get hold of the file system
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;

            //open file if exists
            IFile file = await folder.GetFileAsync(fileName);
            //load stream to buffer
            using Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite);
            long length = stream.Length;
            byte[] streamBuffer = new byte[length];
            stream.Read(streamBuffer, 0, (int)length);
            return streamBuffer;

        }

        public static string ImageToBase64(string _imagePath)
        {
            string _base64String = null;

            using (System.Drawing.Image _image = System.Drawing.Image.FromFile(_imagePath))
            {
                using (MemoryStream _mStream = new MemoryStream())
                {
                    _image.Save(_mStream, _image.RawFormat);
                    byte[] _imageBytes = _mStream.ToArray();
                    _base64String = Convert.ToBase64String(_imageBytes);

                    return "data:image/jpg;base64," + _base64String;
                }
            }
        }

        public static async Task<System.IO.Stream> GetFileStreamAsync(string filePath)
        {
 
            var openAsync = (await FileSystem.Current.GetFileFromPathAsync(filePath))?.OpenAsync(FileAccess.ReadAndWrite);
            if (openAsync == null)
            {
                return null;
            }else
            return await openAsync;
        }

        public static async Task Save(string path, string content)
        {  //realizar con este
            IFileSystem fileSystem = FileSystem.Current;
            IFolder rootFolder = fileSystem.LocalStorage;
            var subFolder = await rootFolder.CreateFolderAsync(Path.GetDirectoryName(path), CreationCollisionOption.OpenIfExists);
            var file = await subFolder.CreateFileAsync(Path.GetFileName(path), CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(content);
        }

        public static async Task SaveFileAsync(string fileName, System.IO.MemoryStream inputStream)
        {
            var file = await FileSystem.Current.LocalStorage.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            using var stream = await file.OpenAsync(FileAccess.ReadAndWrite);
            inputStream.WriteTo(stream);
        }

        public static string GetFilePathFromRoot(string fileName) => System.IO.Path.Combine(FileSystem.Current.LocalStorage.Path, fileName);

        public static async Task<bool> ExistsAsync(string fileName) => await FileSystem.Current.LocalStorage.CheckExistsAsync(fileName) == ExistenceCheckResult.FileExists;

        public static async Task DownloadDocumentsAsync(PdfDocEntity pdfDocEntity)
        {
            var stream = await RestApiHelper.DownloadFileAsync(pdfDocEntity.Url);
            if (stream == null)
            {
                return;
            }
            await SaveFileAsync(pdfDocEntity.FileName, stream);
        }

        internal static byte[] ToByteArray(this Stream stream)
        {
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length;)
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
            return buffer;
        }

        public static async Task<PermissionStatus> VerificarPermisos()
        {
            PermissionStatus status = new PermissionStatus();
            //aqui se cambia el storagepermitios en lo que uno desee localizacion etc
            bool status1 = await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage).ConfigureAwait(false);
            if (status1 == true)
            {
            }
            else {
                //await App.Current.MainPage.DisplayAlert("Acceso", "Deberá aceptar los permisos de escritura", "OK");
                status = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>().ConfigureAwait(false);

            }

            return status;


        }

        public static FileStream OpenRead(string path)
        {
            // Open a file stream for reading and that supports asynchronous I/O
            return new FileStream(path, FileMode.Open, (System.IO.FileAccess)FileAccess.Read, FileShare.Read, BUFFER_SIZE, true);
        }

        public static async Task<byte[]> ReadAllBytes(string path)
        {
            using (var fs = OpenRead(path))
            {
                var buff = new byte[fs.Length];
                await fs.ReadAsync(buff, 0, (int)fs.Length);
                return buff;
            }
        }

        public static async Task<IFolder> CrearCarpetasEnDispositivo(string folderPath, string carpetaNueva)
        {

            //IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(folderPath );
            //IFolder folder = await rootFolder.CreateFolderAsync("/storage/emulated/0/Pictures/SysDatec/"+carpetaNueva, CreationCollisionOption.OpenIfExists); 
            //IFile file = await folder.CreateFileAsync("archivopdf.pdf", CreationCollisionOption.ReplaceExisting);
            //IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(folderPath );
            IFolder folder = await FileSystem.Current.GetFolderFromPathAsync(folderPath);
            folder = await folder.CreateFolderAsync(carpetaNueva, CreationCollisionOption.ReplaceExisting);
            return folder;
        }

        public static ObservableCollection<string> ListaDirectorios()
        {
            DirectoryInfo di = new DirectoryInfo("/storage/emulated/0/Pictures/SysDatec/");
            Console.WriteLine("realizar filtros de los archivos");
            List<ListaArchivosCarpetas> Lista = new List<ListaArchivosCarpetas>();
            ListaArchivosCarpetas Datos = new ListaArchivosCarpetas();
            ObservableCollection<NombresCarpetas> ListaCarpetasSysdatec = new ObservableCollection<NombresCarpetas>();
            ObservableCollection<NombresubCarpetas> ListaSubCarpetasSysdatec = new ObservableCollection<NombresubCarpetas>();
            ObservableCollection<ListadosMedia> ListaMedia = new ObservableCollection<ListadosMedia>();
            ListadosMedia DatosMedia = new ListadosMedia();
            ObservableCollection<string> listaAll = new ObservableCollection<string>();

            try
            {
                DirectoryInfo[] dirs = di.GetDirectories();
                Console.WriteLine("el numero de directorios encontrados es de {0}.", dirs.Length);

                foreach (DirectoryInfo fi in dirs)
                {

                    ListaCarpetasSysdatec.Add(new NombresCarpetas() { Name = fi.Name, FechaCreacion = fi.CreationTime, Picture = "carpeta", CantidadArchivos = fi.GetFiles().Length.ToString() });
                    listaAll.Add(fi.Name);
                    
                }
                foreach (var sc in ListaCarpetasSysdatec)
                {
                    di = new DirectoryInfo("/storage/emulated/0/Pictures/SysDatec/"+sc.Name);
                    dirs = di.GetDirectories();
                    Console.WriteLine("el numero de Sub-directorios encontrados es de {0}.", dirs.Length);
                    foreach (DirectoryInfo fi in dirs)
                    {

                        ListaSubCarpetasSysdatec.Add(new NombresubCarpetas() { Name = fi.Name, FechaCreacion = fi.CreationTime, Picture = "carpeta", CantidadArchivos = fi.GetFiles().Length.ToString(), DirectorioPadre = sc.Name.ToString() });
                        listaAll.Add(fi.Name);

                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("El proceso fallo y no hay carpetas en SysDatec directorio, se mostrara el Directorio raiz {0}", e.ToString());


            }
            
            DatosMedia.Carpetas =ListaCarpetasSysdatec;
            DatosMedia.Subcarpetas = ListaSubCarpetasSysdatec;
           
            ListaMedia.Add(DatosMedia);
           
            return listaAll;
        }

        public static ObservableCollection<ListadosMedia> ListaDirectoriosAll()
        {
            DirectoryInfo di = new DirectoryInfo("/storage/emulated/0/Pictures/SysDatec/");
            Console.WriteLine("realizar filtros de los archivos");
            List<ListaArchivosCarpetas> Lista = new List<ListaArchivosCarpetas>();
            ListaArchivosCarpetas Datos = new ListaArchivosCarpetas();
            ObservableCollection<NombresCarpetas> ListaCarpetasSysdatec = new ObservableCollection<NombresCarpetas>();
            ObservableCollection<NombresubCarpetas> ListaSubCarpetasSysdatec = new ObservableCollection<NombresubCarpetas>();
            ObservableCollection<ListadosMedia> ListaMedia = new ObservableCollection<ListadosMedia>();
            ListadosMedia DatosMedia = new ListadosMedia();
            ObservableCollection<string> listaAll = new ObservableCollection<string>();

            try
            {
                DirectoryInfo[] dirs = di.GetDirectories();
                Console.WriteLine("el numero de directorios encontrados es de {0}.", dirs.Length);

                foreach (DirectoryInfo fi in dirs)
                {

                    ListaCarpetasSysdatec.Add(new NombresCarpetas() { Name = fi.Name, FechaCreacion = fi.CreationTime, Picture = "carpeta", CantidadArchivos = fi.GetFiles().Length.ToString() });
                    listaAll.Add(fi.Name);

                }
                foreach (var sc in ListaCarpetasSysdatec)
                {
                    di = new DirectoryInfo("/storage/emulated/0/Pictures/SysDatec/" + sc.Name);
                    dirs = di.GetDirectories();
                    Console.WriteLine("el numero de Sub-directorios encontrados es de {0}.", dirs.Length);
                    foreach (DirectoryInfo fi in dirs)
                    {

                        ListaSubCarpetasSysdatec.Add(new NombresubCarpetas() { Name = fi.Name, FechaCreacion = fi.CreationTime, Picture = "carpeta", CantidadArchivos = fi.GetFiles().Length.ToString(), DirectorioPadre = sc.Name.ToString() });
                        listaAll.Add(fi.Name);

                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("El proceso fallo y no hay carpetas en SysDatec directorio, se mostrara el Directorio raiz {0}", e.ToString());


            }

            DatosMedia.Carpetas = ListaCarpetasSysdatec;
            DatosMedia.Subcarpetas = ListaSubCarpetasSysdatec;

            ListaMedia.Add(DatosMedia);

            return ListaMedia;
        }

        public static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries) {
                ProcessDirectory(subdirectory);
            }
               
        }

        // Insert logic for processing found files here.
        public static void ProcessFile(string path)
        {
            Console.WriteLine("Processed file '{0}'.", path);
            
        }

        public static string ImageTo64()
        {
            string path = @"/storage/emulated/0/Android/data/com.plugin.mediatest/files/";
            byte[] bytearray = null;
           
            if (bytearray != null)
            {
                string btmStr = Convert.ToBase64String(bytearray);
            }
            return null;
        } 
       
        public static Task GuardaEnAssets()
        {
            var databaseName = "database.pdf";

            // Android application default folder.
            var appFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var dbFile = Path.Combine(appFolder, databaseName);


            // Check if the file already exists.
            if (!File.Exists(dbFile))
            {
                using FileStream writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, (System.IO.FileAccess)FileAccess.ReadAndWrite);
                // Assets is comming from the current context.
                //await Assets.Open(databaseName).CopyToAsync(writeStream);
            }

            return Task.CompletedTask;
        }
    }
}

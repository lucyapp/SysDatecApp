using PCLStorage;
using ScanApp.Models;
using ScanApp.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using FileAccess = PCLStorage.FileAccess;

namespace DevEnvExe_LocalStorage
{
    public static class PCLHelper
    {

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

        public async static Task<IFolder> CreateFolder(this string folderName, IFolder rootFolder = null)
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
        public async static Task SaveImage(this byte[] image, String fileName, IFolder rootFolder = null)
        {
            // get hold of the file system
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;

            // create a file, overwriting any existing file
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            // populate the file with image data
            using (System.IO.Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                stream.Write(image, 0, image.Length);
            }
        }



        public async static Task<byte[]> LoadImage(this byte[] image, String fileName, IFolder rootFolder = null)
        {
            // get hold of the file system
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;

            //open file if exists
            IFile file = await folder.GetFileAsync(fileName);
            //load stream to buffer
            using (System.IO.Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                long length = stream.Length;
                byte[] streamBuffer = new byte[length];
                stream.Read(streamBuffer, 0, (int)length);
                return streamBuffer;
            }

        }

        public static async Task<System.IO.Stream> GetFileStreamAsync(string filePath)
        {
            var openAsync = (await FileSystem.Current.GetFileFromPathAsync(filePath))?.OpenAsync(FileAccess.Read);
            if (openAsync == null)
            {
                return null;
            }
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
            using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                inputStream.WriteTo(stream);
            }
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

    }
}

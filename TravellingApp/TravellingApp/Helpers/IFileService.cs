using System.IO;

namespace ScanApp.Helpers
{
    public interface IFileService
    {
        void SavePicture(string name, Stream data, string location = "temp");
    }
}

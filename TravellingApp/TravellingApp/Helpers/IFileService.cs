using System.IO;

namespace SysDatecScanApp.Helpers
{
    public interface IFileService
    {
        void SavePicture(string name, Stream data, string location = "temp");
    }
}

namespace ScanApp.Helper
{
    public interface IExport
    {
        bool IsPermissionNeeded();

        bool ExportFile(string contents, string filename);
    }
}

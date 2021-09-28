namespace ScanApp.Helpers
{
    public interface IAppParameters
    {
        int GetIdProgram();
        string GetAppName();
        string GetAppCenterKey();
        string GetAppVersion();
        string GetOneSignalKey();
    }
}
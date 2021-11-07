using System;
using Xamarin.Forms;

namespace ScanApp.Helper
{
    public class ExportData
    {
        public bool CreateExportFile(string text)
        {
            return DependencyService.Get<IExport>().ExportFile(text, "/storage/emulated/0/Pictures/SysDatec/2021-08-11.pdf");
        }
    }
}

using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Xamarin.Essentials;

namespace SysDatecScanApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Formulario : ContentPage
    {
        private string fileContents;

        public Formulario()
        {
            ArchivosCarpetas();
            InitializeComponent();
        }

        public static List<string> GetAllFilesFromFolder(string root, bool searchSubfolders)
        {
            Queue<string> folders = new Queue<string>();
            List<string> files = new List<string>();
            folders.Enqueue(root);
            while (folders.Count != 0)
            {
                string currentFolder = folders.Dequeue();
                try
                {
                    string[] filesInCurrent = System.IO.Directory.GetFiles(currentFolder, "*.*", System.IO.SearchOption.TopDirectoryOnly);
                    files.AddRange(filesInCurrent);
                }
                catch
                {
                    // Do Nothing
                }
                try
                {
                    if (searchSubfolders)
                    {
                        string[] foldersInCurrent = System.IO.Directory.GetDirectories(currentFolder, "*.*", System.IO.SearchOption.TopDirectoryOnly);
                        foreach (string _current in foldersInCurrent)
                        {
                            folders.Enqueue(_current);
                        }
                    }
                }
                catch
                {
                    // Do Nothing
                }
            }
            return files;
        }
        public static void DirectorySearch(string dir)
        {/// storage / emulated / 0 / Android / data / com.companyname.app / files / count.txt


            var lista = GetAllFilesFromFolder("/storage/emulated/0/Android/data/com.mult/SysDatec/", true);

            try
            {
                foreach (string f in Directory.GetFiles(dir))
                {
                    Console.WriteLine(Path.GetFileName(f));
                }
                foreach (string d in Directory.GetDirectories(dir))
                {
                    Console.WriteLine(Path.GetFileName(d));
                    DirectorySearch(d);
                }
            }
            catch (System.Exception ex)
            {
               
                Console.WriteLine(ex.Message);
            }
        }

        void ArchivosCarpetas() 
        {
          


            DirectorySearch("SysDatec");

        }

        private void btnGuardarFormulario_Clicked(object sender, EventArgs e)
        {
          
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistrarUsuario());
        }
    }
}
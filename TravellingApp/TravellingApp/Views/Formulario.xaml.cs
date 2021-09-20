using System;
using System.Collections.Generic;
using SysDatecScanApp.Data;
using SysDatecScanApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace SysDatecScanApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Formulario : ContentPage
    {
        public Formulario()
        {
            InitializeComponent();
        }


        void ArchivosCarpetas() 
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/Pictures"; // Documents folder  
            var path = Path.Combine(documentsPath, "/SysDatec/");

        
            if (!System.IO.Directory.Exists(documentsPath + path))
            {
                Console.WriteLine("\n carpeta ya Sysdatec ya existe: " + path);

                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Pictures/SysDatec/";

                List<string> dirs = new List<string>(Directory.EnumerateDirectories(docPath));

                foreach (var dir in dirs)
                {
                    Console.WriteLine($"{dir.Substring(dir.LastIndexOf(Path.DirectorySeparatorChar) + 1)}");
                }
                Console.WriteLine($"{dirs.Count} directories found.");



            }
            else
            {
                //aqui arrojar mensaje de que no existe y que se va crear
                //verificar si en la nube hay datos de usuario de documentos y carpetas 

                Console.WriteLine("\ncarpeta no existente SysDatec : " + path);


            }
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
using Newtonsoft.Json;
using ScanApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Xamarin.Forms;

namespace ScanApp
{
    public partial class ApiRest : ContentPage
    {
        public static ApiRest Instance;
        private const string Url = "http://servicios.sysdatec.com/WsAPABot01/api/UserDetailCredentials"; //modificar el modelo dependiendo de la url los campos
        private readonly HttpClient client = new HttpClient();
        public ObservableCollection<UsuarioModel> _post { get; private set; } //cambiar este por model ubicacion para poder traer el post de los datos de mapas ofertas
        //private ObservableCollection<Model_Ubicacion> _post;
        public ApiRest()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            OnAppearing();
            Instance = this;

        }
        protected override async void OnAppearing()
        {
            string content = await client.GetStringAsync(Url).ConfigureAwait(true);

            //este es para poder mostrarlo en la interfaz
            List<UsuarioModel> posts = JsonConvert.DeserializeObject<List<UsuarioModel>>(content);
            _post = new ObservableCollection<UsuarioModel>(posts);
            //ListViewSelected.ItemsSource = _post;

            //este pedazo me agrega a la base de datos toda la lista de el modelo para guardarlo
            //var sourceData = "";
            //UserDB userData = new UserDB();
            //List<UsuarioModel> auxSUB = new List<UsuarioModel>();
            //auxSUB = userData.GetUsuarios().ToList();
            //fin de el guardar a la base de datos los datos traido de la api en JSON

            //for (int i = 0; i < _post.Count; i++)
            //{
            //    sourceData = userData.Addusuario(_post[i]);
            //    Console.WriteLine(sourceData);
            //}

            base.OnAppearing();
        }

    }
}
using Newtonsoft.Json;
using ScanApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScanApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarUsuario : ContentPage
    {
        private const string Url = "http://servicios.sysdatec.com/WsAPABot01/api/UserDetailCredentials"; //modificar el modelo dependiendo de la url los campos
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<UsuarioModel> _post;
        private UsuarioModel user;
        public bool resultado;
        public RegistrarUsuario()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            if (VersionTracking.IsFirstLaunchEver)
            {
                Navigation.PushModalAsync(new OnboardingPage());
            }
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            var ni = NetworkInterface.GetAllNetworkInterfaces()
           .OrderBy(intf => intf.NetworkInterfaceType)
           .FirstOrDefault(intf => intf.OperationalStatus == OperationalStatus.Up
                 && (intf.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
                     || intf.NetworkInterfaceType == NetworkInterfaceType.Ethernet));

            var hw = ni.GetPhysicalAddress();
            var device = string.Join(":", (from ma in hw.GetAddressBytes() select ma.ToString("X2")).ToArray());
            user = new UsuarioModel
            {
                Username = txtUsername.Text,
                Password = txtContrasena.Text,
                Email = txtEmail.Text,
                Name = txtName.Text,
                LoginTime = DateTime.Now.ToString(),
                Device = device
            };

            AreCredentialsCorrect(user);
        }


        public async void PostAsync(string uri, string data)
        {
            var client = new HttpClient();
            Uri RequestUri = new Uri(uri);

            var json = JsonConvert.SerializeObject(user);
            var contentJSON = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(RequestUri, contentJSON);

            switch (response.StatusCode)
            {
                case (System.Net.HttpStatusCode.OK):

                    break;

                case (System.Net.HttpStatusCode.BadRequest):

                    break;

                case (System.Net.HttpStatusCode.Forbidden):

                    break;

            }

            if (response.StatusCode == HttpStatusCode.OK)
            {

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Datos", "Ocurrio un error al registrar, por lo tanto verifique correctamente que los datos esten correctamente y que tenga acceso a internet", "OK").ConfigureAwait(true);
                    await Task.Delay(2000).ConfigureAwait(true);
                    resultado = false;
                });

            }
            else
            {

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Datos", "Se agrego correctamente la información de usuario, y se envio un correo electronico para confirmacion", "Ok").ConfigureAwait(true);
                    await Task.Delay(2000).ConfigureAwait(true);
                    txtContrasena.Text = "";
                    txtEmail.Text = "";
                    txtName.Text = "";
                    txtUsername.Text = "";
                    resultado = true;

                });




            }


        }


        public void Llamada()
        {
            PostAsync(Url, JsonConvert.SerializeObject(user));
        }


        bool AreCredentialsCorrect(UsuarioModel user)
        {
            Llamada();
            return resultado;
        }

    }
}
﻿
using Newtonsoft.Json;
using SysDatecScanApp.Data;
using SysDatecScanApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SysDatecScanApp.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private const string Url = "http://lucy.sysdatec.com/WsLucy01/api/UserDetailCredentials"; //modificar el modelo dependiendo de la url los campos
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<UsuarioModel> _post;
        private UsuarioModel user;
        public bool resultado;

        async void OnSaveClicked()
        {
            var todoItem = (UsuarioModel)BindingContext;
            UsuarioDatabase database = await UsuarioDatabase.Instance;
            await database.SaveItemAsync(todoItem);
            await Navigation.PopAsync();


        }

        async void OnDeleteClicked()
        {
            var todoItem = (UsuarioModel)BindingContext;
            UsuarioDatabase database = await UsuarioDatabase.Instance;
            await database.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked()
        {
            await Navigation.PopAsync();
        }




        void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            user = new UsuarioModel
            {
                Username = EntryUsername.Text,
                Password = EntryPassword.Text
            };

            AreCredentialsCorrect(user);

        }

        void ButtonRegistrarse_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistrarUsuario());
        }

        public async void Llamada()
        {

            string content = await client.GetStringAsync(Url).ConfigureAwait(true);
            List<UsuarioModel> posts = JsonConvert.DeserializeObject<List<UsuarioModel>>(content);
            _post = new ObservableCollection<UsuarioModel>(posts);
            var xx = _post.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
            if (xx is null)
            {
                //messageLabel.Text = "Login fallido";
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Autenticacion", "Su nombre de usuario o contrasena son incorrectos, verifique bien antes de acceder", "Ok").ConfigureAwait(true);

                });
                Application.Current.Properties["IsLoggedIn"] = false;
                resultado = false;
            }
            else
            {
                //messageLabel.Text = "Login exitoso";
                //await Navigation.PushAsync(new CarpetaListPage());
                resultado = true;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Autenticacion", "Su acceso al sistema ha sido satisfactorio, desea ir pagina principal", "Ok").ConfigureAwait(true);

                    //await Navigation.PopAsync().ConfigureAwait(true); //para el main 
                    //await Navigation.PushAsync(new CarpetaListPage());   //para una nueva ventana


                    await Task.Delay(2000).ConfigureAwait(true);

                });

                Application.Current.Properties["username"] = user.Username;
                Application.Current.Properties["password"] = user.Password;
                Application.Current.Properties["name"] = xx.Name;
                Application.Current.Properties["email"] = xx.Email;
                Application.Current.Properties["IsLoggedIn"] = true;

                Application.Current.MainPage = new AppShell();
            }

        }


        bool AreCredentialsCorrect(UsuarioModel user)
        {
            Llamada();
            return resultado;
        }



        private void Label_BindingContextChanged(object sender, EventArgs e)
        {
            messageLabel.Text = "";

        }

        private void ButtonRecuperarClave_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecuperarClave());
        }
    }
}

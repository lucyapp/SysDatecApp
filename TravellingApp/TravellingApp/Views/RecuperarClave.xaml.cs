using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SysDatecScanApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SysDatecScanApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecuperarClave : ContentPage
    {

        private string Url = "http://lucy.sysdatec.com/WsLucy01/api/UserDetailCredentials"; //modificar el modelo dependiendo de la url los campos
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<UsuarioModel> _post;
        private UsuarioModel user;
        public bool resultado;
        public RecuperarClave()
        {
            InitializeComponent();
        }
        async void OnCancelClicked()
        {
            await Navigation.PopAsync();
        }

        void ButtonRecuperarClave_Clicked(object sender, EventArgs e)
        {
            user = new UsuarioModel
            {
                Username = EntryUsername.Text,
                Email = EntryCorreo.Text
            };

            AreCredentialsCorrect(user);

        }
		public async void Llamada()
		{
		
			string content = await client.GetStringAsync(Url).ConfigureAwait(true);
			List<UsuarioModel> posts = JsonConvert.DeserializeObject<List<UsuarioModel>>(content);
			_post = new ObservableCollection<UsuarioModel>(posts);
			var xx = _post.Where(x => x.Username == user.Username && x.Email == user.Email).FirstOrDefault();
			if (xx is null)
			{
				//messageLabel.Text = "Login fallido";
				Device.BeginInvokeOnMainThread(async () =>
				{
					await DisplayAlert("Recuperacion", "Su nombre de usuario o email son incorrectos, debe colocar datos validos.", "Ok").ConfigureAwait(true);
					await Task.Delay(2000).ConfigureAwait(true);
					


				});
				resultado = false;
			}
			else
			{
				//messageLabel.Text = "Login exitoso";
				//await Navigation.PushAsync(new CarpetaListPage());
				var url2 = Url += "/" + EntryUsername.Text + "/" + EntryCorreo.Text + "/1";
				content = await client.GetStringAsync(url2).ConfigureAwait(true);
                //posts = JsonConvert.DeserializeObject<List<UsuarioModel>>(content);
                //_post = new ObservableCollection<UsuarioModel>(posts);
                if (content is null)
				{ 
					
				}else
                {
					Device.BeginInvokeOnMainThread(async () =>
					{
						await DisplayAlert("Recuperacion", "El sistema le ha enviado un correo con las nuevas credenciales, ingrese nuevamente!", "Ok").ConfigureAwait(true);
						await Task.Delay(2000).ConfigureAwait(true);


					});
				}
				resultado = true;
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
	}
}
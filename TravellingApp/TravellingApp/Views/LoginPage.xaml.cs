using SysDatecScanApp.Models;
using SysDatecScanApp.RestAPIClient;
using SysDatecScanApp.ServicesHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
           
            RestClient<UserDetailCredentials> _restClient = new RestClient<UserDetailCredentials>();
            var getLoginDetails =  await _restClient.AuthenticateUser(EntryUsername.Text, EntryPassword.Text);

            if (getLoginDetails)
            {
                await DisplayAlert("Autenticacion exitosa", "Autenticacion de usuarios", "Ok", "Cancelar");
            }
            else
            {
                await DisplayAlert("Autenticacion erronea", "Username or Password son incorrectos o no existen", "Ok", "Cancelar");
            }
        }
    }
}

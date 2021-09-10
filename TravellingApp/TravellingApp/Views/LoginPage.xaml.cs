using LoginNavigation;
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
			Routing.RegisterRoute("AppShell", typeof(AppShell));
		}
		

		async void OnSignUpButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new SignUpPage());
		}

		async void OnLoginButtonClicked(object sender, EventArgs e)
		{
			var user = new UserDetailCredentials
			{
				Username = EntryUsername.Text,
				Password = EntryPassword.Text
			};

			var isValid = AreCredentialsCorrect(user);
			if (isValid)
			{
				App.IsUserLoggedIn = true;
				App.Current.MainPage  = new AppShell();
				//Navigation.InsertPageBefore(new App(), this);
				//await Navigation.PushModalAsync();
				//await Navigation.PopAsync();
			}
			else
			{
				messageLabel.Text = "Login fallido";
				EntryPassword.Text = string.Empty;
			}
		}

		bool AreCredentialsCorrect(UserDetailCredentials user)
		{
			bool res =  user.Username == Constants.Username && user.Password == Constants.Password;
			return res; 
		}

		/*private async void ButtonLogin_Clicked(object sender, EventArgs e)
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
        }*/
	}
}

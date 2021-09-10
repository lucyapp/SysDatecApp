using SysDatecScanApp;
using SysDatecScanApp.Models;
using System;
using System.Linq;
using Xamarin.Forms;

namespace LoginNavigation
{
	public partial class SignUpPage : ContentPage
	{
		public SignUpPage ()
		{
			InitializeComponent ();
		}

		async void OnSignUpButtonClicked (object sender, EventArgs e)
		{
			var user = new UserDetailCredentials () {
				Username = usernameEntry.Text,
				Password = passwordEntry.Text,
				Name = nameEntry.Text,
				Email = emailEntry.Text
			};

			// Sign up logic goes here

			var signUpSucceeded = AreDetailsValid (user);
			if (signUpSucceeded) {
				var rootPage = Navigation.NavigationStack.FirstOrDefault ();
				if (rootPage != null) {
					App.IsUserLoggedIn = true;
					Navigation.InsertPageBefore (new AppShell (), Navigation.NavigationStack.First ());
					await Navigation.PopToRootAsync ();
				}
			} else {
				messageLabel.Text = "Sign up failed";
			}
		}

		bool AreDetailsValid (UserDetailCredentials user)
		{
			return (!string.IsNullOrWhiteSpace (user.Username) && !string.IsNullOrWhiteSpace (user.Password) && !string.IsNullOrWhiteSpace (user.Email) && user.Email.Contains ("@"));
		}
	}
}

using SysDatecScanApp;
using SysDatecScanApp.Models;
using System;
using System.Linq;
using Xamarin.Forms;

namespace LoginNavigation
{
	public class SignUpPageCS : ContentPage
	{
		Entry usernameEntry, 
			passwordEntry,
			nameEntry,
			emailEntry;
		Label messageLabel;

		public SignUpPageCS ()
		{
			messageLabel = new Label ();
			usernameEntry = new Entry {
				Placeholder = "username"	
			};
			nameEntry = new Entry
			{
				Placeholder = "name"
			};
			passwordEntry = new Entry {
				IsPassword = true
			};
			emailEntry = new Entry ();
			var signUpButton = new Button {
				Text = "Sign Up"
			};
			signUpButton.Clicked += OnSignUpButtonClicked;

			Title = "Sign Up";
			Content = new StackLayout { 
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children = {
					new Label { Text = "Username" },
					usernameEntry,
					new Label { Text = "name" },
					nameEntry,
					new Label { Text = "Password" },
					passwordEntry,
					new Label { Text = "Email address" },
					emailEntry,
					signUpButton,
					messageLabel
				}
			};
		}

		async void OnSignUpButtonClicked (object sender, EventArgs e)
		{
			var user = new UserDetailCredentials() {
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

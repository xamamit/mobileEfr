
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Acs.Services.Helpers;
using Acs.Mobile.ESig.Controls;
using Acs.Mobile.ESig.ViewModels;
using Acs.Mobile.ESig.Views.Base;
using Xamarin.Forms;

namespace Acs.Mobile.ESig.Views
{
    public partial class LogView : ViewBase<LoginViewModel>
    {
        /// <summary>Initializes a new instance of the type.</summary>
        public LogView()
        {
            InitializeComponent();
            this.Title = "eSignature";

            usernameEntry.TextChanged += (snd, args) =>
            {
                IsUsernameNotNull = !String.IsNullOrWhiteSpace(usernameEntry.Text);
                IsAllValidationsTrue();
            };

            passwordEntry.TextChanged += (snd, args) =>
            {
                IsPasswordNotNull = !String.IsNullOrWhiteSpace(passwordEntry.Text);
                IsAllValidationsTrue();
            };
        }

        public bool IsUsernameNotNull = false;
        public bool IsPasswordNotNull = false;


        public void OnSave(object o, EventArgs e) { }

        public void IsAllValidationsTrue()
        {
            if (IsUsernameNotNull == false)
            {
                btnLogin.IsEnabled = false;
            }
            else if (IsPasswordNotNull == false)
            {
                btnLogin.IsEnabled = false;
            }
            else
            {
                btnLogin.IsEnabled = true;
            }
        }

        protected async override void OnAppearing()
        {
            // TODO: call base here?
            base.OnAppearing();

            //try
            //{
            //    //await ViewModel.Register();
            //}
            //catch (Exception exs)
            //{
            //    Debug.WriteLine($"Exception in LogView.xaml.cs - OnAppearing: message =: {exs.Message}");
            //    await DisplayAlert("Sorry!", "Something went wrong", "OK");
            //}
        }

		async void OnLoginButtonClicked(object sender, EventArgs e)
		{
   //         // THIS CODE WILL NEVER execute.
   //         if (false)
			//{
			//	await DisplayAlert("Save Settings", "Please save your settings before to continuing", "OK");

   //             //Navigation.InsertPageBefore(new SettingssView(), this);
   //             Application.Current.MainPage = new NavigationPage(new SettingsView());
				
			//	await Navigation.PopAsync().ConfigureAwait(false);

			//	return;
			//}

			var user = new User
			{
				Username = usernameEntry.Text,
				Password = passwordEntry.Text,
				BadgeBarcode = ""
			};

			// Authenticate the user.
			bool isValid = false;

			try
			{
				isValid = await ViewModel.AreCredentialsCorrect(user);
			}
			catch (Exception exs)
			{
                Debug.WriteLine($"Exception in LogView.cs - OnLoginButtonClicked() Message: {exs.Message}");
				await DisplayAlert("Sorry!", "Something went wrong", "OK");
			}

			if (isValid)
			{
				App.IsUserLoggedIn = true;
                App.IsLoggedInFirstTime = true;

                Application.Current.MainPage = new NavigationPage(new MainViewsMasters());
				await Navigation.PopAsync().ConfigureAwait(false);
			}
			else
			{
				messageLabel.Text = "Login failed";
				passwordEntry.Text = string.Empty;
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Acs.Services.Helpers;
using Acs.Mobile.ESig.Controls;
using Acs.Mobile.ESig.ViewModels;
using Acs.Mobile.ESig.Views.Base;
using Plugin.DeviceInfo;
using Xamarin.Forms;

namespace Acs.Mobile.ESig.Views
{
    public partial class SettingsView : ViewBase<LoginViewModel>
    {
        // TODO: all logic in code behind should be moved to ViewModel
        public SettingsView()
        {
            InitializeComponent();

            // TODO: move titles and static strings into a single class.
            this.Title = "Settings";

            btnSave.Clicked += OnSaveButtonClicked;

            passportServerURLEntry.TextChanged += (snd, args) =>
            {
                IsPasswordServerUrlNotNull = !string.IsNullOrEmpty(passportServerURLEntry.Text);
                IsAllvalidationsTrue();
            };

            domainNameEntry.TextChanged += (snd, args) =>
            {
                IsDomainNameNotNull = !string.IsNullOrEmpty(domainNameEntry.Text);
                IsAllvalidationsTrue();
            };              
        }

        public bool IsPasswordServerUrlNotNull = false;
        public bool IsDomainNameNotNull = false;

        // TODO: move to constant class and use the same filter everywhere.
        private const string EmailRegex = @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$";


        public void IsAllvalidationsTrue()
		{
			if (IsPasswordServerUrlNotNull == false)
			{
                btnSave.IsEnabled = false;
			}
			else if (IsDomainNameNotNull == false)
			{
                btnSave.IsEnabled = false;
			}
			else
			{
                btnSave.IsEnabled = true;
			}
		}
        
		async void OnBackButtonClicked(object sender, EventArgs e)
		{
            // Determine which page to navigate to.
		    Application.Current.MainPage = App.IsUserLoggedIn ? 
                new NavigationPage(new MainViewsMasters()) 
                : 
                new NavigationPage(new LogViewMasters());

		    await Navigation.PopAsync().ConfigureAwait(false);
		}

		async void OnSaveButtonClicked(object sender, EventArgs e)
		{
			var settings = new Setting()
			{
				PassportServerURL = passportServerURLEntry.Text,
				DomainName = domainNameEntry.Text
			};

            // Sign up logic
			var signUpSucceeded = AreDetailsValid(settings);

			if (false == signUpSucceeded)
			{
			    // TODO: move all static text and messages to a single class
			    errorMsgLabel.Text = "Settings seem to be incorrect";

			    return;
			}

		    Application.Current.MainPage = App.IsUserLoggedIn ?
		        new NavigationPage(new MainViewsMasters())
		        :
		        new NavigationPage(new LogViewMasters());

		    await Navigation.PopAsync().ConfigureAwait(false);
        }

		private bool AreDetailsValid(Setting settings)
		{
			bool IsValid = false;
			if (!string.IsNullOrEmpty(settings.PassportServerURL))
			{
				IsValid = (Regex.IsMatch(settings.PassportServerURL, EmailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
			}

		    return !string.IsNullOrWhiteSpace(settings.PassportServerURL)
                    &&
                    !string.IsNullOrWhiteSpace(settings.DomainName)
                    && IsValid;
        }

		public static bool CheckURLValid(string strURL)
		{
			return Uri.IsWellFormedUriString(strURL, UriKind.RelativeOrAbsolute);
		}

        protected async override void OnAppearing()
        {
			base.OnAppearing();

			Domain.Models.ResponseModels.RegisterModel registrationInfo = await ViewModel.GetDeviceStatusAsync();

            // TODO: move Status to enum as in specification
			if (registrationInfo.Status == "0")
			{
				Domain.Models.ResponseModels.RegisterModel regMod = await ViewModel.Register();

                // TODO: add if DEBUG before going to prod
				if (regMod.Success)
				{
					var answer = await DisplayAlert("Device Registration", "This device is not approved for use with the eSignature app. Would you like to request this device be approved for use with the eSignature app?", "Yes", "No");
                    
                    // Request hasn't been sent nor processed -- debug will approve
					if (answer)
					{
                        // TODO: Remove automatic approval of device. 
						await ViewModel.ChangeDeviceStatus();

						await DisplayAlert("Device Registration", "A request has been sent to register this decive for use with the eSignature app. The approval process can take between 1 - 3 business days. Please check back tomorrow", "OK");
					}
					else
					{
					}
				}
			}
            // Registration request received, but not processed yet.
			else if (registrationInfo.Status == "1")
			{
				await DisplayAlert("Device Registration", "A request to approve this device for use with the eSignature app has been sent, but approval hasn't been granted yet. Please check back tomorrow.\" This device is pending approval request has been sent to have this device approved for use with the eSignature app. The approval process can take between 1 - 3 business days. Please check back tomorrow", "OK");
			}
            // Success - Device has been approved and can run this application
			else if (registrationInfo.Status == "2")
			{
				Settings.SetAuthorizationToken(registrationInfo.AuthToken);
			}
            // Device not allowed to run this application
			else if (registrationInfo.Status == "3")
			{
				await DisplayAlert("Device Registration", "This mobile application is not allowed to run on this device. Please contact support if you need help. The app will close now", "OK");
			}
            // Request was declined
			else if (registrationInfo.Status == "4")
			{
				await DisplayAlert("Device Registration", "The request to register this device was declined by a system admin and blocked from connecting to Passport", "OK");
			}
        }
    }
}
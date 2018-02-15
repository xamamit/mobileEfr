using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Acs.Services.Helpers;
using Acs.Mobile.EFR.Controls;
using Acs.Mobile.EFR.ViewModels;
using Acs.Mobile.EFR.Views.Base;
using Xamarin.Forms;
using Acs.Mobile.EFR.Helpers;
using Plugin.DeviceInfo;
using System.Diagnostics;

namespace Acs.Mobile.EFR.Views
{
    public partial class SettingsView : ViewBase<RegisterationViewModel>
    {
        public bool IsPasswordServerUrlValid = false;
        public bool IsDomainNameValid = false;
        public bool IsSaveAllowed = false;

        public SettingsView()
        {
            InitializeComponent();

            this.Title = "Application Settings";

            // Bind the controls to default values.
            passportServerURLEntry.BindingContext = Constants.DefaultPassportServerUrl;
            domainNameEntry.BindingContext = Constants.DefaultDomainName;

            ac.Color = Color.FromHex(AppStyle.ActivityIndicatorColor);
            btnSave.BackgroundColor = Color.FromHex(AppStyle.ButtonBackGroundColor);

            btnSave.Clicked += OnSaveButtonClicked;

            passportServerURLEntry.TextChanged += (snd, args) =>
            {
                IsPasswordServerUrlValid = !string.IsNullOrEmpty(passportServerURLEntry.Text);
                AllValidationsAreTrue();
            };

            domainNameEntry.TextChanged += (snd, args) =>
            {
                IsDomainNameValid = !string.IsNullOrEmpty(domainNameEntry.Text);
                AllValidationsAreTrue();
            };
        }

        /// <summary>
        /// Gets a value indicating whether Url to the Passport servier is valid.
        /// </summary>
        /// <value><c>true</c> if well formed; otherwise, <c>false</c>.</value>
        public bool PassportServerUrlValid
        {
            get => String.IsNullOrWhiteSpace(passportServerURLEntry.Text);
        }

        /// <summary>
        /// Gets a value indicating whether domain related to this app is valid.
        /// </summary>
        /// <value><c>true</c> if <c>domain name</c>; otherwise, <c>false</c>.</value>
        public bool DomainNameValid
        {
            get => String.IsNullOrWhiteSpace(domainNameEntry.Text);
        }

        public void AllValidationsAreTrue()
        {
            if (IsPasswordServerUrlValid == false)
            {
                btnSave.IsEnabled = false;
            }
            else if (IsDomainNameValid == false)
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
           // Application.Current.MainPage = new NavigationPage(new MainViewsMasters());
          
            await Navigation.PopAsync();
            await Navigation.PushAsync(new MainViewsMasters());
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var settings = new AppSettingsModel()
            {
                PassportServerUrl = passportServerURLEntry.Text,
                DomainName = domainNameEntry.Text
            };

            // Sign up logic goes here

            using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
            {

            var signUpSucceeded = AreUserSettingsValid(settings);

            if (signUpSucceeded)
            {
                Acr.Settings.Settings.Current.SetValue("IsSettingsSaved", "true");



                if (CrossDevice.Hardware.OperatingSystem == "WINDOWS")
                {
                    try
                    {
                        Application.Current.MainPage = new NavigationPage(new MainViewsMasters());
                    }
                    catch
                    {

                    }
                }
                else
                {
                    await Navigation.PushAsync(new MainViewsMasters());
                }



                /*    if (App.IsUserLoggedIn)
                    {
                        Application.Current.MainPage = new MainViewsMasters();
                    }
                    else {
                        Application.Current.MainPage = new LogViewMasters();
                    }
                    */
                // await Navigation.PopAsync().ConfigureAwait(false);
            }
            else
            {
                errorMsgLabel.Text = "Settings are incorrect";
            }
        }
        }


        /// <summary>Provides a collective value indicating if the user settings valid.</summary>
        private bool AreUserSettingsValid(AppSettingsModel appSettings)
        {
            bool isValid = false;

            if (!string.IsNullOrWhiteSpace(appSettings.PassportServerUrl))
            {
                isValid = (Regex.IsMatch(
                    appSettings.PassportServerUrl,
                    Constants.EmailRegex,
                    RegexOptions.IgnoreCase,
                    TimeSpan.FromMilliseconds(250))
                    );
            }

            return (!string.IsNullOrWhiteSpace(appSettings.PassportServerUrl) &&
                    !string.IsNullOrWhiteSpace(appSettings.DomainName) && isValid);
        }


        public static bool CheckURLValid(string strUrl)
        {
            return Uri.IsWellFormedUriString(strUrl, UriKind.RelativeOrAbsolute);
        }


        //
        // ***************************** REGISTRATION ***************************** 
        //
        // THIS SHOULD BE BROKEN INTO A DIFFERENT PAGE OR pagecontent and inserted into the parent.
        //
        // Just before the page becomes visible.
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Domain.Models.ResponseModels.RegisterDeviceModel registrationInfo = await ViewModel.GetDeviceStatusAsync();

            // TODO: move Status to enum as in specification
            if (registrationInfo.Status == "0")
            {
                Domain.Models.ResponseModels.RegisterDeviceModel regMod = await ViewModel.RegisterDeviceAsync();

                // TODO: add if DEBUG before going to prod
                if (regMod.Success)
                {
                    var answer = await DisplayAlert("Device Registration", "This device has not been approved to run the EFR " +
                        "app. Would you like to submit an approval request so this device can be approved to run the EFR app?",
                        "Yes", "No");

                    // Request hasn't been sent nor processed -- debug will approve
                    if (answer)
                    {
                        // TODO: Remove automatic approval of device. 
                        await ViewModel.ChangeDeviceStatusAsync();

                        await DisplayAlert("Device Registration", "An approval request for this device to be used with the EFR " +
                            "app has been sent. The approval process can take a few business days. Please check back tomorrow.", "OK");
                    }
                    else
                    {
                    }
                }
            }
            // Registration request received, but not processed yet.
            else if (registrationInfo.Status == "1")
            {
                await DisplayAlert("Device Registration", "An approval request for this device to be used with the EFR app has " +
                    "been sent, but the approval has yet to be granted. The approval process can take a few business days. Please " +
                    "check back tomorrow.", "OK");
            }
            // Success - Device has been approved and can run this application
            else if (registrationInfo.Status == "2")
            {
                //TODO: Settings needs to be moved out of Services.
                Settings.SetAuthorizationToken(registrationInfo.AuthToken);
            }
            // Device not allowed to run this application
            else if (registrationInfo.Status == "3")
            {
                await DisplayAlert("Device Registration", "This mobile application is not allowed to run on this device. Please " +
                    "contact support if you need help. The app will now close.", "OK");
            }
            // Request was declined
            else if (registrationInfo.Status == "4")
            {
                await DisplayAlert("Device Registration", "The request to register this device was declined by a system admin and " +
                    "blocked from connecting to Passport. The app will now close.", "OK");
            }
        }
    }
}
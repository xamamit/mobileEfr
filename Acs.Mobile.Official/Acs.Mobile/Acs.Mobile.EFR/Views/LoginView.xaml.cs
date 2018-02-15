using System;
using System.Diagnostics;
using Acr.UserDialogs;
using Acs.Mobile.EFR.Helpers;
using Acs.Mobile.EFR.ViewModels;
using Acs.Mobile.EFR.Views.Base;
using Acs.Services.Helpers;
using Plugin.DeviceInfo;
using Xamarin.Forms;

namespace Acs.Mobile.EFR.Views
{
    public partial class LoginView : ViewBase<LoginViewModel>
    {
        public bool IsUserNameValid = false;
        public bool IsPasswordValid = false;

        public LoginView()
        {
            InitializeComponent();
            this.Title = Constants.Title_LoginPage;

            if (CrossDevice.Hardware.OperatingSystem == "WINDOWS")
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }

            btnLogin.BackgroundColor = Color.FromHex(AppStyle.ButtonBackGroundColor);
            
            usernameEntry.TextChanged += (snd, args) =>
            {
                IsUserNameValid = !string.IsNullOrWhiteSpace(usernameEntry.Text);
                IsAllvalidationsTrue();
            };

            passwordEntry.TextChanged += (snd, args) =>
            {
                IsPasswordValid = !string.IsNullOrEmpty(passwordEntry.Text);
                IsAllvalidationsTrue();
            };

            btnLogin.Clicked += OnLoginButtonClicked;
        }

        public void OnSave(object o, EventArgs e) { }

        public void IsAllvalidationsTrue()
        {
            btnLogin.IsEnabled = (IsUserNameValid == false) ? false : (IsPasswordValid == false) ? false : true;
        }

        protected async override void OnAppearing()
        {
            // invoked just before the screen appears to the user.
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

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            Domain.Models.RequestModels.AuthModel user = new Domain.Models.RequestModels.AuthModel
            {
                LoginName = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            // Authenticate the user.

            using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
            {

            bool isValid = false;


            try
            {
                isValid = await ViewModel.AuthenticateUserAsync(user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in LoginView.xaml.cs -> OnLoginButtonClicked: {ex.Message}");
                await DisplayAlert(Constants.ErrorMsgTitleOops, Constants.ErrorMsgSorry, "OK").ConfigureAwait(false);
            }

            if (isValid)
            {
                App.IsUserLoggedIn = true;
                App.IsLoggedInFirstTime = true;



                if (CrossDevice.Hardware.OperatingSystem == "WINDOWS")
                {
                    Application.Current.MainPage = new NavigationPage(new MainViewsMasters());
                }
                else
                {
                    await Navigation.PushAsync(new MainViewsMasters());
                }



                // await Navigation.PopAsync().ConfigureAwait(false);
            }
            else
            {
                // login was not a successful attempt, so notify the user... vaguely
                messageLabel.Text = "Invalid user name or password. Please try again.";
                passwordEntry.Text = string.Empty;
            }
        }
        }


    }
}
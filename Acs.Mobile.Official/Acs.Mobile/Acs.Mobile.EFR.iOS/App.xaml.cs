
using System;
using System.Diagnostics;
// using System.Net.Mime;
using Xamarin.Forms;
using Acs.Mobile.EFR.Views;
using Acs.Mobile.EFR.Configuration;
using Acs.Mobile.EFR.Helpers;
//using Acr.Settings;

namespace Acs.Mobile.EFR
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }

        public static bool IsFormsAdded { get; set; }

        public static string PhoneType { get; set; }


        public static bool IsLoggedInFirstTime = false;

        public App(AppSetup appSetup)
        {
            try
            {
                IsUserLoggedIn = false;
                IsFormsAdded = false;
                string checkSettings = String.Empty;

                // Create the Autofac IOC container.
                AppContainer.Container = appSetup.CreateContainer();


                // Determine if we have settings and / or if the user is logged in.

                if (checkSettings != "true")
                {
                    MainPage = new NavigationPage(new SettingsView())
                    {
                        BarBackgroundColor = Color.FromHex(AppStyle.BarBackGroundColor),
                        BarTextColor = Color.FromHex(AppStyle.BarTextColor)
                    };

                    return;
                }

                if (!IsUserLoggedIn)
                {

                    MainPage = new NavigationPage(new LogViewMasters())
                    {
                        BarBackgroundColor = Color.FromHex(AppStyle.BarBackGroundColor),
                        BarTextColor = Color.White
                    };
                }
                else
                {
                    MainPage = new NavigationPage(new MainViewsMasters());
                }
            }
            catch (Exception esx)
            {
                Debug.WriteLine(esx);
            }
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}

using Xamarin.Forms;
using Acs.Mobile.ESig.Views;
using Acs.Mobile.ESig.Configuration;
using Xamarin.Forms;
using Acs.Mobile.ESig.Controls;
using Acs.Services.Helpers;
using System;
using System.Diagnostics;

namespace Acs.Mobile.ESig
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }

        public static bool IsFormsAdded { get; set; }

        public static string PhoneType { get; set; }

        public static string IsSettings { get; set; }

        public static bool ProcessFormsIsBack = false;

        public static bool IsLoggedInFirstTime = false;

		
        public App(AppSetup appSetup)
        {
            // TODO: Dispose of AppSetup
            try
            {              
                IsUserLoggedIn = false;
                IsFormsAdded = false;

                string checkSettings = String.Empty;

                AppContainer.Container = appSetup.CreateContainer();
              
             //   checkSettings = DependencyService.Get<IUserPreferences>().GetString("IsSettingsSaved");

                if (checkSettings != "true")
                {
                    // MainPage = new NavigationPage(new Index())
                    MainPage = new NavigationPage(new SettingsView())
                    {
                        // TODO: refactor colors out to style resources in shared.
                        // Just as planned
                        BarBackgroundColor = Color.FromHex(AppStyle.barBackGroundColor),
                        BarTextColor = Color.FromHex(AppStyle.barTextColor)
                    };

                    return;
                }

                if (!IsUserLoggedIn)
                {
                    // MainPage = new NavigationPage(new Index())

                   //DependencyService.Get<IUserPreferences>().GetString("SetVersion");

                    //Settings.SetAuthorizationToken(DependencyService.Get<IUserPreferences>().GetString("SetAuthorizationToken"));
                        //Settings.SetSerialNumber(DependencyService.Get<IUserPreferences>().GetString("SetSerialNumber"));
                        //Settings.SetDomainName(DependencyService.Get<IUserPreferences>().GetString("SetDomainName"));
                        //Settings.SetApplicationId(DependencyService.Get<IUserPreferences>().GetString("SetApplicationId"));
                        //Settings.SetVersion(DependencyService.Get<IUserPreferences>().GetString("SetVersion"));

                    MainPage = new NavigationPage(new LogViewMasters())
                    {
                        // TODO: refactor colors out to style resources in shared.
                        // Just as planned
                        BarBackgroundColor = Color.FromHex(AppStyle.barBackGroundColor),
                        BarTextColor = Color.White
                    };
                }
                else
                {
					//Settings.SetAuthorizationToken(DependencyService.Get<IUserPreferences>().GetString("SetAuthorizationToken"));
					//Settings.SetSerialNumber(DependencyService.Get<IUserPreferences>().GetString("SetSerialNumber"));
					//Settings.SetDomainName(DependencyService.Get<IUserPreferences>().GetString("SetDomainName"));
					//Settings.SetApplicationId(DependencyService.Get<IUserPreferences>().GetString("SetApplicationId"));
					//Settings.SetVersion(DependencyService.Get<IUserPreferences>().GetString("SetVersion"));


                    MainPage = new NavigationPage(new MainViewsMasters());
                    // MainPage = new NavigationPage(new Index());
                }
            }
            catch(Exception esx){

                Debug.Write(esx);
            }            
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}
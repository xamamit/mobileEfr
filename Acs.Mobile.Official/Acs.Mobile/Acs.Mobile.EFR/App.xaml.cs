
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Acs.Mobile.EFR.Views;
using Acs.Mobile.EFR.Configuration;
using Acs.Mobile.EFR.Helpers;
using Acs.Common.Util;
using Xamarin.Forms.Xaml;
using Acr.Settings;
using Plugin.DeviceInfo;

namespace Acs.Mobile.EFR
{
    public partial class App : Application
    {
        #region Public Properties
        //
        // It's not necessary to document thfiles.e properties below since they are self-explanatory. It would 
        // only clutter the 
        //

        public static bool IsUserLoggedIn = false;

        public static bool IsFormsAdded { get; set; }

        public static string PhoneType { get; set; }
        
        /// <summary>Keeps track whether it's the first time the user has logged in. This is important 
        /// because the user needs to grant permission for the app to access certain resources on the device.
        /// </summary>
        public static bool IsLoggedInFirstTime = false;

        #endregion Public Properties

        /// <summary>Initializes a new instance of the <see cref="App"/> class. The IoC container is created 
        /// and the app is kicked off.
        /// </summary>
        /// <param name="appSetup">The <see cref="Acs.Mobile.EFR.Configuration.AppSetup"/> that is responsible 
        /// for facilitating the registration of the types for IoC.</param>
        public App(AppSetup appSetup)
        {
            try
            {

            
                IsFormsAdded = false;
                string checkSettings = String.Empty;
                checkSettings = Settings.Current.Get<string>("IsSettingsSaved");

                // Create the Autofac IOC container.
                AppContainer.Container = appSetup.CreateContainer();

                // Determine if we have settings and / or if the user is logged in.
                if (checkSettings != "true")
                {
                    MainPage = new NavigationPage(new SettingsView())
                    {
                        
						// this line was commented out in my work from yesterday.
						// BarBackgroundColor = Color.FromHex(AppStyle.BarBackGroundColor),
                        
                    };

                    return;
                }

                if (!IsUserLoggedIn)
                {
                   
                    if (CrossDevice.Hardware.OperatingSystem == "WINDOWS")
                    {

                        MainPage = new NavigationPage(new MainViewsMasters())
                        {
                            // BarBackgroundColor = Color.FromHex(AppStyle.BarBackGroundColor),
                            // BarTextColor = Color.White
                        };

                    }
                    else
                    {
                        MainPage =new NavigationPage(new MainViewsMasters())
                        {
                            // BarBackgroundColor = Color.FromHex(AppStyle.BarBackGroundColor),
                            // BarTextColor = Color.White
                        };


                    }


                }
                else
                {
                    if (CrossDevice.Hardware.OperatingSystem == "WINDOWS")
                    {

                        MainPage = new NavigationPage(new MainViewsMasters())
                        {
                            // BarBackgroundColor = Color.FromHex(AppStyle.BarBackGroundColor),
                            // BarTextColor = Color.White
                        };

                    }
                    else
                    {
                        MainPage = new NavigationPage(new MainViewsMasters())
                        {
                            // BarBackgroundColor = Color.FromHex(AppStyle.BarBackGroundColor),
                            // BarTextColor = Color.White
                        };


                    }
                }
            }
            catch (Exception esx)
            {
                Debug.WriteLine($"EXCEPTION: {esx.Message}");
            }
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}
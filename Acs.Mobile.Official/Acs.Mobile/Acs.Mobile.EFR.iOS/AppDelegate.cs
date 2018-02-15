using System;
using System.Collections.Generic;
using System.Linq;
using Acs.Mobile.EFR.iOS.Controls;
using FormsPlugin.Iconize.iOS;
using Foundation;
using Plugin.Iconize;
using UIKit;

namespace Acs.Mobile.EFR.iOS
{
    /// <summary>
    /// The UIApplicationDelegate for the application. This class is responsible for launching the User 
    /// Interface of the application, as well as listening(and optionally responding) to application events from iOS.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.Platform.iOS.FormsApplicationDelegate" />
    [Register("AppDelegate")]
    public partial class AppDelegate :
        global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public static string PHONE_TYPE = "IOS";

        /// <summary>
        /// ***************************************************************************************
        /// YOU HAVE 17 SECONDS to return from this method, or iOS will terminate your application.
        /// ***************************************************************************************
        ///
        /// This method is invoked when the application has loaded and is ready to run. In this method you should 
        /// instantiate the window, load the UI into it and then make the window visible.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="options">The options.</param>
        /// <returns><c>true</c> is the application finishes loading, otherwise, <c>false</c>.</returns>
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Iconize.With(new Plugin.Iconize.Fonts.EntypoPlusModule())
                .With(new Plugin.Iconize.Fonts.FontAwesomeModule())
                .With(new Plugin.Iconize.Fonts.IoniconsModule())
                .With(new Plugin.Iconize.Fonts.MaterialModule())
                .With(new Plugin.Iconize.Fonts.MeteoconsModule())
                .With(new Plugin.Iconize.Fonts.SimpleLineIconsModule())
                .With(new Plugin.Iconize.Fonts.TypiconsModule())
                .With(new Plugin.Iconize.Fonts.WeatherIconsModule());

            global::Xamarin.Forms.Forms.Init();

            IconControls.Init();

            App.PhoneType = PHONE_TYPE;
           
            LoadApplication(new App(new Setup()));

            return base.FinishedLaunching(app, options);
        }
    }
}
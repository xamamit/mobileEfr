using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Xamarin.Forms.Platform.UWP;

using Acs.Mobile.EFR;

namespace Acs.Mobile.EFR.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            // We are calling back into the main application (Acs.Mobile.EFR) so the dependencies and setup will 
            // be handled via IoC.
            LoadApplication(new Acs.Mobile.EFR.App(new Setup()));
        }
    }
}

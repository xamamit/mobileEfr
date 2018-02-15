
using System;
using System.Diagnostics;
using Acs.Mobile.EFR.Models;
using Acs.Mobile.EFR.ViewModels;
using Acs.Mobile.EFR.Views.Base;
using Plugin.DeviceInfo;

namespace Acs.Mobile.EFR.Views
{
    /// <summary>
    /// View that will display provided information to the user about the application and its use.
    /// </summary>
    /// <seealso cref="Acs.Mobile.EFR.Views.Base.ViewBase{Acs.Mobile.EFR.ViewModels.AboutViewModel}" />
    public partial class AboutView : ViewBase<AboutViewModel>
    {
        public AboutView()
        {
            
            InitializeComponent();
            this.Title = Constants.Title_AboutPage;

            try
            {               
                ViewModel.SetAboutValues();
                BindingContext = ViewModel.BindingContext;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"EXCEPTION: AboutView.cs --> AboutView() ctr: {ex.Message}");
            }
        }
    }
}
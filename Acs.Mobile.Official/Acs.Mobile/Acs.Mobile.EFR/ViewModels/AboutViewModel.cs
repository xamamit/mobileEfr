
using System;
using Acs.Mobile.EFR.Models;
using Acs.Mobile.EFR.ViewModels.Base;
using Plugin.DeviceInfo;

namespace Acs.Mobile.EFR.ViewModels
{
    public class AboutViewModel : ViewModelBase, IViewModel
    {
        private AboutModel _aboutModel;

        /// <summary>Initializes a new instance of the <see cref="AboutViewModel"/> class.</summary>
        public AboutViewModel() { }

        /// <summary>Sets the values for the <c>About</c> page can display them.</summary>
        public void SetAboutValues()
        {
            _aboutModel = new AboutModel();
            _aboutModel.DeviceVersion = CrossDevice.Hardware.OperatingSystemVersion;
            _aboutModel.DeviceId = CrossDevice.Hardware.DeviceId; 
            _aboutModel.DevicePlatform = CrossDevice.Hardware.OperatingSystem;
            _aboutModel.DeviceModel = CrossDevice.Hardware.Model;
            _aboutModel.AppVersion = CrossDevice.App.Version;

            BindingContext = _aboutModel;
        }
    }
}
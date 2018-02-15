
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Acs.Services.AuthServices;
using Acs.Mobile.ESig.ViewModels.Base;

using Acs.Services;
using Acs.Services.RegistrationServices;
using Acs.Services.Helpers;
using Acs.Domain.Models;

using Plugin.DeviceInfo;
using Acs.Mobile.ESig.Controls;
using Xamarin.Forms;


namespace Acs.Mobile.ESig.ViewModels
{
    public class RegisterationViewModel : ViewModelBase, IViewModel
    {
        private IRegistrerDeviceService _registerService;

        // TODO: These should go in a global config.
        private static readonly string DefaultDomainName = "accessefm";
        private static readonly string DefaultApplicationId = "Acs.ESig.Mobile";

        /// <summary>Initializes and instance of the type. </summary>
        /// <param name="registerService">A RegistrationService facilitating registration of a device.</param>
        public RegisterationViewModel(IRegistrerDeviceService registerService)
        {
            if (null == registerService)
            {
                Debug.WriteLine($"Registration service was null in RegisterationViewModel constructor.");
                return;
            }

            _registerService = registerService;
        }

        public async Task<Domain.Models.ResponseModels.RegisterModel> RegisterDeviceAsync()
        {
            Acs.Domain.Models.RequestModels.RegisterModel deviceInfo = GetDeviceDetails();

            //Settings.SetSerialNumber(CrossDevice.Hardware.DeviceId);            
            //Settings.SetDomainName("accessefm");            
            //Settings.SetApplicationId("Acs.ESig.Mobile");
            //Settings.SetVersion(CrossDevice.App.Version);            

            //Acs.Domain.Models.RequestModels.RegisterModel deviceInfo = new Acs.Domain.Models.RequestModels.RegisterModel();

            //deviceInfo.SerialNumber = Settings.GetSerialNumber();
            //deviceInfo.AuthToken = Settings.GetAuthorizationToken();
            //deviceInfo.DeviceName = CrossDevice.Hardware.DeviceId;
            //deviceInfo.OS = CrossDevice.Hardware.OperatingSystem;
            //deviceInfo.Version = CrossDevice.Hardware.OperatingSystemVersion;
            //deviceInfo.Manufacturer = CrossDevice.Hardware.Manufacturer;
            //deviceInfo.Model = CrossDevice.Hardware.Model;
            
            // Register device and set AuthToken
            Domain.Models.ResponseModels.RegisterModel registerModel = await _registerService.RegisterDeviceAsync(deviceInfo);
            Settings.SetAuthorizationToken(registerModel.AuthToken);

            // TODO: Remove hard-coded strings.
            DependencyService.Get<IUserPreferences>().SetString("SetAuthorizationToken", registerModel.AuthToken);

            return registerModel;
        }

        public async Task<Domain.Models.ResponseModels.RegisterModel> GetDeviceStatusAsync()
        {
            //SetDefaultSettings();

            //Settings.SetSerialNumber(CrossDevice.Hardware.DeviceId);
            //Settings.SetDomainName("accessefm");
            //Settings.SetApplicationId("2");
            //Settings.SetVersion(CrossDevice.App.Version);
            
            //Acs.Domain.Models.RequestModels.RegisterModel deviceInfo = new Acs.Domain.Models.RequestModels.RegisterModel();

            //deviceInfo.SerialNumber = Settings.GetSerialNumber();
            //deviceInfo.AuthToken = Settings.GetAuthorizationToken();
            //deviceInfo.DeviceName = CrossDevice.Hardware.DeviceId;
            //deviceInfo.OS = CrossDevice.Hardware.OperatingSystem;
            //deviceInfo.Version = CrossDevice.Hardware.OperatingSystemVersion;
            //deviceInfo.Manufacturer = CrossDevice.Hardware.Manufacturer;
            //deviceInfo.Model = CrossDevice.Hardware.Model;

            // Domain.Models.ResponseModels.RegisterModel sd = await _authService.VerifyStatus(deviceInfo);

            Acs.Domain.Models.RequestModels.RegisterModel deviceInfo = GetDeviceDetails();

            Domain.Models.ResponseModels.RegisterModel sd = await _registerService.GetDeviceStatusAsync(deviceInfo);
            Settings.SetAuthorizationToken(sd.AuthToken);

            //DependencyService.Get<IUserPreferences>().SetString("SetAuthorizationToken", sd.AuthToken);

            return sd;
        }

        // TODO: Move to register vm
        public Task ChangeDeviceStatusAsync()
        {
            //SetDefaultSettings();

            //Settings.SetSerialNumber(CrossDevice.Hardware.DeviceId);
            //Settings.SetDomainName("accessefm");
            //Settings.SetApplicationId("2");
            //Settings.SetVersion(CrossDevice.App.Version);

            //Acs.Domain.Models.RequestModels.RegisterModel deviceInfo = new Acs.Domain.Models.RequestModels.RegisterModel();

            //deviceInfo.SerialNumber = Settings.GetSerialNumber();
            //deviceInfo.AuthToken = Settings.GetAuthorizationToken();
            //deviceInfo.DeviceName = CrossDevice.Hardware.DeviceId;
            //deviceInfo.OS = CrossDevice.Hardware.OperatingSystem;
            //deviceInfo.Version = CrossDevice.Hardware.OperatingSystemVersion;
            //deviceInfo.Manufacturer = CrossDevice.Hardware.Manufacturer;
            //deviceInfo.Model = CrossDevice.Hardware.Model;


            Acs.Domain.Models.RequestModels.RegisterModel deviceInfo = GetDeviceDetails();
            return _registerService.ChangeDeviceStatusAsync(deviceInfo);

            //Settings.SetAuthorizationToken(sd.AuthToken);
        }

        private Acs.Domain.Models.RequestModels.RegisterModel GetDeviceDetails()
        {
            Settings.SetSerialNumber(CrossDevice.Hardware.DeviceId);
            Settings.SetDomainName(DefaultDomainName);
            Settings.SetApplicationId(DefaultApplicationId);
            Settings.SetVersion(CrossDevice.App.Version);

            Acs.Domain.Models.RequestModels.RegisterModel deviceInfo = new Acs.Domain.Models.RequestModels.RegisterModel();

            deviceInfo.SerialNumber = Settings.GetSerialNumber();
            deviceInfo.AuthToken = Settings.GetAuthorizationToken();
            deviceInfo.DeviceName = CrossDevice.Hardware.DeviceId;
            deviceInfo.OS = CrossDevice.Hardware.OperatingSystem;
            deviceInfo.Version = CrossDevice.Hardware.OperatingSystemVersion;
            deviceInfo.Manufacturer = CrossDevice.Hardware.Manufacturer;
            deviceInfo.Model = CrossDevice.Hardware.Model;

            return deviceInfo;
        }
    }
}
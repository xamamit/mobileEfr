
using System.Diagnostics;
using System.Threading.Tasks;

using Plugin.DeviceInfo;
using Acs.Mobile.EFR.ViewModels.Base;

using Acs.Services.RegistrationServices;
using Acs.Services.Helpers;

namespace Acs.Mobile.EFR.ViewModels
{
    /// <summary>
    /// Represents a device's status in the Passport system as it related to accessing an application.
    /// </summary>
    public enum RegistrationStatus
    {
        NotRegistered = 0,
        PendingApproval = 1,    
        Approved = 2,
        ApplicationNotAllowed = 3,
        Blocked = 4
    }


    /// <summary>
    ///     Provides the general functionality for the features associated with <c>Registrating</c> a device.
    /// </summary>
    /// <seealso cref="Acs.Mobile.EFR.ViewModels.Base.ViewModelBase" />
    /// <seealso cref="Acs.Mobile.EFR.ViewModels.Base.IViewModel" />
    public class RegisterationViewModel : ViewModelBase, IViewModel
    {
        private Services.RegistrationServices.IRegistrerDeviceService _registerService;

        /// <summary>Initializes and instance of the type. </summary>
        /// <param name="registerService">A RegistrationService facilitating registration of a device.</param>
        public RegisterationViewModel(Services.RegistrationServices.IRegistrerDeviceService registerService)
        {
            if (null == registerService)
            {
                Debug.WriteLine($"Registration service was null in RegisterationViewModel constructor.");
                return;
            }
            _registerService = registerService;
        }

        /// <summary>Registers the device asynchronously.</summary>
        /// <returns>A Task<Domain.Models.ResponseModels.RegisterDeviceModel>.</returns>
        public async Task<Domain.Models.ResponseModels.RegisterDeviceModel> RegisterDeviceAsync()
        {
            Acs.Domain.Models.RequestModels.RegisterDeviceModel deviceInfo = GetDeviceDetails();

            // Register device and set AuthToken
            Domain.Models.ResponseModels.RegisterDeviceModel registerModel = await _registerService.RegisterDeviceAsync(deviceInfo);

            Settings.SetAuthorizationToken(registerModel.AuthToken);

            return registerModel;
        }

        /// <summary>Gets the device status indicating if the device can be used with the system asynchronous.</summary>
        public async Task<Domain.Models.ResponseModels.RegisterDeviceModel> GetDeviceStatusAsync()
        {
            Acs.Domain.Models.RequestModels.RegisterDeviceModel deviceInfo = GetDeviceDetails();

            Domain.Models.ResponseModels.RegisterDeviceModel sd = await _registerService.GetDeviceStatusAsync(deviceInfo);

            Settings.SetAuthorizationToken(sd.AuthToken);

            return sd;
        }

        /// <summary>Allows the device's status to be changed changed asynchronous.</summary>
        public Task ChangeDeviceStatusAsync()
        {
            Acs.Domain.Models.RequestModels.RegisterDeviceModel deviceInfo = GetDeviceDetails();

            return _registerService.ChangeDeviceStatusAsync(deviceInfo);
        }

        /// <summary>Helper method to obtain the disparate pieces of information about the device.</summary>
        private Acs.Domain.Models.RequestModels.RegisterDeviceModel GetDeviceDetails()
        {
            Settings.SetSerialNumber(CrossDevice.Hardware.DeviceId);

            Settings.SetDomainName(Constants.DefaultDomainName);
            Settings.SetApplicationId(Constants.DefaultApplicationId);
            Settings.SetVersion(CrossDevice.App.Version);

            Acs.Domain.Models.RequestModels.RegisterDeviceModel deviceInfo = new Acs.Domain.Models.RequestModels.RegisterDeviceModel();

            deviceInfo.SerialNumber = Settings.GetSerialNumber();
            deviceInfo.AuthToken = Settings.GetAuthorizationToken();
            deviceInfo.DeviceName =CrossDevice.Hardware.Manufacturer+" "+CrossDevice.Hardware.Model;
            deviceInfo.OS = CrossDevice.Hardware.OperatingSystem;
            deviceInfo.Version = CrossDevice.Hardware.OperatingSystemVersion;
            deviceInfo.Manufacturer = CrossDevice.Hardware.Manufacturer;
            deviceInfo.Model = CrossDevice.Hardware.Model;

            return deviceInfo;
        }
    }
}

using System;
using Acs.Services;
using Acs.Services.AuthServices;
using Acs.Services.Helpers;
using Acs.Mobile.EFR.ViewModels.Base;
using Acs.Domain.Models;
using Acs.Domain.Models.RequestModels;
using Acs.Domain.Models.ResponseModels;
using System.Threading.Tasks;
using Plugin.DeviceInfo;
using Acs.Mobile.EFR.Controls;
using Xamarin.Forms;
using System.ComponentModel;

namespace Acs.Mobile.EFR.ViewModels
{
    public class LoginViewModel : ViewModelBase, IViewModel, INotifyPropertyChanged
    {
        /// <summary>Initializes a new instance of the <see cref="LoginViewModel"/> class.</summary>
        public LoginViewModel() { }

        /// <summary>Initializes a new instance of the <see cref="LoginViewModel"/> class.</summary>
        /// <remarks>Constructor to be used with IoC and DI and allow easier unit testing.</remarks>
        /// <param name="authService">The authentication service.</param>
        public LoginViewModel(Services.AuthServices.IESigAuthService authService)
        {
            _authService = authService;

            Settings.SetSerialNumber(CrossDevice.Hardware.DeviceId);
            Settings.SetVersion(CrossDevice.App.Version);
            Settings.SetDomainName(Constants.DefaultDomainName);
            Settings.SetApplicationId(Constants.DefaultApplicationId);
        }


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



        private Services.AuthServices.IESigAuthService _authService;
        
        public string UserName { get; set; }

        public string Password { get; set; }

        public string SelectedDomain { get; set; }

        public async Task<bool> AuthenticateUserAsync(Domain.Models.RequestModels.AuthModel user)
        {
            if (string.IsNullOrWhiteSpace(user.LoginName) || string.IsNullOrWhiteSpace(user.Password)) { return false; }

            Domain.Models.ResponseModels.AuthModel result = await _authService.AuthenticateUserAsync(user);

            // Setting access token
            Settings.SetAccessToken(result.AccessToken);

            return result.Success;
        }

        private void SetBaseProperties(Acs.Domain.Models.RequestModels.BaseModel baseModel)
        {
            baseModel.ApplicationId = Settings.GetApplicationId();
            baseModel.SerialNumber = Settings.GetSerialNumber();
            baseModel.Version = Settings.GetVersion();
            baseModel.AuthToken = Settings.GetAuthorizationToken();
            baseModel.AccessToken = Settings.GetAccessToken();
        }


        public async Task<Domain.Models.ResponseModels.PaitentAuthModel> GetPersonDetialsByAccountNoAsync(string patientNumber)
        {
            Domain.Models.RequestModels.PaitentAuthModel domainUser = new Domain.Models.RequestModels.PaitentAuthModel();

            SetBaseProperties(domainUser);
            domainUser.AccountNumber = patientNumber;

            // Call REST service
            Domain.Models.ResponseModels.PaitentAuthModel res = await _authService.GetPersonDetialsByAccountNoAsync(domainUser);

            return res;
        }

        public async Task<Domain.Models.ResponseModels.FormGroupGroupingsModel> GetFormGroupsAsync()
        {
            Domain.Models.RequestModels.FormGroupModel domainUser = new Domain.Models.RequestModels.FormGroupModel() {
                LocationID = Settings.GetLocationID(),
                FaciltyID = Settings.GetFacilityID()
            };
            SetBaseProperties(domainUser);

            // Call REST service
            Domain.Models.ResponseModels.FormGroupGroupingsModel res = await _authService.GetFormGroupsAsync(domainUser);

            return res;
        }

        //FormGroupsFormsModel
        public async Task<Domain.Models.ResponseModels.GroupOfFormTypesModel> GetGroupOfFormTypesAsync(int formGroupId)
        {
            Domain.Models.RequestModels.FormGroupModel domainUser = new Domain.Models.RequestModels.FormGroupModel();
            SetBaseProperties(domainUser);
            domainUser.FormGroupId = formGroupId;

            // Call REST service
            Domain.Models.ResponseModels.GroupOfFormTypesModel res = await _authService.GetGroupOfFormTypesAsync(domainUser);

            return res;
        }

        public async Task<Domain.Models.ResponseModels.AddFormModel> AddFormsAsync(string formKey)
        {
            Domain.Models.RequestModels.AddFormModel domainUser = new Domain.Models.RequestModels.AddFormModel();
            SetBaseProperties(domainUser);
            domainUser.FormKey = formKey;
            domainUser.VisitId = Settings.GetVisitId();

            // Call REST service
            Domain.Models.ResponseModels.AddFormModel res = await _authService.AddFormsAsync(domainUser);

            return res;
        }

        public async Task<Domain.Models.ResponseModels.ProcessPatientFormModel> ProcessFormsAsync()
        {
            Domain.Models.RequestModels.ProcessPatientFormModel domainUser = new Domain.Models.RequestModels.ProcessPatientFormModel();
            SetBaseProperties(domainUser);
            domainUser.AccountNumber = Settings.GetAccountNumber();
            domainUser.VisitId = Settings.GetVisitId();
            domainUser.Forms = Settings.GetFormsList();

            // Call REST service
            Domain.Models.ResponseModels.ProcessPatientFormModel res = await _authService.ProcessFormsAsync(domainUser);

            return res;
        }




        #region Check Status and Register Device
        /// <summary>Registers the device asynchronously.</summary>
        /// <returns>A Task<Domain.Models.ResponseModels.RegisterDeviceModel>.</returns>
        public async Task<Domain.Models.ResponseModels.RegisterDeviceModel> RegisterDeviceAsync()
        {
            Acs.Domain.Models.RequestModels.RegisterDeviceModel deviceInfo = GetDeviceDetails();

            // Register device and set AuthToken
            Domain.Models.ResponseModels.RegisterDeviceModel registerModel = await _authService.RegisterDeviceAsync(deviceInfo);

            Settings.SetAuthorizationToken(registerModel.AuthToken);

            return registerModel;
        }

        /// <summary>Gets the device status indicating if the device can be used with the system asynchronous.</summary>
        public async Task<Domain.Models.ResponseModels.RegisterDeviceModel> GetDeviceStatusAsync()
        {
            Acs.Domain.Models.RequestModels.RegisterDeviceModel deviceInfo = GetDeviceDetails();

            Domain.Models.ResponseModels.RegisterDeviceModel sd = await _authService.GetDeviceStatusAsync(deviceInfo);

            Settings.SetAuthorizationToken(sd.AuthToken);

            return sd;
        }

        /// <summary>Allows the device's status to be changed changed asynchronous.</summary>
        public Task ChangeDeviceStatusAsync()
        {
            Acs.Domain.Models.RequestModels.RegisterDeviceModel deviceInfo = GetDeviceDetails();

            return _authService.ChangeDeviceStatusAsync(deviceInfo);
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
            deviceInfo.DeviceName = CrossDevice.Hardware.Manufacturer + " " + CrossDevice.Hardware.Model;
            deviceInfo.OS = CrossDevice.Hardware.OperatingSystem;
            deviceInfo.Version = CrossDevice.Hardware.OperatingSystemVersion;
            deviceInfo.Manufacturer = CrossDevice.Hardware.Manufacturer;
            deviceInfo.Model = CrossDevice.Hardware.Model;

            return deviceInfo;
        }


        #endregion check status and register Device


        public double searchFont { get; set; }


        public double searchFontSize
        {
            get
            {
                return searchFont;
            }
            set
            {
                searchFont = value;
                PropertyChanged(this, new PropertyChangedEventArgs("searchFontSize"));

            }
        }

        public bool MenuIconIsVisible = false;


        public bool MenuIconSetIsVisible
        {
            get
            {
                return MenuIconIsVisible;
            }
            set
            {
                if (MenuIconIsVisible != value)
                {
                    MenuIconIsVisible = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("MenuIconSetIsVisible"));
                }
            }
        }


        public double SpacingMenuItems = 25;


        public double SetSpacingMenuItems
        {
            get
            {
                return SpacingMenuItems;
            }
            set
            {
                if (SpacingMenuItems != value)
                {
                    SpacingMenuItems = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("SetSpacingMenuItems"));
                }
            }
        }


        public double ToolBarSize = 18;


        public double SetToolBarSize
        {
            get
            {
                return ToolBarSize;
            }
            set
            {
                
                ToolBarSize = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SetToolBarSize"));

            }
        }





        public event PropertyChangedEventHandler PropertyChanged = delegate { };

    }
}
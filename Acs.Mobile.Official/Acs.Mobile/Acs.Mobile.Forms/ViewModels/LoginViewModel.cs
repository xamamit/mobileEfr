
using Acs.Services;
using Acs.Services.AuthServices;
using Acs.Services.RegistrationServices;
using Acs.Services.Helpers;
using Acs.Mobile.ESig.ViewModels.Base;
using Acs.Domain.Models;
using System.Threading.Tasks;
using Plugin.DeviceInfo;
using Acs.Mobile.ESig.Controls;
using Xamarin.Forms;
using System;

namespace Acs.Mobile.ESig.ViewModels
{
    // TODO: Async - Ensure services are both Async and sync. Add Async methods

    // TODO:  VM should NOT have callbacks that are event handlers for controls on the view:
    // Instead, commands should be exposed from the VM to the View:
    // https://developer.xamarin.com/guides/xamarin-forms/xaml/xaml-basics/data_bindings_to_mvvm/

    // https://app.pluralsight.com/player?course=mobile-application-xamarin-forms-visual-studio-2017&author=jesse-liberty&name=mobile-application-xamarin-forms-visual-studio-2017-m4&clip=5&mode=live

    public class LoginViewModel : ViewModelBase, IViewModel
    {
        public LoginViewModel() { }

        // KEEP becasue hope is we can move our register work and get to use only this constructor
        //public LoginViewModel(IESigAuthService authService)
        //{
        //    _authService = authService;
        //}

        // TODO: Get this class to the point where the constructor does NOT need registerService.
        //  all the register work should happen in the RegisterViewModel, so we should be able to 
        // slowly move it out of this class.
        public LoginViewModel(IESigAuthService authService, IRegistrerDeviceService registerService)
        {
            // TODO, get user and pass in User type to the auth Service for authenticaiton.
            _authService = authService;
            _registerService = registerService;
        }

        private Acs.Services.AuthServices.IESigAuthService _authService;
        private Acs.Services.RegistrationServices.IRegistrerDeviceService _registerService;

        // TODO: Make true properties to bind to and implement PropertyChanged?
        public bool IsAuth { get; set; }


        public string UserName { get; set; }


        public String Password { get; set; }
        

        public string SelectedDomain { get; set; }


        public async Task<bool> AreCredentialsCorrect(Mobile.ESig.User user)
        {
            var isValid = ValidateUser(user);
            if (!isValid) { return false; }

            Domain.Models.RequestModels.AuthModel domainUser = ConvertUserToDomain(user);

            // TODO: Call the REST Service for authentication
            var res = await _authService.ValidateLoginAsync(domainUser);

            return res;
        }

        private bool ValidateUser(Mobile.ESig.User user)
        {			
            if (null == user) { return false; }

            // If we have barcode then no need to check un/pw
            if ( ! string.IsNullOrWhiteSpace(user.BadgeBarcode)) { return true; }

            if ( string.IsNullOrWhiteSpace(user.Username)) { return false; }
            if (string.IsNullOrWhiteSpace(user.Password)) { return false; }
            // if (string.IsNullOrWhiteSpace(user.SelectedDomain)) { return false; }

            return true;
        }

        private Domain.Models.RequestModels.AuthModel ConvertUserToDomain(Mobile.ESig.User user)
        {
            Domain.Models.RequestModels.AuthModel domainUser = new Domain.Models.RequestModels.AuthModel(
                user.Username,
                user.Password
                 );

            return domainUser;
        }

        // TODO: Move to register vm
        public async Task<Domain.Models.ResponseModels.RegisterModel> Register()
		{
            Settings.SetSerialNumber( CrossDevice.Hardware.DeviceId);
			Settings.SetDomainName("accessefm");
            Settings.SetApplicationId("2");
			Settings.SetVersion(CrossDevice.App.Version);

   //         string s= _userPreferencesService.GetString("IsSettingsSave");

			//DependencyService.Get<IUserPreferences>().SetString("SetSerialNumber", CrossDevice.Hardware.DeviceId);
            //DependencyService.Get<IUserPreferences>().SetString("SetDomainName", "accessefm");
            //DependencyService.Get<IUserPreferences>().SetString("SetApplicationId", "2");
            //DependencyService.Get<IUserPreferences>().SetString("SetVersion", CrossDevice.App.Version);

            Acs.Domain.Models.RequestModels.RegisterModel deviceInfo = new Acs.Domain.Models.RequestModels.RegisterModel();

            deviceInfo.SerialNumber = Settings.GetSerialNumber();
			deviceInfo.AuthToken = Settings.GetAuthorizationToken();
            deviceInfo.DeviceName = CrossDevice.Hardware.DeviceId;
			deviceInfo.OS = CrossDevice.Hardware.OperatingSystem;
			deviceInfo.Version = CrossDevice.Hardware.OperatingSystemVersion;
			deviceInfo.Manufacturer = CrossDevice.Hardware.Manufacturer;
			deviceInfo.Model = CrossDevice.Hardware.Model;

		    // Domain.Models.ResponseModels.RegisterModel sd = await _authService.RegisterDeviceAsync(deviceInfo);

            Domain.Models.ResponseModels.RegisterModel sd = await _registerService.RegisterDeviceAsync(deviceInfo);
        
            Settings.SetAuthorizationToken(sd.AuthToken);
				
            DependencyService.Get<IUserPreferences>().SetString("SetAuthorizationToken", sd.AuthToken);

            return sd;
		}

        // TODO: Move to register vm
		public async Task<Domain.Models.ResponseModels.RegisterModel> GetDeviceStatusAsync()
		{
			Settings.SetSerialNumber(CrossDevice.Hardware.DeviceId);
			Settings.SetDomainName("accessefm");
			Settings.SetApplicationId("2");
			Settings.SetVersion(CrossDevice.App.Version);

			//DependencyService.Get<IUserPreferences>().SetString("SetSerialNumber", CrossDevice.Hardware.DeviceId);
			//DependencyService.Get<IUserPreferences>().SetString("SetDomainName", "accessefm");
			//DependencyService.Get<IUserPreferences>().SetString("SetApplicationId", "2");
			//DependencyService.Get<IUserPreferences>().SetString("SetVersion", CrossDevice.App.Version);

			Acs.Domain.Models.RequestModels.RegisterModel deviceInfo = new Acs.Domain.Models.RequestModels.RegisterModel();

			deviceInfo.SerialNumber = Settings.GetSerialNumber();
			deviceInfo.AuthToken = Settings.GetAuthorizationToken();
			deviceInfo.DeviceName = CrossDevice.Hardware.DeviceId;
			deviceInfo.OS = CrossDevice.Hardware.OperatingSystem;
			deviceInfo.Version = CrossDevice.Hardware.OperatingSystemVersion;
			deviceInfo.Manufacturer = CrossDevice.Hardware.Manufacturer;
			deviceInfo.Model = CrossDevice.Hardware.Model;

            // Domain.Models.ResponseModels.RegisterModel sd = await _authService.VerifyStatus(deviceInfo);

		    Domain.Models.ResponseModels.RegisterModel sd = await _registerService.GetDeviceStatusAsync(deviceInfo);

            Settings.SetAuthorizationToken(sd.AuthToken);

            //DependencyService.Get<IUserPreferences>().SetString("SetAuthorizationToken", sd.AuthToken);

            return sd;
		}

        // TODO: Move to register vm
        public Task ChangeDeviceStatus()
		{
			Settings.SetSerialNumber(CrossDevice.Hardware.DeviceId);
			Settings.SetDomainName("accessefm");
			Settings.SetApplicationId("2");
			Settings.SetVersion(CrossDevice.App.Version);

			Acs.Domain.Models.RequestModels.RegisterModel deviceInfo = new Acs.Domain.Models.RequestModels.RegisterModel();

			deviceInfo.SerialNumber = Settings.GetSerialNumber();
			deviceInfo.AuthToken = Settings.GetAuthorizationToken();
			deviceInfo.DeviceName = CrossDevice.Hardware.DeviceId;
			deviceInfo.OS = CrossDevice.Hardware.OperatingSystem;
			deviceInfo.Version = CrossDevice.Hardware.OperatingSystemVersion;
			deviceInfo.Manufacturer = CrossDevice.Hardware.Manufacturer;
			deviceInfo.Model = CrossDevice.Hardware.Model;

            //await _authService.ChangeDeviceStatus(deviceInfo);

            return _registerService.ChangeDeviceStatusAsync(deviceInfo);

            //Settings.SetAuthorizationToken(sd.AuthToken);
        }

        public async System.Threading.Tasks.Task<Domain.Models.ResponseModels.PaitentModel> GetPatientData(string patientNumber)
		{
			Domain.Models.RequestModels.PaitentModel domainUser = new Domain.Models.RequestModels.PaitentModel();
			domainUser.ApplicationId = Settings.GetApplicationId();
            domainUser.SerialNumber = Settings.GetSerialNumber();
            domainUser.Version = Settings.GetVersion();
            domainUser.AuthToken = Settings.GetAuthorizationToken();
            domainUser.AccessToken = Settings.GetAccessToken();

            domainUser.AccountNumber = patientNumber;

			// TODO: Call the REST Service for authentication
			Domain.Models.ResponseModels.PaitentModel res = await _authService.SearchResultAsync(domainUser);

			return res;
		}

        public async System.Threading.Tasks.Task<Domain.Models.ResponseModels.FormsGroupModel> GetFormCategoriesAsync()
		{
            Domain.Models.RequestModels.FormsGroupModel domainUser = new Domain.Models.RequestModels.FormsGroupModel();
			domainUser.ApplicationId = Settings.GetApplicationId();
			domainUser.SerialNumber = Settings.GetSerialNumber();
			domainUser.Version = Settings.GetVersion();
			domainUser.AuthToken = Settings.GetAuthorizationToken();
			domainUser.AccessToken = Settings.GetAccessToken();

			// TODO: Call the REST Service for authentication
            Domain.Models.ResponseModels.FormsGroupModel res = await _authService.GetFormCategoriesAsync(domainUser);

			return res;
		}

        public async System.Threading.Tasks.Task<Domain.Models.ResponseModels.FormGroupFormModel> GetCategorysFormListAsync(int formGroupId)
		{
            Domain.Models.RequestModels.FormGroupFormModel domainUser = new Domain.Models.RequestModels.FormGroupFormModel();
			domainUser.ApplicationId = Settings.GetApplicationId();
			domainUser.SerialNumber = Settings.GetSerialNumber();
			domainUser.Version = Settings.GetVersion();
			domainUser.AuthToken = Settings.GetAuthorizationToken();
			domainUser.AccessToken = Settings.GetAccessToken();
            domainUser.FormGroupId = formGroupId;

			// TODO: Call the REST Service for authentication
            Domain.Models.ResponseModels.FormGroupFormModel res = await _authService.GetCategorysFormListAsync(domainUser);

			return res;
		}

        public async System.Threading.Tasks.Task<Domain.Models.ResponseModels.AddFormModel> AddFormsAsync(string formKey)
		{
            Domain.Models.RequestModels.AddFormModel domainUser = new Domain.Models.RequestModels.AddFormModel();
			domainUser.ApplicationId = Settings.GetApplicationId();
			domainUser.SerialNumber = Settings.GetSerialNumber();
			domainUser.Version = Settings.GetVersion();
			domainUser.AuthToken = Settings.GetAuthorizationToken();
			domainUser.AccessToken = Settings.GetAccessToken();
            domainUser.FormKey = formKey;
            domainUser.VisitID = Settings.GetVisitId();

			// TODO: Call the REST Service for authentication
            Domain.Models.ResponseModels.AddFormModel res = await _authService.AddFormsAsync(domainUser);

			return res;
		}

        public async System.Threading.Tasks.Task<Domain.Models.ResponseModels.ProcessPatientFormModel> ProcessFormsAsync()
		{
            Domain.Models.RequestModels.ProcessPatientFormModel domainUser = new Domain.Models.RequestModels.ProcessPatientFormModel();
			domainUser.ApplicationId = Settings.GetApplicationId();
			domainUser.SerialNumber = Settings.GetSerialNumber();
			domainUser.Version = Settings.GetVersion();
			domainUser.AuthToken = Settings.GetAuthorizationToken();
			domainUser.AccessToken = Settings.GetAccessToken();
            domainUser.AccountNumber = Settings.GetAccountNumber().ToString();
			domainUser.VisitID = Settings.GetVisitId();
            domainUser.Forms = Settings.GetFormsList();

			// TODO: Call the REST Service for authentication
            Domain.Models.ResponseModels.ProcessPatientFormModel res = await _authService.ProcessFormsAsync(domainUser);

			return res;
		}
    }
}
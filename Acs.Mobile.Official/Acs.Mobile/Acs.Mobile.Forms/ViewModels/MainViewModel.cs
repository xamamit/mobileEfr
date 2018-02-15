using System;

using Acs.Mobile.ESig.ViewModels.Base;
using Acs.Services.AuthServices;
namespace Acs.Mobile.ESig.ViewModels
{
    public class MainViewModel : ViewModelBase, IViewModel
    {
        public MainViewModel() { }

		private IESigAuthService _authService;


		public MainViewModel(IESigAuthService authService)
		{
			// TODO, get user and pass in User type to the auth Service for authenticaiton.
			_authService = authService;
		}


		public async System.Threading.Tasks.Task<Domain.Models.ResponseModels.PaitentModel> GetPatientData()
		{
            Domain.Models.RequestModels.PaitentModel domainUser = new Domain.Models.RequestModels.PaitentModel();
            //domainUser.ApplicationId = "2";
            //domainUser.SerialNumber = "24saaa";
            //domainUser.Version = "2";
            //domainUser.AuthToken = "913ac84c-a235-4cbf-b434-29a0df02b2b7";
            //domainUser.AccessToken = "54fcf0cb-1e1b-4e70-8f6a-c867e5ffd8df";

            //domainUser.AccountNumber = "90970059";

            // TODO: Call the REST Service for authentication
            Domain.Models.ResponseModels.PaitentModel res = await _authService.SearchResultAsync(domainUser);

			return res;
		}
    }
}

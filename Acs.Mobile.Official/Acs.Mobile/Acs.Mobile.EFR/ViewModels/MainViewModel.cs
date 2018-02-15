using System;

using Acs.Mobile.ESig.ViewModels.Base;
using Acs.Common.Services.AuthServices;
namespace Acs.Mobile.ESig.ViewModels
{
    public class MainViewModel : ViewModelBase, IViewModel
    {
        public MainViewModel()
        {
        }



		private IESigAuthService _authService;


	


		public MainViewModel(IESigAuthService authService)
		{
			_authService = authService;
		}

		public async System.Threading.Tasks.Task<Common.Domain.Models.Response.PaitentModel> GetPatientData()
		{


			Common.Domain.Models.Request.PaitentModel domainUser = new Common.Domain.Models.Request.PaitentModel();
			domainUser.ApplicationID = "2";
			domainUser.SerialNumber = "24saaa";
			domainUser.Version = "2";
			domainUser.AuthorizationToken = "913ac84c-a235-4cbf-b434-29a0df02b2b7";
			domainUser.AccessToken = "54fcf0cb-1e1b-4e70-8f6a-c867e5ffd8df";

			domainUser.AccountNumber = "90970059";

			// TODO: Call the REST Service for authentication
			Common.Domain.Models.Response.PaitentModel res = await _authService.SearchResult(domainUser);

			return res;


		}
    }
}

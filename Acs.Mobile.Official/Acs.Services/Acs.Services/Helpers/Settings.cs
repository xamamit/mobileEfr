using System;
using System.Collections.Generic;
using System.Text;
using Acs.Domain.Models;

namespace Acs.Services.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        #region Setting Constants

        private static readonly string _settingsDefault = string.Empty;

        private static readonly string _baseUrlDeafult = "https://passport-lab.accessefm.com/api/v2/mobile";

        private static string _applicationId = string.Empty;

        private static string _serialNumber = string.Empty;

        private static string _version =string.Empty;

        private static string _domainName = string.Empty;

        private static string _authorizationToken = string.Empty;

        private static string _accessToken = string.Empty;

        private static int _visitId = 0;

        private static int _locationId = 0;

        private static int _facilityId = 0;

        private static List<Domain.Models.ResponseModels.Form> _listForms { get; set; }

        private static string _accountNumber { get; set; }

		#endregion

        public static string GetAccountNumber()
		{
            return _accountNumber;
		}

		public static List<Domain.Models.ResponseModels.Form> GetFormsList()
		{
            return _listForms;
		}
       
		public static string GetBaseUrl()
		{
            return _baseUrlDeafult;
		}

		public static int GetVisitId()
		{
            return _visitId;
		}

		public static string GetApplicationId()
		{
            return _applicationId;
		}
		public static void SetApplicationId(string applicationId)
		{
            _applicationId = applicationId;
		}


		public static string GetSerialNumber()
		{
            return _serialNumber;
		}

		public static void SetSerialNumber(string serialNumber)
		{
            _serialNumber = "359106081471908";
		}

		public static string GetVersion()
		{
            return _version;
		}

		public static void SetVersion(string version)
		{
            _version = version;
		}

		public static string GetAuthorizationToken()
		{
            return _authorizationToken;
		}
		public static void SetAuthorizationToken(string authorizationToken)
		{
            _authorizationToken = authorizationToken;
		}

		public static string GetDomainName()
		{
            return _domainName;
		}
		public static void SetDomainName(string domainName)
		{
			_domainName = domainName;
		}

		public static string GetAccessToken()
		{
            return _accessToken;
		}
		public static void SetAccessToken(string accessToken)
		{
            _accessToken = accessToken;
		}

		public static void SetVisitId(int visitId)
		{
            _visitId = visitId;
		}

        public static void SetFormsList(List<Domain.Models.ResponseModels.Form> formsList)
		{ 
            _listForms = formsList;
		}


        public static void SetAccoiuntNumber(string accountNumber)
		{
            _accountNumber = accountNumber;
		}

        public static void SetLocationID(int id) {
            _locationId = id;
        }

        public static int GetLocationID() {
            return _locationId;
        }

        public static void SetFacilityID(int id) {
            _facilityId = id;
        }

        public static int GetFacilityID() {
            return _facilityId;
        }

    }

    public class Forms
	{
		public int BusinessProcessID { get; set; }
		public string SubmissionReferenceKey { get; set; }
        public string FormKey { get; set; }

	}
}
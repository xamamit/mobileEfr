
using System;

namespace Acs.Domain.Models.RequestModels
{
    /// <summary>
    /// Represents the common data shared between all (or most) all of the models used in Requests.
    /// </summary>
    public class BaseModel
    {
        public BaseModel() { }

		public BaseModel(string applicationId, 
            string serialNumber, 
            string version, 
            string domainName, 
            string autorizationToken, 
            string accessToken)
		{
			ApplicationId = applicationId;
			SerialNumber = serialNumber;
			Version = version;
			DomainName = domainName;
            AuthToken = autorizationToken;
            AccessToken = accessToken;
		}

		public string ApplicationId { get; set; }

		public string SerialNumber { get; set; }

		public string Version { get; set; }

		public string DomainName { get; set; }

        public string AuthToken { get; set; }

        public string AccessToken { get; set; }
    }
}
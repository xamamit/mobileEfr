
using System;

namespace Acs.Domain.Models.ResponseModels
{
    /// <summary>
    /// Provides the <c>response</c> information returned from the source <c>API</c> call.
    /// </summary>
    /// <seealso cref="Acs.Domain.Models.ResponseModels.BaseModel" />
    public class RegisterDeviceModel : BaseModel
    {
        /// <summary>Initializes a new instance of the <see cref="RegisterDeviceModel"/> class.</summary>
        public RegisterDeviceModel() { }


        /// <summary>Initializes a new instance of the <see cref="RegisterDeviceModel"/> class.</summary>
        /// <param name="authToken">The authentication token.</param>
        /// <param name="status">The status.</param>
        /// <param name="statusDescription">The status description.</param>
        public RegisterDeviceModel(string authToken, string status, string statusDescription)
        {
            AuthToken = authToken;
            Status = status;
            StatusDescription = statusDescription;
        }

        /// <summary>Gets or sets the authentication token.</summary>
        /// <value>The authentication token.</value>
        public string AuthToken { get; set; }

        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        public string Status { get; set; }

        /// <summary>Gets or sets the status description.</summary>
        /// <value>The status description.</value>
        public string StatusDescription { get; set; }
    }
}
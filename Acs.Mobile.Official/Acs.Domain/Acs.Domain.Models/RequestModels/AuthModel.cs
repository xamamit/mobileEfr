
using System;

namespace Acs.Domain.Models.RequestModels
{
    /// <summary>
    /// Model representing a user attempting to log into an <c>Acs</c> system.
    /// </summary>
    /// <seealso cref="Acs.Domain.Models.RequestModels.BaseModel" />
    public class AuthModel : BaseModel
    {
        /// <summary>Initializes a new instance of the <see cref="AuthModel"/> class.</summary>
        public AuthModel() { }

        /// <summary>Initializes a new instance of the <see cref="AuthModel"/> which is used to authenticate a user.</summary>
        /// <param name="userName">Name of the user attempting to login.</param>
        /// <param name="password">The user's password.</param>
        public AuthModel(string userName, string password)
        {
            LoginName = userName;
            Password = password;
        }

        /// <summary>Gets or sets the name of the login.</summary>
        /// <value>The name of the login.</value>
        public string LoginName { get; set; }


        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public string Password { get; set; }
    }
}
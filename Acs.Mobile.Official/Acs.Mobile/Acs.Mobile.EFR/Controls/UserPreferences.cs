using System;
namespace Acs.Mobile.EFR.Controls
{
    public class UserPreferences : IUserPreferences
    {
        /// <summary>Initializes a new instance of the <see cref="UserPreferences"/> class.</summary>
        public UserPreferences() { }
        
        public string GetString(string key)
        {
            return key;
        }

        public void SetString(string key, string value)
        {
            
        }
    }
}

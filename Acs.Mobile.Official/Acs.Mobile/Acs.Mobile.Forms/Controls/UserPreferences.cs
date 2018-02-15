using System;
namespace Acs.Mobile.ESig.Controls
{
    public class UserPreferences : IUserPreferences
    {
        public UserPreferences()
        {
        }

        public string GetString(string key)
        {
            return key;
        }

        public void SetString(string key, string value)
        {
            
        }
    }
}

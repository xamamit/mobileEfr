using System;
namespace Acs.Mobile.ESig.Controls
{
    public interface IUserPreferences
    {
		void SetString(string key, string value);
		string GetString(string key);
    }
}

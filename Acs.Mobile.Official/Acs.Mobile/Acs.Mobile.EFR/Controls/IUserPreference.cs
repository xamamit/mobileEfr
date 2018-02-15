
using System;

namespace Acs.Mobile.EFR.Controls
{
    /// <summary>Interface for user preferences when they become necessary.</summary>
    public interface IUserPreferences
    {
		void SetString(string key, string value);

		string GetString(string key);
    }
}

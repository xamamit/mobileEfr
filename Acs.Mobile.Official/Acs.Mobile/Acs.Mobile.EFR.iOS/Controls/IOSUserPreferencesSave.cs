using System;
using Acs.Mobile.EFR.Controls;
using Foundation;

namespace Acs.Mobile.EFR.iOS.Controls
{
    public class IOSUserPreferencesSave : IUserPreferences
    {
		public void SetString(string key, string value)
		{
			NSUserDefaults.StandardUserDefaults.SetString(key, value);
		}

		public string GetString(string key)
		{
            string val = NSUserDefaults.StandardUserDefaults.StringForKey(key);
            return val;
	
	    }
    }
}

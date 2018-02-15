using System;
using Acs.Services.AuthServices;
using Acs.Mobile.EFR.Controls;
using Acs.Mobile.EFR.Droid.Controls;
using Android.Content;
using Android.Preferences;


namespace Acs.Mobile.EFR.Droid.Controls
{
    public class AndroidUserPreferencesSave : IUserPreferences
    {
		public void SetString(string key, string value)
		{
			ISharedPreferences pref = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(Android.App.Application.Context);
			ISharedPreferencesEditor edit = pref.Edit();
            edit.PutString(key, value);

			edit.Apply();
		}

		public string GetString(string key)
		{
			ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(Android.App.Application.Context);
            string val = preferences.GetString(key, "");

            return val;
	
	    }
    }
}


namespace Acs.Mobile.EFR
{
    /// <summary>Initializes a new instance of the <c>AppSettingsModel</c>.</summary>
    public class AppSettingsModel
	{
	    //
	    // There isn't a reason to document single read-write properties in this case.
	    //
        public string PassportServerUrl { get; set; }

		public string DomainName { get; set; }
	}
}

/*
Acs.Mobile.EFR.ViewModels - SettingsViewModel
    public string PassportServerUrl { get; set; }
    public string DomainName { get; set; }

Acs.Mobile.EFR.Models - SettingsModel
    PassportServerUrl
    DomainName






*/

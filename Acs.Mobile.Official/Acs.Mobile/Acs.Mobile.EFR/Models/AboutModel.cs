using System;
namespace Acs.Mobile.EFR.Models
{
    /// <summary>Initializes a new instance of the <c>AboutModel</c>. The properties in this 
    /// model are the values displayed at the top of the About page from the menu.
    /// </summary>
    public class AboutModel
    {
        //
        // There isn't a reason to document single read-write properties in this case.
        // These are the property values that are displayed and bound to the 
        // About page (at the top of the page).
        //

        public string DeviceId { get; set; }
        public string DevicePlatform { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceVersion { get; set; }
        public string AppVersion { get; set; }
    }
}
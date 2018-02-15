using System;

namespace Acs.Mobile.ESig
{
    public class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string SelectedDomain { get; set; }

        public string BadgeBarcode { get; set; }

        public bool IsUsingBarcode
        {
            get { return String.IsNullOrWhiteSpace(BadgeBarcode); }
        }
    }
}
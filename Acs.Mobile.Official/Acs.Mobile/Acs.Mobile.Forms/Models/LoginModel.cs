using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acs.Mobile.ESig.Models
{
    public class LoginModel
    {
        public LoginModel()
        {

        }

        public LoginModel(string userName, string password, string selectedDomain)
        {
            // TODO: validate arguments

            UserName = userName;
            Password = password;
            SelectedDomain = selectedDomain;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string SelectedDomain { get; set; }
    }
}
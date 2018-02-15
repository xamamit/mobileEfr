using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Acs.Mobile.ESig.ViewModels.Base;
using Acs.Services.AuthServices;

namespace Acs.Mobile.ESig.ViewModels
{
    public class UserViewModel : IViewModel
    {
        public int UserId { get; set; }


        public string UserName { get; set; }

        public string Password { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string EmailAddress { get; set; }
    }
}
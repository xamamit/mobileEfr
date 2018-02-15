using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acs.Services.AuthServices;
using Acs.Mobile.ESig.ViewModels.Base;

namespace Acs.Mobile.ESig.ViewModels
{
    public class AuthViewModel : ViewModelBase, IViewModel
    {
        public AuthViewModel(IESigAuthService authService)
        {
            // TODO: set configuration error message.
            if ( null == authService )
            {
                IsAuth = false;
                return;
            }

            // authenticate user via service.
            //IsAuth = authService.AuthenticateUser();
        }

        public bool IsAuth { get; set; }
    }
}
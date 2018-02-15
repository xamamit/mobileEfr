using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Acs.Services.AuthServices;
using Acs.Mobile.EFR.ViewModels.Base;

namespace Acs.Mobile.EFR.ViewModels
{
    public enum AuthenticationStatus
    {
        Success = 0,
        InvalidUnOrPwd = 1,
        InvalidDeviceId = 2,
        InvalidAuthorizationToken = 3,
        ApplicationNotAllowed = 4
    }

    public class AuthViewModel : ViewModelBase, IViewModel
    {
        public AuthViewModel(Services.AuthServices.IESigAuthService authService) { }
    }
}
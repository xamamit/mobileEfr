using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acs.Services.AuthServices;
using Acs.Mobile.ESig.ViewModels.Base;

namespace Acs.Mobile.ESig.ViewModels
{
    public class BarcodeScanViewModel : ViewModelBase, IViewModel
    {
        private IESigAuthService _authService;

        public BarcodeScanViewModel() { }

        public string BarCode { get; set; }

        public BarcodeScanViewModel(IESigAuthService authService)
        {
            // TODO, get user and pass in User type to the auth Service for authenticaiton.
            _authService = authService;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acs.Services.AuthServices;
using Acs.Mobile.EFR.ViewModels.Base;

namespace Acs.Mobile.EFR.ViewModels
{
    public class BarcodeScanViewModel : ViewModelBase, IViewModel
    {
        private IESigAuthService _authService;

        public BarcodeScanViewModel() { }

        public string BarCode { get; set; }

        public BarcodeScanViewModel(IESigAuthService authService)
        {
            _authService = authService;
        }
    }
}
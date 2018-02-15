
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Acs.Mobile.EFR.Models;
using Acs.Mobile.EFR.ViewModels.Base;
using Acs.Services.RegistrationServices;

namespace Acs.Mobile.EFR.ViewModels
{
    public class SettingsViewModel : ViewModelBase, IViewModel
    {
        private Acs.Services.RegistrationServices.IRegistrerDeviceService _rdDeviceService;

        private SettingsModel _settingsModel;

        public SettingsViewModel() { }

        // <summary>Initializes a new instance of the <see cref="LoginViewModel"/> class.</summary>
        public SettingsViewModel(Acs.Services.RegistrationServices.IRegistrerDeviceService rdService)
        {
            _rdDeviceService = rdService;
        }

        public string PassportServerUrl { get; set; }

        public string DomainName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acs.Mobile.ESig.ViewModels.Base;

namespace Acs.Mobile.ESig.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public string PassportServerURL { get; set; }

        public string DomainName { get; set; }
    }
}
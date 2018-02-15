
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Acs.Domain.Models.RequestModels;
using Acs.Domain.Models.ResponseModels;

namespace Acs.Services.RegistrationServices
{
    public interface IRegistrerDeviceService
    {
        Task<Domain.Models.ResponseModels.RegisterDeviceModel> RegisterDeviceAsync( 
            Domain.Models.RequestModels.RegisterDeviceModel deviceInfo);

        Task<Domain.Models.ResponseModels.RegisterDeviceModel> GetDeviceStatusAsync( 
            Domain.Models.RequestModels.RegisterDeviceModel deviceInfo);

        Task<bool> ChangeDeviceStatusAsync(Domain.Models.RequestModels.RegisterDeviceModel deviceInfo);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acs.Domain.Models;

namespace Acs.Services.AuthServices
{
    public interface IESigAuthService
    {
        Task<Domain.Models.ResponseModels.AuthModel> AuthenticateUserAsync( 
            Domain.Models.RequestModels.AuthModel user);

        Task<Domain.Models.ResponseModels.PaitentAuthModel> GetPersonDetialsByAccountNoAsync( 
            Domain.Models.RequestModels.PaitentAuthModel accountNumber);

        Task<Domain.Models.ResponseModels.FormGroupGroupingsModel> GetFormGroupsAsync( 
            Domain.Models.RequestModels.FormGroupModel formGroupRequest);

        Task<Domain.Models.ResponseModels.GroupOfFormTypesModel> GetGroupOfFormTypesAsync( 
            Domain.Models.RequestModels.FormGroupModel formGroupRequest);
        
        Task<Domain.Models.ResponseModels.AddFormModel> AddFormsAsync( 
            Domain.Models.RequestModels.AddFormModel formGroupRequest);

        Task<Domain.Models.ResponseModels.ProcessPatientFormModel> ProcessFormsAsync( 
            Domain.Models.RequestModels.ProcessPatientFormModel processFormRequest);

        bool ValidateLogin(String barCode);


        Task<Domain.Models.ResponseModels.RegisterDeviceModel> RegisterDeviceAsync(
            Domain.Models.RequestModels.RegisterDeviceModel deviceInfo);

        Task<Domain.Models.ResponseModels.RegisterDeviceModel> GetDeviceStatusAsync(
            Domain.Models.RequestModels.RegisterDeviceModel deviceInfo);

        Task<bool> ChangeDeviceStatusAsync(Domain.Models.RequestModels.RegisterDeviceModel deviceInfo);

    }
}
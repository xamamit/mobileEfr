
using System;
using System.Threading.Tasks;

namespace Acs.Services.FormGroupsServices
{
    public interface IFormGroupsServices
    {
        Task<Domain.Models.ResponseModels.FormGroupGroupingsModel> FormGroupsResult( 
            Domain.Models.ResponseModels.FormGroupGroupingsModel formGroupRequest);

    }
}

using System.Collections.Generic;

namespace Acs.Domain.Models.ResponseModels
{
    public class ProcessPatientFormModel : BaseModel
    {
        public ProcessPatientFormModel() { }

        public List<string> URLs { get; set; }
    }
}
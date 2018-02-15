
namespace Acs.Domain.Models.ResponseModels
{
    public class PaitentAuthModel : BaseModel
    {
        public PaitentAuthModel() { }

        public Patient Patient { get; set; }
    }
}
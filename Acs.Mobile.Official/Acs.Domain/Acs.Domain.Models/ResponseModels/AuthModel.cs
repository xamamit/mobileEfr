
namespace Acs.Domain.Models.ResponseModels
{
    public class AuthModel : BaseModel
    {
        public AuthModel() { }

        public AuthModel(string status, string accesstoken)
        {
            Status = status;
            AccessToken = accesstoken;
        }

        public string Status { get; set; }

        public string AccessToken { get; set; }
    }
}
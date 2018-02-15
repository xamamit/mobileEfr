using System;
namespace Acs.Mobile.ESig.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
        }


		public BaseModel(bool success, string error)
		{
			// TODO: validate arguments

			Success = success;
			Error = error;
			
		}
		public bool Success { get; set; }
		public String Error { get; set; }
    }
}

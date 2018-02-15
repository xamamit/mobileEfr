
using System;

namespace Acs.Domain.Models.ResponseModels
{
    public class BaseModel
    {
        public BaseModel() { }

		public BaseModel(bool success, string error)
		{
			Success = success;
			Error = error;
		}

		public bool Success { get; set; }

		public String Error { get; set; }

        public String SerialNumber { get; set; }
    }
}
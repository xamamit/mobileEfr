using System.Collections.Generic;

namespace Acs.Domain.Models.ResponseModels
{
    public class PatientForm
    {
        public PatientForm() { }

		public List<string> URLs { get; set; }

		public bool Success { get; set; }

		public object Error { get; set; }
    }
}
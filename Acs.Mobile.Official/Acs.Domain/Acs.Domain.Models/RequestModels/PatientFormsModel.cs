using System;
using System.Collections.Generic;

namespace Acs.Domain.Models.RequestModels
{
    /// <summary>
    /// Represents the different forms belonging to the specific person.
    /// </summary>
    /// <seealso cref="Acs.Domain.Models.RequestModels.BaseModel" />
    public class PatientFormsModel : BaseModel
    {
        public PatientFormsModel() { }

        public List<PatientForm> ListForms { get; set; }

        public string AccountNumber { get; set; }

        public int VisitId { get; set; }        
    }



    public class PatientForm
    {
        public int BusinessProcessId { get; set; }
		public string SubmissionReferenceKey { get; set; }
		public string FormKey { get; set; }
    }
}
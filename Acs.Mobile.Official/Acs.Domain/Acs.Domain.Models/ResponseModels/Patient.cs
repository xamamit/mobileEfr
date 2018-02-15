
using System;
using System.Collections.Generic;

namespace Acs.Domain.Models.ResponseModels
{
    public class Patient 
    {
        public Patient() { }

        public int PersonId { get; set; }
        
        public int LocationID { get; set; }
        public int FacilityID { get; set; }

        public int OrganizationId { get; set; }

        public int PatientId { get; set; }

        public int VisitId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string MedicalRecordNumber { get; set; }

        public string AccountNumber { get; set; }

		public List<Form> Forms { get; set; }
    }
}
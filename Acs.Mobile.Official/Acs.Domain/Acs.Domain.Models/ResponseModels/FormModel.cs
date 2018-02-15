
using System;

namespace Acs.Domain.Models.ResponseModels
{
    public class FormModel
    {
        /// <summary>Initializes a new instance of the <see cref="FormModel"/> class.</summary>
        public FormModel() { }


        public int FormId { get; set; }


        public string Name { get; set; }


		public string Description { get; set; }
        

		public DateTime DateCreated { get; set; }


		public int Status { get; set; }


		public int BusinessProcessId { get; set; }


		public string SubmissionReferenceKey { get; set; }


		public string FormKey { get; set; }
    }
}
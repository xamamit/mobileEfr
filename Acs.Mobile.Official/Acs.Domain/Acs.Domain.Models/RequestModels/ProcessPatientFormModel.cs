
using System;
using System.Collections.Generic;

namespace Acs.Domain.Models.RequestModels
{
    public class ProcessPatientFormModel : BaseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessPatientFormModel"/> class.
        /// </summary>
        public ProcessPatientFormModel() { }

        /// <summary>Gets or sets the list of forms for a particular <c>person</c>.</summary>
        /// <value>The forms.</value>
        public List<Domain.Models.ResponseModels.Form> Forms { get; set; }

        /// <summary>Gets or sets the <c>person's</c> account number.</summary>
        /// <value>The account number.</value>
        public string AccountNumber { get; set; }

        /// <summary>Gets or sets the visit identifier for the particular visit, appointment, meeting or etc.</summary>
        /// <value>The visit identifier.</value>
        public int VisitId { get; set; }
    }
}

using System;

namespace Acs.Domain.Models.RequestModels
{
    /// <summary>
    /// Represents the model / data necessary to add a new form the person of interest.
    /// </summary>
    /// <seealso cref="Acs.Domain.Models.RequestModels.BaseModel" />
    public class AddFormModel : BaseModel
    {
        /// <summary>Initializes a new instance of the <see cref="AddFormModel"/> class.</summary>
        public AddFormModel() { }

        /// <summary>Gets or sets the visit identifier for the person.</summary>
        /// <value>The visit identifier.</value>
        public int VisitId { get; set; }

        /// <summary>Gets or sets the key identifying the form.</summary>
        /// <value>The form key.</value>
        public string FormKey { get; set; }
    }
}
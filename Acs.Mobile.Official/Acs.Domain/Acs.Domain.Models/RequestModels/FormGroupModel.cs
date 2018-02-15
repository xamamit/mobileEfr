using System;
namespace Acs.Domain.Models.RequestModels
{
    /// <summary>Represents a simple way to <c>group</c> forms.</summary>
    /// <seealso cref="Acs.Domain.Models.RequestModels.BaseModel" />
    public class FormGroupModel : BaseModel
    {
        /// <summary>Initializes a new instance of the <see cref="FormGroupModel"/> class.</summary>
        public FormGroupModel() { }

        /// <summary>Gets or sets the form's group identifier.</summary>
        /// <value>The form's group identifier.</value>
        public int FormGroupId { get; set; }

        /// <summary>
        /// The FacilityID for the current patient.
        /// </summary>
        public int FaciltyID { get; set; }

        /// <summary>
        /// The LocationID for the current patient.
        /// </summary>
        public int LocationID { get; set; }
    }
}

using System.Collections.Generic;

namespace Acs.Domain.Models.ResponseModels
{
    /// <summary>
    /// Represents the list of <c>forms</c> belonging to a specific <c>group</c>. 
    /// (a FormGroup's Forms).
    /// </summary>
    /// <seealso cref="Acs.Domain.Models.ResponseModels.BaseModel" />
    public class GroupOfFormTypesModel : BaseModel
    {
        /// <summary>Initializes a new instance of the <see cref="GroupOfFormTypesModel"/> class.</summary>
        public GroupOfFormTypesModel() { }

        public List<FormTypeModel> Forms { get; set; }

        /// <summary>Gets the number of unique form types in the specified form group.</summary>
        /// <value>The number of unique form types in the group. Will return 0 if the underlying <see cref="List{T}"/> 
        /// is null.</value>
        public int FormTypeCount
        {
            get
            {
                if (null == Forms) return 0;
                return Forms.Count;
            }
        }
    }


    /// <summary>Represents a unique type of form.</summary>
    /// <seealso cref="Acs.Domain.Models.ResponseModels.BaseModel" />
    public class FormTypeModel : BaseModel
    {
        /// <summary>Gets or sets the unique form identifier.</summary>
        /// <value>The form identifier.</value>
        public int FormId { get; set; }

        /// <summary>Gets or sets the name of the form type.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>Gets or sets the description of the form type.</summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        public string FormKey { get; set; }
    }
}
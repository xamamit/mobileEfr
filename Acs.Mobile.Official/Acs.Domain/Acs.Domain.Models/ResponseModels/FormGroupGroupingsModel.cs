using System.Collections.Generic;

namespace Acs.Domain.Models.ResponseModels
{
    /// <summary>
    /// Represents the <see cref="List{T}"/> of different form <c>groups</c> (or categories of forms).
    /// </summary>
    /// <seealso cref="Acs.Domain.Models.ResponseModels.BaseModel" />
    public class FormGroupGroupingsModel : BaseModel
    {
        /// <summary>Gets or sets a <see cref="List{T}"/> of form groups.</summary>
        /// <value>The list of groups.</value>
        public List<FormGroupModel> FormGroups { get; set; }

        /// <summary>Gets the number of form groups in the list.</summary>
        /// <value>The form group count. Will return 0 if the underlying <see cref="List{T}"/> 
        /// is null.</value>
        public int FormGroupCount
        {
            get
            {
                if (null == FormGroups) return 0;
                return FormGroups.Count;
            }
        }
    }


    /// <summary>Represents a single group of <c>forms</c>.</summary>
    /// <seealso cref="Acs.Domain.Models.ResponseModels.BaseModel" />
    public class FormGroupModel : BaseModel
    {
        /// <summary>Gets or sets the form group identifier.</summary>
        /// <value>The form group identifier.</value>
        public int FormGroupId { get; set; }

        /// <summary>Gets or sets the name of the group.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>Gets or sets the description of th group.</summary>
        /// <value>The description.</value>
        public string Description { get; set; }
    }
}
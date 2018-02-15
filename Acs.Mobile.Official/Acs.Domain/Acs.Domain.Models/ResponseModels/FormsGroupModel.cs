using System.Collections.Generic;

namespace Acs.Domain.Models.ResponseModels
{
    public class FormsGroupModel : BaseModel
    {
        public FormsGroupModel() { }

        public List<FormGroup> FormGroups { get; set; }

        public int FormGroupCount
        {
            get
            {
                if (null == FormGroups) return 0;
                return FormGroups.Count;
            }
        }
    }


    public class FormGroup
    {
        public int FormGroupId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
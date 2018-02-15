
using System.Collections.Generic;

namespace Acs.Domain.Models.ResponseModels
{
    public class FormGroupFormModel :BaseModel
    {
        public FormGroupFormModel() { }

        public List<Forms> Forms { get; set; }
    }

	public class Forms
	{
		public int FormId { get; set; }


		public string Name { get; set; }


		public string Description { get; set; }


        public string FormKey { get; set; }
	}
}
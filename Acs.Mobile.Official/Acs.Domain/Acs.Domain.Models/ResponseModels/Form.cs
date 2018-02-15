using System;
using System.ComponentModel;

namespace Acs.Domain.Models.ResponseModels
{
    public class Form : INotifyPropertyChanged
    {
        /// <summary>Initializes a new instance of the <see cref="Form"/> class.</summary>
        public Form() { }


		public string Name { get; set; }


		public string Description { get; set; }
        

		public DateTime DateCreated { get; set; }


		public int Status { get; set; }


		public int BusinessProcessId { get; set; }


		public string SubmissionReferenceKey { get; set; }


		public string FormKey { get; set; }

        public string Show = "checkbox.png";

        public string UnShow
        {
            get
            {
                return Show;
            }
            set
            {
                if (Show != value)
                {
                    Show = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("UnShow"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}

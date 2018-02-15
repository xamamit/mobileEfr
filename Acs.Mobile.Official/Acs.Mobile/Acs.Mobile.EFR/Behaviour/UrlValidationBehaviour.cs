
using System;
using Xamarin.Forms;

namespace Acs.Mobile.EFR
{
    public class UrlValidationBehaviour : Xamarin.Forms.Behavior<Entry>
    {
        public UrlValidationBehaviour() { }

		protected override void OnAttachedTo(Entry entry)
		{
			entry.TextChanged += OnEntryTextChanged;
			base.OnAttachedTo(entry);
		}

		protected override void OnDetachingFrom(Entry entry)
		{
			entry.TextChanged -= OnEntryTextChanged;
			base.OnDetachingFrom(entry);
		}

		void OnEntryTextChanged(object sender, TextChangedEventArgs args)
		{			
			Uri uriResult;
            bool result = Uri.TryCreate(args.NewTextValue, UriKind.Absolute, out uriResult);
			
            ((Entry)sender).TextColor = result ? Color.Default : Color.Red;
		}
    }
}

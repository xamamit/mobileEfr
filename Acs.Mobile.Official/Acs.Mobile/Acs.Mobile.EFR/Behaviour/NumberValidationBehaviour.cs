﻿using System;
using Xamarin.Forms;

namespace Acs.Mobile.EFR.Behaviours
{
	public class NumericValidationBehavior : Xamarin.Forms.Behavior<Entry>
    {
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
			double result;
			bool isValid = double.TryParse(args.NewTextValue, out result);
			((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
		}
	}
}

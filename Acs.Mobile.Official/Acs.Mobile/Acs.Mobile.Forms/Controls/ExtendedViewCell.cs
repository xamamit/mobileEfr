using System;
using Xamarin.Forms;

namespace Acs.Mobile.ESig.Controls
{
	public class ExtendedViewCell : ViewCell
	{
		public static readonly BindableProperty SelectedBackgroundColorProperty =
			BindableProperty.Create("SelectedBackgroundColor",
									typeof(Color),
									typeof(ExtendedViewCell),
									Color.Default);

		public Color SelectedBackgroundColor
		{
			get { return (Color)GetValue(SelectedBackgroundColorProperty); }
			set { SetValue(SelectedBackgroundColorProperty, value); }
		}
	}
}

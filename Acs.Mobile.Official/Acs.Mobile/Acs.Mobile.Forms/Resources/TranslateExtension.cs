using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Acs.Mobile.ESig.Resources
{
   
		[ContentProperty("Text")]
		public class TranslateExtension : IMarkupExtension
		{
			public string Text { get; set; }

			public object ProvideValue(IServiceProvider serviceProvider)
			{
				if (Text == null)
					return null;

            return AppResources.ResourceManager.GetString(Text, System.Globalization.CultureInfo.CurrentCulture);
			}
		}

}

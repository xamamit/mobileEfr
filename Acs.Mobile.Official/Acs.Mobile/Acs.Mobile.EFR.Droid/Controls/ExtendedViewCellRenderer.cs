using System;
using System.ComponentModel;
using Acs.Mobile.EFR.Controls;
using Acs.Mobile.EFR.Droid.Controls;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedViewCell), typeof(ExtendedViewCellRenderer))]
namespace Acs.Mobile.EFR.Droid.Controls
{
    public class ExtendedViewCellRenderer : ViewCellRenderer
    {
		private Android.Views.View _cellCore;
		private Drawable _unselectedBackground;
		private bool _selected;       

        protected override Android.Views.View GetCellCore(Cell item,
														  Android.Views.View convertView,
														  ViewGroup parent,
														  Context context)
		{

			var nativeCell = (ExtendedViewCell)item;
			_cellCore = base.GetCellCore(item, convertView, parent, context);

         

            nativeCell.SelectedBackgroundColor = Color.Black;
          
			return _cellCore;
		}

		protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			base.OnCellPropertyChanged(sender, args);

			if (args.PropertyName == "IsSelected")
			{
				_selected = !_selected;

				if (_selected)
				{
					var extendedViewCell = sender as ExtendedViewCell;
					_cellCore.SetBackgroundColor(extendedViewCell.SelectedBackgroundColor.ToAndroid());
				}
				else
				{
					_cellCore.SetBackground(_unselectedBackground);
				}
			}
		}

		void OnNativeCellPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var nativeCell = (ExtendedViewCell)sender;
			if (e.PropertyName == "IsSelected")
			{
				_selected = !_selected;

				if (_selected)
				{
					var extendedViewCell = sender as ExtendedViewCell;
					_cellCore.SetBackgroundColor(extendedViewCell.SelectedBackgroundColor.ToAndroid());
				}
				else
				{
					_cellCore.SetBackground(_unselectedBackground);
				}
			}
		}
    }
}

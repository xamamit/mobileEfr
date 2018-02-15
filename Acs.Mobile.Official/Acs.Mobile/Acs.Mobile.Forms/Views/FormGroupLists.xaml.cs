using System;
using System.Collections.Generic;
using Acs.Services.Helpers;
using Acs.Mobile.ESig.ViewModels;
using Acs.Mobile.ESig.Views.Base;
using Xamarin.Forms;

namespace Acs.Mobile.ESig.Views
{
    public partial class FormGroupLists : ViewBase<LoginViewModel>
    {
		Domain.Models.ResponseModels.FormsGroupModel record;
        public FormGroupLists()
        {
            InitializeComponent();
            this.Title = "Select Form Type";

			this.BackgroundColor = Color.FromHex(AppStyle.pageBackgroundColorforIndicator);
			this.Opacity = 0.5;


			listForms.ItemSelected += (object sender, SelectedItemChangedEventArgs args) =>
			{
				var item = (Domain.Models.ResponseModels.FormGroups)args.SelectedItem;


				Navigation.PushAsync(new FormGroupForms(item.FormGroupId));

			};
        }

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			try
			{


				//record = await ViewModel.GetFormGroupsAsync();
			    record = await ViewModel.GetFormCategoriesAsync();
			}
			catch
			{
				await DisplayAlert("Sorry!", "Something went wrong", "OK");
			}

			if (record != null)
			{
				ac.IsVisible = false;
				stkFinal.IsVisible = true;
				listForms.ItemsSource = record.FormGroups;
				this.BackgroundColor = Color.White;
				this.Opacity = 1;
			}




		}
    }
}

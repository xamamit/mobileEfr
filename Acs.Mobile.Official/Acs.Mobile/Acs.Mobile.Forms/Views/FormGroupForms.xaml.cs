using System;
using System.Collections.Generic;
using Acs.Services.Helpers;
using Acs.Mobile.ESig.ViewModels;
using Acs.Mobile.ESig.Views.Base;
using Xamarin.Forms;

namespace Acs.Mobile.ESig.Views
{
    public partial class FormGroupForms : ViewBase<LoginViewModel>
    {
		int formGroupIdpvt;

		Domain.Models.ResponseModels.FormGroupFormModel record;
		Domain.Models.ResponseModels.AddFormModel recordForAddForms;

        public FormGroupForms(int formGroupId)
        {
            InitializeComponent();
            this.Title = "Add Form";

            this.BackgroundColor = Color.FromHex(AppStyle.pageBackgroundColorforIndicator);
			this.Opacity = 0.5;

			listForms.ItemSelected += async (object sender, SelectedItemChangedEventArgs args) =>
			{
				var item = (Domain.Models.ResponseModels.Forms)args.SelectedItem;

				var answer = await DisplayAlert("Add Form", "Would you like to add this form to the Patient", "Yes", "No");

				if (answer)
				{
					recordForAddForms = await ViewModel.AddFormsAsync(item.FormKey);
					if (recordForAddForms != null)
					{
						if (recordForAddForms.Success)
						{
							await DisplayAlert("Success", "Form added successfully", "OK");

                            App.IsFormsAdded = true;
                            Application.Current.MainPage = new NavigationPage(new MainViewsMasters());
							await Navigation.PopAsync().ConfigureAwait(false);
                        }
						else
						{
							await DisplayAlert("Sorry!", "Something went wrong", "OK");
						}
					}
				}
				else
				{
				}
			};
        }

		protected override async void OnAppearing()
		{
			base.OnAppearing();
		
		    record = await ViewModel.GetCategorysFormListAsync(formGroupIdpvt);

			if (record != null)
			{
				ac.IsVisible = false;
				stkFinal.IsVisible = true;
				listForms.ItemsSource = record.Forms;

				this.BackgroundColor = Color.White;
				this.Opacity = 1;
			}
		}
	}
}
using System;
using System.Diagnostics;
using Acs.Mobile.EFR.Helpers;
using Acs.Mobile.EFR.ViewModels;
using Acs.Mobile.EFR.Views.Base;
using Xamarin.Forms;

namespace Acs.Mobile.EFR.Views
{
    public partial class FormGroupLists : ViewBase<LoginViewModel>
    {
        private Domain.Models.ResponseModels.FormGroupGroupingsModel _record;

        public FormGroupLists()
        {
            InitializeComponent();
            this.Title = Constants.Title_FormGroupList;

            this.BackgroundColor = Color.FromHex(AppStyle.PageBackgroundColorforIndicator);
            this.Opacity = 0.5;
            ac.Color = Color.FromHex(AppStyle.ActivityIndicatorColor);


            listForms.ItemSelected += (object sender, SelectedItemChangedEventArgs args) =>
            {
                var item = (Domain.Models.ResponseModels.FormGroupModel)args.SelectedItem;
                if (item != null)
                {
                    FormGroupForms._formGroupIdpvt = item.FormGroupId;
                    Navigation.PushAsync(new FormGroupForms());
                }
                else
                {
                    DisplayAlert(Constants.Title_FormGroupList, Constants.ErrorMsgSorry, "OK");
                }
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                _record = await ViewModel.GetFormGroupsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in FormGroupList.xaml.cs -> OnAppearing(): Message: {ex.Message}");

                await DisplayAlert(Constants.ErrorMsgTitleOops, Constants.ErrorMsgSorry, "OK");
            }

            if (_record != null)
            {
                ac.IsVisible = false;
                stkFinal.IsVisible = true;

                listForms.ItemsSource = _record.FormGroups;
                this.BackgroundColor = Color.White;
                this.Opacity = 1;
            }
        }
    }
}
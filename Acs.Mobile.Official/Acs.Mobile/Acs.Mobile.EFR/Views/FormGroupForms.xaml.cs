using System.Diagnostics;
using Acs.Mobile.EFR.ViewModels;
using Acs.Mobile.EFR.Views.Base;
using Acs.Mobile.EFR.Helpers;
using Xamarin.Forms;
using Plugin.DeviceInfo;

namespace Acs.Mobile.EFR.Views
{
    public partial class FormGroupForms : ViewBase<LoginViewModel>
    {
        public static int _formGroupIdpvt;
        private Domain.Models.ResponseModels.GroupOfFormTypesModel _records;
        private Domain.Models.ResponseModels.AddFormModel _recordForAddForms;

        public FormGroupForms()
        {
            InitializeComponent();
            this.Title = Constants.Title_FormGroupForms;
            
            this.BackgroundColor = Color.FromHex(AppStyle.PageBackgroundColorforIndicator);
            this.Opacity = 0.5;
            ac.Color =Color.FromHex(AppStyle.ActivityIndicatorColor);

            listForms.ItemSelected += async (object sender, SelectedItemChangedEventArgs args) =>
            {
                var msg = " is null: FormGroupForms.xaml.cs -> ctr -> listForms.ItemSelected +=....";

                if (null == sender)
                {
                    Debug.WriteLine($"'sender' argument {msg}");
                    return;
                }
                if (null == args)
                {
                    Debug.WriteLine($"'args' argument {msg}");
                    return;
                }
                
                var item = (Domain.Models.ResponseModels.FormTypeModel)args.SelectedItem;
                var answer = await DisplayAlert("Add Form", "Would you like to add this form to their list?", "Yes", "No");

                if (answer)
                {
                    _recordForAddForms = await ViewModel.AddFormsAsync(item.FormKey);
                    if (_recordForAddForms != null && _recordForAddForms.Success)
                    {
                        //if (_recordForAddForms.Success)
                        //{
                            await DisplayAlert("Add Form", "Form added successfully", "OK");

                            App.IsFormsAdded = true;
                       
                        if(CrossDevice.Hardware.OperatingSystem=="WINDOWS")
                        {
                            Application.Current.MainPage = new NavigationPage(new MainViewsMasters());
                        }
                        else
                        {
                            await Navigation.PushAsync(new MainViewsMasters());
                        }
                       
                           
                        //await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert(Constants.ErrorMsgTitleOops, Constants.ErrorMsgSorry, "OK");
                    }
                    //}
                }
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            _records = await ViewModel.GetGroupOfFormTypesAsync(_formGroupIdpvt);

            if (_records != null)
            {
                ac.IsVisible = false;
                stkFinal.IsVisible = true;
                listForms.ItemsSource = _records.Forms;

                this.BackgroundColor = Color.White;
                this.Opacity = 1;
            }
        }
    }
}
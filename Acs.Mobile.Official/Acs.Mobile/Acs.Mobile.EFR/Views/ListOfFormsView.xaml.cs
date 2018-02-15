using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using Acs.Services.Helpers;
using Acs.Mobile.EFR.ViewModels;
using Acs.Mobile.EFR.Views.Base;
using Xamarin.Forms;
using Acs.Mobile.EFR.Helpers;

namespace Acs.Mobile.EFR.Views
{
    public partial class ListOfFormsView : ViewBase<LoginViewModel>
    {
        // Yes, I don't like the name of this view either, but changing the words around always 
        // ends up with the suffix being ListView which is confusing to make devs later on 
        // think this 'view' pertains to a control of type 'ListView' when, in fact, it does not.
        //

        public static List<UrlPvt> ListUrlPvt;
        public static List<UrlPvt> ListUrlPvtToBeProcessed;

        private Domain.Models.ResponseModels.ProcessPatientFormModel _processFormRecordsPvt;
        private Domain.Models.ResponseModels.ProcessPatientFormModel _processFormRecords;

        public ListOfFormsView()
        {
            this.Title = Constants.Title_ListOfFormsSelectionPage;

            ListUrlPvtToBeProcessed = null;
            ListUrlPvtToBeProcessed = new List<UrlPvt>();

            InitializeComponent();
            ac.Color = Color.FromHex(AppStyle.ActivityIndicatorColor);
            btnProcessForms.BackgroundColor = Color.FromHex(AppStyle.ButtonBackGroundColor);


            listForms.ItemSelected += (object sender, SelectedItemChangedEventArgs args) =>
            {
                #region Previous Code

                if (((ListView)sender).SelectedItem == null) return;

                var item = ((ListView)sender).SelectedItem as UrlPvt;

                if (null == item) return;

                if (item.Show == "checkbox_checked")
                {
                    ListUrlPvtToBeProcessed.Remove(item);
                    item.UnShow = "checkbox";
                }
                else
                {
                    item.UnShow = "checkbox_checked";
                    ListUrlPvtToBeProcessed.Add(item);
                }

                ((ListView)sender).SelectedItem = null;

                #endregion
            };

            TapGestureRecognizer tapCheckClickTap = new TapGestureRecognizer();
            tapCheckClickTap.Tapped += (snd, args) =>
            {
                var src = selectAllForms.Source as FileImageSource;

                if (null == src)
                {
                    Debug.WriteLine("ListToProcessForms.xaml.cs in constructor: src is null: " +
                        "var src = selectAllForms.Source as FileImageSource;");
                }

                if (src.File.ToString() == "checkbox")
                {
                    selectAllForms.Source = "checkbox_checked";

                    if (ListUrlPvtToBeProcessed != null)
                    {
                        ListUrlPvtToBeProcessed.Clear();
                    }

                    foreach (var item in ListUrlPvt)
                    {
                        item.UnShow = "checkbox_checked";
                        ListUrlPvtToBeProcessed.Add(item);
                    }
                }
                else
                {
                    selectAllForms.Source = "checkbox";
                    foreach (var item in ListUrlPvt)
                    {
                        item.UnShow = "checkbox";
                    }

                    if (ListUrlPvtToBeProcessed != null)
                    {
                        ListUrlPvtToBeProcessed.Clear();
                    }
                }
            };

            selectAllForms.GestureRecognizers.Add(tapCheckClickTap);

            btnProcessForms.Clicked += (snd, args) =>
            {
                if (ListUrlPvtToBeProcessed != null)
                {
                    if (ListUrlPvtToBeProcessed.Count > 0)
                    {
                        Forms.item = _processFormRecords.URLs[0];
                        Navigation.PushAsync(new Forms());
                    }
                    else
                    {
                        DisplayAlert("Process Forms", "Please select any form to Continue", "OK");
                    }
                }
                else
                {
                    DisplayAlert("Process Forms", "Please select any form to Continue", "OK");
                }
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            #region get forms to be processed

            try
            {
                _processFormRecords = await ViewModel.ProcessFormsAsync();
            }
            catch
            {
                await DisplayAlert(Constants.ErrorMsgTitleOops, Constants.ErrorMsgSorry, "OK");
            }

            try
            {

                if (_processFormRecords != null)
                {
                    if (_processFormRecords.Success)
                    {
                        if (_processFormRecords.URLs.Count > 0)
                        {
                            //fill list View with data
                            Domain.Models.RequestModels.ProcessPatientFormModel domainUser =
                                new Domain.Models.RequestModels.ProcessPatientFormModel();

                            domainUser.Forms = Settings.GetFormsList();

                            _processFormRecordsPvt = _processFormRecords;
                            ListUrlPvt = new List<UrlPvt>();

                            int ii = 0;

                            foreach (var url in _processFormRecordsPvt.URLs)
                            {
                                UrlPvt pvtu = new UrlPvt();
                                pvtu.UrlName = url;
                                pvtu.FormName = domainUser.Forms[ii].Name;
                                ListUrlPvt.Add(pvtu);

                                ii++;
                            }

                            listForms.ItemsSource = ListUrlPvt;

                            //end
                        }
                        else
                        {
                            await DisplayAlert("Form Processing", "There are no forms to process for the given identifier", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Form Processing", "There are no forms to process for the given identifier", "OK");
                    }
                }

                #endregion

                listForms.IsVisible = true;
                ac.IsVisible = false;
                stkFinal.IsVisible = true;

            }
            catch(Exception exs)
            {
                Debug.Write("Process Forms Issue :"+exs);
            }
        }
    }

    public class UrlPvt : INotifyPropertyChanged
    {
        public string UrlName { get; set; }

        public string FormName { get; set; }

        public string Show = "checkbox";

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
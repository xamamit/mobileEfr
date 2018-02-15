using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Acs.Services.Helpers;
using Acs.Mobile.ESig.ViewModels;
using Acs.Mobile.ESig.Views.Base;
using Xamarin.Forms;

namespace Acs.Mobile.ESig.Views
{
    public partial class ListToProcessForms : ViewBase<LoginViewModel>
    {
        private Domain.Models.ResponseModels.ProcessPatientFormModel _processFormRecordsPvt;
        private Domain.Models.ResponseModels.ProcessPatientFormModel _processFormRecords;

        private const string MessageNoFormsFound = "No forms found for the given identifier";
        private const string MessageSelectFormToProceed = "Select any form to Continue";

        public static List<UrlPvt> listUrlPvt;
        public static List<UrlPvt> listUrlPvtToBeProcessed;

        public ListToProcessForms()
        {
            this.Title = "List Of forms to be Processed";
            listUrlPvtToBeProcessed = null;
            listUrlPvtToBeProcessed = new List<UrlPvt>();

            InitializeComponent();

            listForms.ItemSelected += (object sender, SelectedItemChangedEventArgs args) =>
            {
                #region Previous Code

                if (((ListView)sender).SelectedItem == null)
                {
                    return;
                }

                var item = ((ListView)sender).SelectedItem as UrlPvt;

                if (item.ShowChkBox == "checkbox_checked")
                {
                    listUrlPvtToBeProcessed.Remove(item);

                    item.UnShow = "checkbox";
                }
                else
                {
                    item.UnShow = "checkbox_checked";
                    listUrlPvtToBeProcessed.Add(item);
                }

                ((ListView)sender).SelectedItem = null;

                #endregion Previous Code

            };

            // Tab gesture logic / rection
            TapGestureRecognizer tapCheckClickTap = new TapGestureRecognizer();

            tapCheckClickTap.Tapped += (snd, args) =>
            {
                var src = selectAllForms.Source as FileImageSource;


                if (src.File.ToString() == "checkbox")
                {
                    selectAllForms.Source = "checkbox_checked";

                    if (listUrlPvtToBeProcessed != null)
                    {
                        listUrlPvtToBeProcessed.Clear();
                    }

                    foreach (var item in listUrlPvt)
                    {
                        item.UnShow = "checkbox_checked";
                        listUrlPvtToBeProcessed.Add(item);
                    }

                    //listUrlPvtToBeProcessed = listUrlPvt;
                }
                else
                {
                    selectAllForms.Source = "checkbox";
                    foreach (var item in listUrlPvt)
                    {
                        item.UnShow = "checkbox";
                    }

                    if (listUrlPvtToBeProcessed != null)
                    {
                        listUrlPvtToBeProcessed.Clear();
                    }
                }
            };

            selectAllForms.GestureRecognizers.Add(tapCheckClickTap);

            btnProcessForms.Clicked += (snd, args) =>
            {
                if (listUrlPvtToBeProcessed != null)
                {
                    if (listUrlPvtToBeProcessed.Count > 0)
                    {
                        Navigation.PushAsync(new Forms(listUrlPvtToBeProcessed[0], 0));
                    }
                    else
                    {
                        DisplayAlert("Process eForms", MessageSelectFormToProceed, "OK");
                    }
                }
                else
                {
                    DisplayAlert("Process eForms", MessageSelectFormToProceed, "OK");
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
                await DisplayAlert("Sorry!", "Something went wrong", "OK");
            }

            if (_processFormRecords != null)
            {
                if (_processFormRecords.Success)
                {
                    if (_processFormRecords.URLs.Count > 0)
                    {
                        Domain.Models.RequestModels.ProcessPatientFormModel domainUser =
                            new Domain.Models.RequestModels.ProcessPatientFormModel();
                        domainUser.Forms = Settings.GetFormsList();

                        _processFormRecordsPvt = _processFormRecords;

                        listUrlPvt = new List<UrlPvt>();

                        int x = 0;

                        foreach (var url in _processFormRecordsPvt.URLs)
                        {
                            UrlPvt pvtu = new UrlPvt();
                            pvtu.UrlName = url;
                            pvtu.FormName = domainUser.Forms[x].Name;
                            listUrlPvt.Add(pvtu);
                            x = x + 1;
                        }

                        listForms.ItemsSource = listUrlPvt;
                        //end
                    }
                    else
                    {
                        await DisplayAlert("eForms", MessageNoFormsFound, "OK");
                    }
                }
                else
                {
                    await DisplayAlert("eForms", MessageNoFormsFound, "OK");
                }
            }

            #endregion

            listForms.IsVisible = true;
            ac.IsVisible = false;
            stkFinal.IsVisible = true;
        }
    }

    public class UrlPvt : INotifyPropertyChanged
    {
        public string UrlName { get; set; }

        public string FormName { get; set; }

        public string ShowChkBox = "checkbox";

        public string UnShow
        {
            get
            {
                return ShowChkBox;
            }
            set
            {
                if (ShowChkBox != value)
                {
                    ShowChkBox = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("unshow"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Acs.Services.Helpers;
using Acs.Mobile.EFR.ViewModels;
using Acs.Mobile.EFR.Views.Base;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using Plugin.DeviceInfo;
using Acs.Mobile.EFR.Helpers;
using System.ComponentModel;

namespace Acs.Mobile.EFR.Views {
    public partial class MainView : ViewBase<LoginViewModel> {
        
        public static bool IsSearched = false;
        public static bool IsFormsSelected = false;
        public bool IsSearchedNotNull = false;
        public static Domain.Models.ResponseModels.PaitentAuthModel Record;
        public static Domain.Models.ResponseModels.ProcessPatientFormModel ProcessFormRecords;

        private static string _patientNumber = String.Empty;
        private double _toolBarTextSize = 10;
        private static List<Domain.Models.ResponseModels.Form> _listOfFormsToBeProcessed;
        private static List<string> _urls;

        public MainView() {
            InitializeComponent();

            if (CrossDevice.Hardware.OperatingSystem == "WINDOWS")
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }



            _urls = new List<string>();

            // initialize the list that will contain the forms available to be proessed.
            _listOfFormsToBeProcessed = new List<Domain.Models.ResponseModels.Form>();
            btnSearch.BackgroundColor = Color.FromHex(AppStyle.ButtonBackGroundColor);
            
            //Dynamic Toolbar Text Size

            double screenWidth = CrossDevice.Hardware.ScreenWidth;
            _toolBarTextSize = screenWidth / 72;

            // Prepare the search controls for use by connecting delegates, etc.
            PrepareSearchControlsForUse();

            // Prepare the scan controls for use by connecting delegates, etc.
            PrepartScanControlsForUse();

            // Prepare the Add Form controls for use by connecting delegates, etc.
            PrepareAddFormControlsForUse();

            // Prepare the Process Form controls for use by connecting delegates, etc.
            PrepareProcessFormsForUse();

            // Prepare the search controls for use by connecting delegates, etc.
            PrepareRefreshFormsForUse();


            listPatientDeatils.ItemTapped += (snd, args) => { var item = snd as ViewCell; };

            listPatientDeatils.ItemSelected += (object sender, SelectedItemChangedEventArgs args) => {
                if (((ListView)sender).SelectedItem == null) return;

                var item = ((ListView)sender).SelectedItem as Domain.Models.ResponseModels.Form;

                if (null == item) return;

                if (item.Show == "checkbox_checked.png")
                {
                    _listOfFormsToBeProcessed.Remove(item);
                    item.UnShow = "checkbox.png";
                }
                else
                {
                    item.UnShow = "checkbox_checked.png";
                    _listOfFormsToBeProcessed.Add(item);
                }

                ((ListView)sender).SelectedItem = null;

                IsAnyFormSelected();
            };


            TapGestureRecognizer tapCheckClickTap = new TapGestureRecognizer();
            tapCheckClickTap.Tapped += (snd, args) => {
                var src = selectAllForms.Source as FileImageSource;

                if (null == src) Debug.WriteLine(
                    "ListToProcessForms.xaml.cs in constructor: src is null: " +
                        $"var src = selectAllForms.Source as FileImageSource;");

                if (src.File.ToString() == "checkbox.png")
                {
                    selectAllForms.Source = "checkbox_checked.png";

                    if (_listOfFormsToBeProcessed != null) {
                        _listOfFormsToBeProcessed.Clear();
                    }

                    foreach (var item in Record.Patient.Forms)
                    {
                        item.UnShow = "checkbox_checked.png";
                        _listOfFormsToBeProcessed.Add(item);
                    }
                }
                else
                {
                    selectAllForms.Source = "checkbox.png";
                    foreach (var item in Record.Patient.Forms)
                    {
                        item.UnShow = "checkbox.png";
                    }

                    if (_listOfFormsToBeProcessed != null) {
                        _listOfFormsToBeProcessed.Clear();
                    }
                }

                IsAnyFormSelected();
            };

            // detect tapping gesture.
            selectAllForms.GestureRecognizers.Add(tapCheckClickTap);

            btnSearch.Clicked += OnBtnSearchClicked;

            // don't enable the search unless there is text to be searched on.
            eSearch.TextChanged += (snd, args) => {
                IsSearchedNotNull = !string.IsNullOrWhiteSpace(eSearch.Text);
                IsAllValidationsTrue();
            };
        }

        public void IsAllValidationsTrue() {
            btnSearch.IsEnabled = IsSearchedNotNull;
        }

        public void IsAnyFormSelected()
        {
            if (_listOfFormsToBeProcessed!=null)
            {
                if (_listOfFormsToBeProcessed.Count > 0)
                {
                    IsFormsSelected = true;
                }
                else
                {
                    IsFormsSelected = false;
                }
            }
            else{
                IsFormsSelected = false;
            }
        }

        #region Search

        private TapGestureRecognizer _tapSearch = new TapGestureRecognizer();
        private TapGestureRecognizer _tapSearchLabel = new TapGestureRecognizer();
        private void PrepareSearchControlsForUse() {

            //TapGestureRecognizer tapSearch = new TapGestureRecognizer();
            _tapSearch.Tapped += OnSearchButtonClicked;
            toolBarSearchButton.GestureRecognizers.Add(_tapSearch);

            // lblSearch.FontSize = _toolBarTextSize;

            //TapGestureRecognizer tapSearchLabel = new TapGestureRecognizer();
            _tapSearchLabel.Tapped += OnSearchButtonClicked;
            lblSearch.GestureRecognizers.Add(_tapSearchLabel);

        }
        #endregion Search


        #region Scan
        TapGestureRecognizer _tapScan = new TapGestureRecognizer();
        TapGestureRecognizer _tapScanLabel = new TapGestureRecognizer();
        private void PrepartScanControlsForUse() {

            //TapGestureRecognizer tapScan = new TapGestureRecognizer();
            _tapScan.Tapped += OnScan;
            toolBarScannerButton.GestureRecognizers.Add(_tapScan);
            // lblScan.FontSize = _toolBarTextSize;

            //TapGestureRecognizer tapScanLabel = new TapGestureRecognizer();
            _tapScanLabel.Tapped += OnScan;
            lblScan.GestureRecognizers.Add(_tapScanLabel);

        }
        #endregion Scan


        #region Add Forms

        private TapGestureRecognizer _tapAddForms = new TapGestureRecognizer();
        private TapGestureRecognizer _tapAddFormsLabel = new TapGestureRecognizer();
        private void PrepareAddFormControlsForUse() {
            //TapGestureRecognizer tapAddForms = new TapGestureRecognizer();
            _tapAddForms.Tapped += OnAddFormsClicked;
            toolBarItemAdd.GestureRecognizers.Add(_tapAddForms);
            // lblAddForms.FontSize = _toolBarTextSize;

            //TapGestureRecognizer tapAddFormsLabel = new TapGestureRecognizer();
            _tapAddFormsLabel.Tapped += OnAddFormsClicked;
            lblAddForms.GestureRecognizers.Add(_tapAddFormsLabel);
        }

        #endregion Add Forms


        #region Process Forms

        private TapGestureRecognizer _tapProcessForms = new TapGestureRecognizer();
        private TapGestureRecognizer _tapProcessFormsLabel = new TapGestureRecognizer();
        private void PrepareProcessFormsForUse() {
            //TapGestureRecognizer tapProcessForms = new TapGestureRecognizer();
            _tapProcessForms.Tapped += OnProcessButtonClicked;
            toolBarprocessFormsButton.GestureRecognizers.Add(_tapProcessForms);
            // lblProcessForms.FontSize = _toolBarTextSize;

            //TapGestureRecognizer tapProcessFormsLabel = new TapGestureRecognizer();
            _tapProcessFormsLabel.Tapped += OnProcessButtonClicked;
            lblProcessForms.GestureRecognizers.Add(_tapProcessFormsLabel);
        }

        #endregion Process Forms


        #region Refresh Forms

        private TapGestureRecognizer _tapRefresh = new TapGestureRecognizer();
        private TapGestureRecognizer _tapRefreshlabel = new TapGestureRecognizer();
        private void PrepareRefreshFormsForUse() {
            //TapGestureRecognizer tapRefresh = new TapGestureRecognizer();
            _tapRefresh.Tapped += OnRefreshButtonClicked;
            toolBarRefreshFormsButton.GestureRecognizers.Add(_tapRefresh);
            // lblRefresh.FontSize = _toolBarTextSize;

            //TapGestureRecognizer tapRefreshlabel = new TapGestureRecognizer();
            _tapRefreshlabel.Tapped += OnRefreshButtonClicked;
            lblRefresh.GestureRecognizers.Add(_tapRefreshlabel);
        }

        #endregion Refresh Forms



        //
        // ***************************** SEARCH ***************************** 
        //
        private async void OnBtnSearchClicked(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(eSearch?.Text)) {
                await DisplayAlert("Search", "Enter an Account Number on which to search.", "OK");
                return;
            }

            using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
            {
                
            try {
                Record = await ViewModel.GetPersonDetialsByAccountNoAsync(eSearch.Text);
                _patientNumber = eSearch.Text;
            }
            catch {
                await DisplayAlert(Constants.ErrorMsgTitleOops, Constants.ErrorMsgSorry, "OK");
            }

            if (Record != null)
            {
               
                 
                if (Record.Success)
                {
                    eSearch.Unfocus();

                    if (Record.Patient.Forms.Count > 0)
                    {
                        listPatientDeatils.ItemsSource = Record.Patient.Forms;//   Record.Patient.Forms;

                        // stkSelectAllForms.IsVisible = true;
                        // stkTop.IsVisible = true;

                        // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
                        // stkSelectAllForms.IsVisible = true;
                        Settings.SetVisitId(Record.Patient.VisitId);

                        Settings.SetFormsList(Record.Patient.Forms);
                        string accountNumber = eSearch.Text;
                        string accNumber = Convert.ToString(eSearch.Text);

                        Settings.SetAccoiuntNumber(accNumber);

                        // toggle searched flag which is used to assist in control flow.
                        IsSearched = true;

                        //Application.Current.MainPage = new NavigationPage(new MainViewsMasters());

                        if (CrossDevice.Hardware.OperatingSystem == "WINDOWS")
                        {
                            Application.Current.MainPage = new NavigationPage(new MainViewsMasters());
                        }
                        else
                        {
                            await Navigation.PopAsync();
                            await Navigation.PushAsync(new MainViewsMasters());

                        }



                    }
                    else
                    {

                        // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
                        //IsSearched = false;
                        stkSelectAllForms.IsVisible = false;
                        stkTop.IsVisible = false;
                        IsSearched = false;

                        // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
                        //stkSelectAllForms.IsVisible = false;

                        List<Domain.Models.ResponseModels.FormModel> listF = new List<Domain.Models.ResponseModels.FormModel>();
                        Domain.Models.ResponseModels.FormModel F = new Domain.Models.ResponseModels.FormModel();
                        F.Name = "No Record Found";

                        listF.Add(F);

                        listPatientDeatils.ItemsSource = listF;
                        await DisplayAlert("Message", "No Record Found", "ok");
                        if (CrossDevice.Hardware.OperatingSystem == "WINDOWS")
                        {
                            Application.Current.MainPage = new NavigationPage(new MainViewsMasters());
                        }
                        else
                        {
                            await Navigation.PopAsync();
                            await Navigation.PushAsync(new MainViewsMasters());

                        }
                    }
                }
                else
                {

                    // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
                    //IsSearched = false;

                    stkTop.IsVisible = false;

                    // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
                    stkSelectAllForms.IsVisible = false;
                    List<Domain.Models.ResponseModels.FormModel> listF = new List<Domain.Models.ResponseModels.FormModel>();
                    Domain.Models.ResponseModels.FormModel F = new Domain.Models.ResponseModels.FormModel();
                    stkSelectAllForms.IsVisible = false;
                    F.Name = "No Record Found";
                    IsSearched = false;
                    listF.Add(F);

                    listPatientDeatils.ItemsSource = listF;
                    await DisplayAlert("Message", "No Record Found", "ok");

                    if (CrossDevice.Hardware.OperatingSystem == "WINDOWS")
                    {
                        Application.Current.MainPage = new NavigationPage(new MainViewsMasters());
                    }
                    else
                    {
                        await Navigation.PopAsync();
                        await Navigation.PushAsync(new MainViewsMasters());

                    }
                }
            }
            }

            if (Record.Success) {
                UpdatePatientProperties();
            }

            listPatientDeatils.IsVisible = true;
            
			// WEDNESDAY - MERGE SLIDER -- THIS ******************* THIS DID NOT EXIST IN SLIDER SIDE   REMOVED IN VERSION IN TFS
			

            stkSearch.IsVisible = false;
            eSearch.Text = "";
        }

        /// <summary>Updates a <c>User's</c> profile information.</summary>
        private void UpdatePatientProperties() {

            DateTime vardate = Record.Patient.DateOfBirth;
            string dateformat = vardate.ToString("MM/dd/yyyy");

            lblPatient.FormattedText = $"{Record.Patient.LastName}, {Record.Patient.FirstName}";
            lblPatientDateOfBirth.Text = dateformat;
            lblAccountNumber.Text = Record.Patient.AccountNumber;

            // Update the LocationId and FacilityId in Settings (these will be used when retrieving the form groups)
            // These values are used to ensure the correct form groups are displayed, based on the current patient's location and facility.
            Settings.SetLocationID(Record.Patient.LocationID);
            Settings.SetFacilityID(Record.Patient.FacilityID);
        }

        async void OnAddFormsClicked(object sender, EventArgs e) {
            if (IsSearched) {
                using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
                {
                    await Navigation.PushAsync(new FormGroupLists());
                }
            }
        }

        async void OnScan(object sender, EventArgs e)
        {
            var options = ConfigureMobileScannerOptions();

            // Get the scanPage
            var scanPage = ConfigureScanPageForScanner(options);

            scanPage.OnScanResult += (result) => {
                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread((Action)(async () => {
                    await Navigation.PopAsync();

                    if (result != null)
                    {
                        using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
                        {

                        try
                        {
                            _patientNumber = result.Text;
                            Record = await ViewModel.GetPersonDetialsByAccountNoAsync(result.Text);
                        }
                        catch
                        {
                            await DisplayAlert(Constants.ErrorMsgTitleOops, Constants.ErrorMsgSorry, "OK");
                        }

                        if (Record != null)
                        {
                            if (Record.Success)
                            {
                                listPatientDeatils.ItemsSource = Record.Patient.Forms;
                                stkTop.IsVisible = true;
                                stkSelectAllForms.IsVisible = true;
                                Settings.SetVisitId(Record.Patient.VisitId);
                                IsSearched = true;
                            }
                            else
                            {

                                IsSearched = false;
                                stkTop.IsVisible = false;
                                List<Domain.Models.ResponseModels.FormModel> listF =
                                    new List<Domain.Models.ResponseModels.FormModel>();
                                Domain.Models.ResponseModels.FormModel F =
                                    new Domain.Models.ResponseModels.FormModel();

                                stkSelectAllForms.IsVisible = false;
                                F.Name = "No Record Found";
                                listF.Add(F);
                                listPatientDeatils.ItemsSource = listF;
                            }
                        }

                        if (Record.Success)
                        {
                            UpdatePatientProperties();
                        }

                        listPatientDeatils.IsVisible = true;


                        stkSearch.IsVisible = false;
                        eSearch.Text = "";
                    }
                    }
                }));
            };

            // Navigate to our scanner page
            await Navigation.PushAsync(scanPage);
        }

        #region On Scan On appearing
       

        async Task OnScan()
        {
            var options = ConfigureMobileScannerOptions();
            
            // Get the scanPage
            var scanPage = ConfigureScanPageForScanner(options);

            scanPage.OnScanResult += (result) => {
                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread((Action)(async () => {
                    await Navigation.PopAsync();

                    if (result != null)
                    {
                        using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
                        {

                        try
                        {
                            _patientNumber = result.Text;
                            Record = await ViewModel.GetPersonDetialsByAccountNoAsync(result.Text);
                        }
                        catch
                        {
                            await DisplayAlert(Constants.ErrorMsgTitleOops, Constants.ErrorMsgSorry, "OK");
                        }

                        if (Record != null)
                        {
                            if (Record.Success)
                            {
                                listPatientDeatils.ItemsSource = Record.Patient.Forms;
                                stkTop.IsVisible = true;
                                stkSelectAllForms.IsVisible = true;
                                // WEDNESDAY - MERGE SLIDER --****************** THIS WAS REMOVED FROM CURRENT AND EXISTED IN SLIDER 
                                //stkSelectAllForms.IsVisible = true;

                                Settings.SetVisitId(Record.Patient.VisitId);
                                IsSearched = true;
                            }
                            else
                            {
                                // WEDNESDAY - MERGE SLIDER --****************** THIS WAS REMOVED FROM CURRENT AND EXISTED IN SLIDER 
                                IsSearched = false;
                                stkSelectAllForms.IsVisible = false;
                                stkTop.IsVisible = false;

                                // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
                                stkSelectAllForms.IsVisible = false;
                                List<Domain.Models.ResponseModels.FormModel> listF =
                                    new List<Domain.Models.ResponseModels.FormModel>();

                                Domain.Models.ResponseModels.FormModel F =
                                    new Domain.Models.ResponseModels.FormModel();

                                F.Name = "No Record Found";
                                listF.Add(F);
                                listPatientDeatils.ItemsSource = listF;
                            }
                        }
                        if (Record.Success)
                        {
                            UpdatePatientProperties();
                        }

                        listPatientDeatils.IsVisible = true;

                        // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN *** SLIDER


                        stkSearch.IsVisible = false;
                        eSearch.Text = "";
                    }
                    }
                }));
            };

            // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
			App.IsLoggedInFirstTime = false;

            // Navigate to our scanner page
            Device.BeginInvokeOnMainThread((Action)(async () => {
			
			// THIS WAS CONFIGURE AWAIT (FALSE) IN SLIDER
            	await Navigation.PushAsync(scanPage).ConfigureAwait(true);

            }));
        }


        #endregion

        private MobileBarcodeScanningOptions ConfigureMobileScannerOptions()
        {
            //setup options
            var options = new MobileBarcodeScanningOptions 
            {
                AutoRotate = false,
                UseFrontCameraIfAvailable = false,
                TryHarder = true,
                TryInverted = true,
                PossibleFormats = new List<ZXing.BarcodeFormat>
                {
                    //ZXing.BarcodeFormat.EAN_13 , ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.QR_CODE

                    ZXing.BarcodeFormat.QR_CODE, ZXing.BarcodeFormat.All_1D,
                }
            };

            return options;
        }


        private ZXingScannerPage ConfigureScanPageForScanner(MobileBarcodeScanningOptions options, 
            string title = "")
        {
           
            var scanPageTitle = String.IsNullOrWhiteSpace(title) ? "Scan QR or Bar code" : title;

            return new ZXingScannerPage(options) {
                DefaultOverlayTopText = "Align the code within the frame",
                DefaultOverlayBottomText = string.Empty,
                DefaultOverlayShowFlashButton = true,
                Title = scanPageTitle,

            };
        }


        async void OnLogoutButtonClicked(object sender, EventArgs e) {
            App.IsUserLoggedIn = false;

            if (CrossDevice.Hardware.OperatingSystem == "WINDOWS")
            {
                Application.Current.MainPage = new NavigationPage(new MainViewsMasters());
            }
            else
            {
                await Navigation.PopAsync();
                await Navigation.PushAsync(new MainViewsMasters());

            }
        }


        async void OnProcessButtonClicked(object sender, EventArgs e) {
            if (IsSearched && IsFormsSelected) {
                using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
                {
                   
                    try
                    {

                        Settings.SetFormsList(_listOfFormsToBeProcessed);
                        ProcessFormRecords = await ViewModel.ProcessFormsAsync();
                    }
                    catch
                    {
                        await DisplayAlert(Constants.ErrorMsgTitleOops, Constants.ErrorMsgSorry, "OK");
                    }

                    try
                    {
                        if (ProcessFormRecords != null)
                        {
                            if (ProcessFormRecords.Success)
                            {
                                if (ProcessFormRecords.URLs.Count > 0)
                                {
                                    Forms.item = ProcessFormRecords.URLs[0];
                                    await Navigation.PushAsync(new Forms());
                                }
                                else
                                {
                                    await DisplayAlert("Process Forms", "Please select a form to proceed.", "OK");
                                }
                            }
                            else
                            {
                                await DisplayAlert("Process Forms", "No form available for submission related to the supplied identifier.", "OK");
                            }
                        }
                    }
                    catch (Exception exNew)
                    {
                        Debug.Write("Process Forms Exception" + exNew);
                        await DisplayAlert("Process Forms", "No Form Url Found", "OK");
                    }


                }

            }
        }

        async void OnSearchButtonClicked(object sender, EventArgs e) {
            stkSearch.IsVisible = true;
            stkTop.IsVisible = false;
            listPatientDeatils.IsVisible = false;
            stkSelectAllForms.IsVisible = false;
            stkForms.IsVisible = false;
        }

        async void OnRefreshButtonClicked(object sender, EventArgs e) {
            if (IsSearched) {
                await Refresh();
            }


        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            try {
                
                IsAnyFormSelected();
                if (App.IsLoggedInFirstTime) {

                     Device.BeginInvokeOnMainThread((Action)(async () => {
                         await OnScan();
                     }));
                   
                    return;
                }
                else {
                    App.IsLoggedInFirstTime = false;
                }

                stkTop.IsVisible = false;
                stkForms.IsVisible = false;
                listPatientDeatils.IsVisible = false;
                stkSelectAllForms.IsVisible = false;


                if (IsSearched && Record != null)
                {
                    using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
                    {
                    Record = await ViewModel.GetPersonDetialsByAccountNoAsync(_patientNumber);
                    stkTop.IsVisible = true;
                    listPatientDeatils.IsVisible = true;
                    stkSelectAllForms.IsVisible = true;
                    if (Record != null && Record.Success)
                    {

                        stkSelectAllForms.IsVisible = true;
                        listPatientDeatils.ItemsSource = Record.Patient.Forms;
                        foreach (var item in Record.Patient.Forms)
                        {
                            item.UnShow = "checkbox.png";
                        }

                        if (_listOfFormsToBeProcessed != null)
                        {
                            _listOfFormsToBeProcessed.Clear();
                        }
                    }
                    else
                    {

                        // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
                        IsSearched = false;

                        stkTop.IsVisible = false;
                        List<Domain.Models.ResponseModels.FormModel> listF = new List<Domain.Models.ResponseModels.FormModel>();
                        Domain.Models.ResponseModels.FormModel F = new Domain.Models.ResponseModels.FormModel();
                        F.Name = "No Record Found";
                        stkSelectAllForms.IsVisible = false;
                        listF.Add(F);

                        listPatientDeatils.ItemsSource = listF;
                    }

                    //IsSearched
                    if (Record.Success)
                    {
                        UpdatePatientProperties();
                    }

                    stkSearch.IsVisible = false;
                    eSearch.Text = String.Empty;
                }
                }
                else{

                    stkSearch.IsVisible = true;
                }



                if (App.IsFormsAdded)
                {
                    using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
                    {
                    try
                    {
                        if (MainView._patientNumber != null)
                        {
                            Record = await ViewModel.GetPersonDetialsByAccountNoAsync(MainView._patientNumber);
                        }
                    }
                    catch
                    {
                        await DisplayAlert(Constants.ErrorMsgTitleOops, Constants.ErrorMsgSorry, "OK");
                    }

                    if (Record != null)
                    {
                        if (Record.Success)
                        {
                            if (Record.Patient.Forms.Count > 0)
                            {
                                listPatientDeatils.ItemsSource = Record.Patient.Forms;

                                foreach (var item in Record.Patient.Forms)
                                {
                                    item.UnShow = "checkbox.png";
                                }

                                if (_listOfFormsToBeProcessed != null)
                                {
                                    _listOfFormsToBeProcessed.Clear();
                                }


                                stkTop.IsVisible = true;

                                // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
                                stkSelectAllForms.IsVisible = true;

                                Settings.SetVisitId(Record.Patient.VisitId);

                                Settings.SetFormsList(Record.Patient.Forms);
                                string accountNumber = eSearch.Text;
                                string accNumber = Convert.ToString(MainView._patientNumber);

                                Settings.SetAccoiuntNumber(accNumber);
                                stkSelectAllForms.IsVisible = true;
                                IsSearched = true;
                                App.IsFormsAdded = false;
                            }
                            else
                            {

                                // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
                                IsSearched = false;

                                stkTop.IsVisible = false;

                                // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
                                stkSelectAllForms.IsVisible = false;

                                List<Domain.Models.ResponseModels.FormModel> listF = new List<Domain.Models.ResponseModels.FormModel>();
                                Domain.Models.ResponseModels.FormModel F = new Domain.Models.ResponseModels.FormModel();
                                F.Name = "No Record Found";
                                stkSelectAllForms.IsVisible = false;
                                App.IsFormsAdded = false;
                                listF.Add(F);

                                listPatientDeatils.ItemsSource = listF;
                            }
                        }
                        else
                        {

                            // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
                            IsSearched = false;

                            stkTop.IsVisible = false;

                            // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
                            stkSelectAllForms.IsVisible = false;

                            List<Domain.Models.ResponseModels.FormModel> listF = new List<Domain.Models.ResponseModels.FormModel>();
                            Domain.Models.ResponseModels.FormModel F = new Domain.Models.ResponseModels.FormModel();
                            F.Name = "No Record Found";
                            stkSelectAllForms.IsVisible = false;
                            listF.Add(F);

                            listPatientDeatils.ItemsSource = listF;
                            App.IsFormsAdded = false;
                        }
                    }

                    if (Record.Success)
                    {
                        UpdatePatientProperties();
                    }

                    listPatientDeatils.IsVisible = true;

                    // THIS DID NOT EXIST IN SLIDER


                    stkSearch.IsVisible = false;
                    eSearch.Text = "";
                }
                }
            }
            catch (Exception ex) {
                var innerMsg = (null == ex.InnerException) ? String.Empty : ex.InnerException.Message;
                var msg = $"Exception in MainView.xaml.cs --> OnAppearing(). Exception: {ex.Message}{Environment.NewLine} Inner Exception: {innerMsg}";
                Debug.WriteLine(msg);
            }
        }

        private async Task Refresh() {
            if (IsSearched && !String.IsNullOrWhiteSpace(_patientNumber))
            {

                using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
                {
                    try
                    {
                        Record = await ViewModel.GetPersonDetialsByAccountNoAsync(_patientNumber);
                    }
                    catch
                    {
                        await DisplayAlert("Refresh List", Constants.ErrorMsgSorry, "OK");
                    }

                    if (Record != null)
                    {
                        if (Record.Success)
                        {
                            eSearch.Unfocus();

                            if (Record.Patient.Forms.Count > 0)
                            {
                                listPatientDeatils.ItemsSource = Record.Patient.Forms;
                                stkTop.IsVisible = true;

                                // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
                                stkSelectAllForms.IsVisible = true;

                                Settings.SetVisitId(Record.Patient.VisitId);

                                Settings.SetFormsList(Record.Patient.Forms);
                                string accountNumber = eSearch.Text;
                                string accNumber = Convert.ToString(eSearch.Text);

                                Settings.SetAccoiuntNumber(accNumber);

                                IsSearched = true;

                                if (CrossDevice.Hardware.OperatingSystem == "WINDOWS")
                                {
                                    Application.Current.MainPage = new NavigationPage(new MainViewsMasters());
                                }
                                else
                                {
                                    await Navigation.PopAsync();
                                    await Navigation.PushAsync(new MainViewsMasters());

                                }
                            }
                            else
                            {

                                // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
                                IsSearched = false;

                                stkTop.IsVisible = false;

                                // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
                                stkSelectAllForms.IsVisible = false;

                                List<Domain.Models.ResponseModels.FormModel> listF = new List<Domain.Models.ResponseModels.FormModel>();
                                Domain.Models.ResponseModels.FormModel F = new Domain.Models.ResponseModels.FormModel();
                                F.Name = "No Record Found";

                                listF.Add(F);

                                listPatientDeatils.ItemsSource = listF;
                            }
                        }
                        else
                        {

                            // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
                            IsSearched = false;
                            stkTop.IsVisible = false;

                            // WEDNESDAY - MERGE SLIDER -- THIS WAS REMOVED IN VERSION IN TFS
                            stkSelectAllForms.IsVisible = false;

                            List<Domain.Models.ResponseModels.FormModel> listF = new List<Domain.Models.ResponseModels.FormModel>();
                            Domain.Models.ResponseModels.FormModel F = new Domain.Models.ResponseModels.FormModel();
                            F.Name = "No Record Found";

                            listF.Add(F);

                            listPatientDeatils.ItemsSource = listF;
                        }
                    }

                    if (Record.Success)
                    {
                        UpdatePatientProperties();
                    }

                    listPatientDeatils.IsVisible = true;

                    // THIS DIDN'T EXIST IN SLIDER
                    stkSelectAllForms.IsVisible = true;

                    stkSearch.IsVisible = false;
                    eSearch.Text = "";
                }
            }
        }
    }
}
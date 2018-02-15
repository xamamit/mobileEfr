using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Acs.Services.Helpers;
using Acs.Mobile.ESig.ViewModels;
using Acs.Mobile.ESig.Views.Base;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace Acs.Mobile.ESig.Views
{
    public partial class MainViews : ViewBase<LoginViewModel>
    {
        public static bool IsSearched = false;
        public static string patientNumber = "";

        public bool IsSearchedNotNull = false;

        public static Domain.Models.ResponseModels.PaitentModel record;
        Domain.Models.ResponseModels.ProcessPatientFormModel processFormRecords;

        public List<string> listOfForms;


        public MainViews()
        {


            InitializeComponent();


            #region Search

            TapGestureRecognizer tapSearch = new TapGestureRecognizer();
            tapSearch.Tapped += onSearchButtonClicked;
            toolBarSearchButton.GestureRecognizers.Add(tapSearch);

			#endregion

			#region Scan

			TapGestureRecognizer tapScan = new TapGestureRecognizer();
            tapScan.Tapped += onScan;
            toolBarScannerButton.GestureRecognizers.Add(tapScan);

			#endregion



			#region Add Forms
			TapGestureRecognizer tapAddForms = new TapGestureRecognizer();
            tapAddForms.Tapped += onAddFormsClicked;
            toolBarItemAdd.GestureRecognizers.Add(tapAddForms);

			#endregion


			#region Process Forms
			TapGestureRecognizer tapProcessForms = new TapGestureRecognizer();
            tapProcessForms.Tapped += OnProcessButtonClicked;
            toolBarprocessFormsButton.GestureRecognizers.Add(tapProcessForms);

			#endregion


			#region Refresh Forms
			TapGestureRecognizer tapRefresh = new TapGestureRecognizer();
            tapRefresh.Tapped += OnRefreshButtonClicked;
            toolBarRefreshFormsButton.GestureRecognizers.Add(tapRefresh);

            #endregion




            /*
            toolBarSearchButton.Clicked += onSearchButtonClicked;


            toolBarScannerButton.Clicked += onScan;


            toolBarItemAdd.Clicked += onAddFormsClicked;
			
            toolBarprocessFormsButton.Clicked += OnProcessButtonClicked;


            toolBarRefreshFormsButton.Clicked += OnRefreshButtonClicked;
*/

            listPatientDeatils.ItemTapped += (snd, args) =>
             {
                var item = snd as ViewCell;

                 
				 
              };

            listPatientDeatils.ItemSelected+=(object snd, SelectedItemChangedEventArgs args)=>
            {
                

               

			};

           

            btnSearch.Clicked += OnBtnSearchClicked;

            eSearch.TextChanged += (snd, args) =>
            {
                if(string.IsNullOrEmpty(eSearch.Text))
                {
                    IsSearchedNotNull = false;
                }
                else{

                    IsSearchedNotNull = true;
                }

                IsAllvalidationsTrue();
            };
        }



		

		public void IsAllvalidationsTrue()
		{
            if (IsSearchedNotNull == false)
			{
                btnSearch.IsEnabled = false;
			}
			
			else
			{

                btnSearch.IsEnabled = true;
			}

		}

		private async void OnBtnSearchClicked(object sender, EventArgs e)
		{

			using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
			{
				await Task.Delay(7000);
			}
			if (string.IsNullOrEmpty(eSearch.Text))
			{
				await DisplayAlert("Account Number Search", "Please enter an Account Number on which to search", "OK");
				
				return;
			}

			try
			{
				record = await ViewModel.GetPatientData(eSearch.Text);
				patientNumber = eSearch.Text;
			}
			catch
			{
				await DisplayAlert("Sorry!", "Something went wrong", "OK");
			}

			if (record != null)
			{

				using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
				{
					await Task.Delay(1000);
				}

				if (record.Success)
				{
                    eSearch.Unfocus();
                               
					if (record.Patient.Forms.Count > 0)
					{
                        listPatientDeatils.ItemsSource = record.Patient.Forms;
						stkTop.IsVisible = true;
						Settings.SetVisitId(record.Patient.VisitID);

						Settings.SetFormsList(record.Patient.Forms);
						string accountNumber = eSearch.Text;
                        string accNumber = Convert.ToString(eSearch.Text);

						Settings.SetAccoiuntNumber(accNumber);

						IsSearched = true;

						
                        Application.Current.MainPage = new NavigationPage(new MainViewsMasters());
					}
					else
					{

						stkTop.IsVisible = false;
						List<Domain.Models.ResponseModels.Form> listF = new List<Domain.Models.ResponseModels.Form>();
						Domain.Models.ResponseModels.Form F = new Domain.Models.ResponseModels.Form();
						F.Name = "No Record Found";

						listF.Add(F);

                        listPatientDeatils.ItemsSource = listF;
						

					}

				}
				else
				{

					stkTop.IsVisible = false;
					List<Domain.Models.ResponseModels.Form> listF = new List<Domain.Models.ResponseModels.Form>();
					Domain.Models.ResponseModels.Form F = new Domain.Models.ResponseModels.Form();
					F.Name = "No Record Found";

					listF.Add(F);

                    listPatientDeatils.ItemsSource = listF;

					
				}

			}
			if (record.Success)
			{

				if (record != null)
				{
					DateTime vardate = record.Patient.DateOfBirth;

					string dateformat = vardate.ToString("MM/dd/yyyy");

					// string patients = "";


					String patient = String.Format("{0}, {1}",
												   "Patient:"+" "+record.Patient.FirstName, record.Patient.LastName);
					lblPatient.FormattedText = patient;
                    lblPatientDateOfBirth.Text=dateformat;
				}

				if (record != null)
				{
                    lblMobileNumber.Text = record.Patient.AccountNumber;
				}

			
			}

            listPatientDeatils.IsVisible = true;

			stkSearch.IsVisible = false;
			eSearch.Text = "";

			
		}

        // TODO: MOVE THIS TO A CONSTANT CLASS.
        private static readonly string SearchResultDetailsTemplate = "NAME: {},{}";

		async void  onAddFormsClicked(object sender, EventArgs e)
		{
			if (IsSearched)
			{
				using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
				{
					await Task.Delay(5000);
				}

				await Navigation.PushAsync(new FormGroupLists());
			}

			/*
                stkTop.IsVisible = false;
                stkSearch.IsVisible = false;
                listPatientsDeatils.IsVisible = false;
                stkForms.IsVisible = true;
            */
		}



		async void onScan(object sender, EventArgs e)
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
                    ZXing.BarcodeFormat.EAN_13 , ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.QR_CODE
				}
			};

			var scanPage = new ZXingScannerPage(options)
			{
				DefaultOverlayTopText = "Align the barcode within the frame",
				DefaultOverlayBottomText = string.Empty,
				DefaultOverlayShowFlashButton = true,
                Title="Scan Code",
			};

			scanPage.OnScanResult += (result) =>
			{
				scanPage.IsScanning = false;

				Device.BeginInvokeOnMainThread(async () =>
				{
					await Navigation.PopAsync();
					await DisplayAlert("Scanned Code", result.Text, "OK");
					if (result != null)
					{
						try
						{
                            patientNumber = result.Text;
							record = await ViewModel.GetPatientData(result.Text);
						}
						catch
						{
							await DisplayAlert("Sorry!", "Something went wrong", "OK");
						}

						if (record != null)
						{
							if (record.Success)
							{
                                listPatientDeatils.ItemsSource = record.Patient.Forms;
								stkTop.IsVisible = true;
								Settings.SetVisitId(record.Patient.VisitID);
								IsSearched = true;
							}
							else
							{
								stkTop.IsVisible = false;
								List<Domain.Models.ResponseModels.Form> listF = new List<Domain.Models.ResponseModels.Form>();
								Domain.Models.ResponseModels.Form F = new Domain.Models.ResponseModels.Form();
								F.Name = "No Record Found";

								listF.Add(F);

								listPatientDeatils.ItemsSource = listF;
							}                            
						}
						if (record.Success)
						{

							if (record != null)
							{
								DateTime vardate = record.Patient.DateOfBirth;

								string dateformat = vardate.ToString("MM/dd/yyyy");

								String patient = String.Format("{0}, {1}",
												   "Patient:" + " " + record.Patient.FirstName, record.Patient.LastName);
								lblPatient.FormattedText = patient;
								lblPatientDateOfBirth.Text = dateformat;
							}

							if (record != null)
							{
                                lblMobileNumber.Text = record.Patient.AccountNumber;
							}
						}

						listPatientDeatils.IsVisible = true;

						stkSearch.IsVisible = false;
						eSearch.Text = "";
					}
				});
			};

			// Navigate to our scanner page
			await Navigation.PushAsync(scanPage).ConfigureAwait(false);
			// await Navigation.PushAsync(scanPage).ConfigureAwait(false);
		}


		#region On Scan On appearing
		async void onScan()
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
					ZXing.BarcodeFormat.EAN_13 , ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.QR_CODE
				}
			};

			var scanPage = new ZXingScannerPage(options)
			{
				DefaultOverlayTopText = "Align the barcode within the frame",
				DefaultOverlayBottomText = string.Empty,
				DefaultOverlayShowFlashButton = true,
                Title="Scan Code",
			};

			scanPage.OnScanResult += (result) =>
			{
				scanPage.IsScanning = false;

				Device.BeginInvokeOnMainThread(async () =>
				{
					await Navigation.PopAsync();
					await DisplayAlert("Scanned Code", result.Text, "OK");
					if (result != null)
					{
						try
						{
							patientNumber = result.Text;
							record = await ViewModel.GetPatientData(result.Text);
						}
						catch
						{
							await DisplayAlert("Sorry!", "Something went wrong", "OK");
						}

						if (record != null)
						{
							if (record.Success)
							{
								listPatientDeatils.ItemsSource = record.Patient.Forms;
								stkTop.IsVisible = true;
								Settings.SetVisitId(record.Patient.VisitID);
								IsSearched = true;
							}
							else
							{
								stkTop.IsVisible = false;
								List<Domain.Models.ResponseModels.Form> listF = new List<Domain.Models.ResponseModels.Form>();
								Domain.Models.ResponseModels.Form F = new Domain.Models.ResponseModels.Form();
								F.Name = "No Record Found";

								listF.Add(F);

								listPatientDeatils.ItemsSource = listF;
							}
						}

                        if (record.Success)
						{
							if (record != null)
							{
								DateTime vardate = record.Patient.DateOfBirth;

								string dateformat = vardate.ToString("MM/dd/yyyy");

								String patient = String.Format("{0}, {1}",
												   "Patient:" + " " + record.Patient.FirstName, record.Patient.LastName);
								lblPatient.FormattedText = patient;
								lblPatientDateOfBirth.Text = dateformat;
							}

							if (record != null)
							{
								lblMobileNumber.Text = record.Patient.AccountNumber;
							}
						}

						listPatientDeatils.IsVisible = true;

						stkSearch.IsVisible = false;
						eSearch.Text = "";
					}
				});
			};

		    App.IsLoggedInFirstTime = false;
		    // Navigate to our scanner page
		    // await Navigation.PushAsync(scanPage);
		    await Navigation.PushAsync(scanPage).ConfigureAwait(true);
        }

		#endregion

		async void OnLogoutButtonClicked(object sender, EventArgs e)
		{
			App.IsUserLoggedIn = false;
            //Navigation.InsertPageBefore(new LogViewMasters(), this);
            Application.Current.MainPage = new NavigationPage(new LogViewMasters());
			await Navigation.PopAsync().ConfigureAwait(false);
		}


		async void OnProcessButtonClicked(object sender, EventArgs e)
		{
			if (IsSearched)
			{
				using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
				{
					await Task.Delay(5000);
				}

				try
				{
					processFormRecords = await ViewModel.ProcessFormsAsync();
				}
				catch
				{
					await DisplayAlert("Sorry!", "Something went wrong", "OK");
				}

				if (processFormRecords != null)
				{
					if (processFormRecords.Success)
					{
						if (processFormRecords.URLs.Count > 0)
						{
							//await Navigation.PushAsync(new ListToProcessFormss(processFormRecords));
                            Application.Current.MainPage = new NavigationPage(new ProcessFormsMaster());
						}
						else
						{
						    await DisplayAlert("Forms", "No forms were found", "OK");
						}
                    }
					else
					{
                       await DisplayAlert("Forms", "No forms were found to be eligible for processing given the identifier.", "OK");
					}
				}
			}
		}


		async void onSearchButtonClicked(object sender, EventArgs e)
		{
			stkSearch.IsVisible = true;
			stkTop.IsVisible = false;
			listPatientDeatils.IsVisible = false;
			stkForms.IsVisible = false;
		}

       async void OnRefreshButtonClicked(object sender, EventArgs e)
		{
            if (IsSearched)
            {
                using (UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black))
                {
                    await Task.Delay(5000);
                }
            }
             refresh();
            /*
            eSearch.Text = "";
			stkSearch.IsVisible = true;
			stkTop.IsVisible = false;
			listPatientDeatils.IsVisible = false;
			stkForms.IsVisible = false;
			*/
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

		    try
		    {
		        if (App.IsLoggedInFirstTime)
		        {
		            onScan();

		            return;
		        }
		        else
		        {
		            App.IsLoggedInFirstTime = false;
		        }

                stkTop.IsVisible = false;
                    stkForms.IsVisible = false;
                    listPatientDeatils.IsVisible = false;
                    stkSearch.IsVisible = true;

                    if (IsSearched && record != null)
                    {
                        stkTop.IsVisible = true;
                        listPatientDeatils.IsVisible = true;
                        if (record != null && record.Success)
                        {
                            listPatientDeatils.ItemsSource = record.Patient.Forms;
                        }
                        else
                        {
                            stkTop.IsVisible = false;
                            List<Domain.Models.ResponseModels.Form> listF = new List<Domain.Models.ResponseModels.Form>();
                            Domain.Models.ResponseModels.Form F = new Domain.Models.ResponseModels.Form();
                            F.Name = "No Record Found";

                            listF.Add(F);

                            listPatientDeatils.ItemsSource = listF;
                        }

                        //IsSearched
                        if (record.Success)
                        {
                            if (record != null)
                            {
                                DateTime vardate = record.Patient.DateOfBirth;

                                string dateformat = vardate.ToString("MM/dd/yyyy");

                                String patient = String.Format("{0}, {1}",
                                                           "Patient:" + " " + record.Patient.FirstName, record.Patient.LastName);
                                lblPatient.FormattedText = patient;
                                lblPatientDateOfBirth.Text = dateformat;
                            }

                            if (record != null)
                            {
                                lblMobileNumber.Text = record.Patient.AccountNumber;
                            }
                        }

                        stkSearch.IsVisible = false;
                        eSearch.Text = "";
                    }

                    if (App.IsFormsAdded)
                    {
                        try
                        {
                            if (MainViews.patientNumber != null)
                            {
                                record = await ViewModel.GetPatientData(MainViews.patientNumber);
                            }
                        }
                        catch
                        {
                            await DisplayAlert("Sorry!", "Something went wrong", "OK");
                        }

                        if (record != null)
                        {
                            if (record.Success)
                            {
                                if (record.Patient.Forms.Count > 0)
                                {
                                    listPatientDeatils.ItemsSource = record.Patient.Forms;
                                    stkTop.IsVisible = true;
                                    Settings.SetVisitId(record.Patient.VisitID);

                                    Settings.SetFormsList(record.Patient.Forms);
                                    string accountNumber = eSearch.Text;
                                    string accNumber = Convert.ToString(MainViews.patientNumber);

                                    Settings.SetAccoiuntNumber(accNumber);

                                    IsSearched = true;
                                    App.IsFormsAdded = false;
                                }
                                else
                                {
                                    stkTop.IsVisible = false;
                                    List<Domain.Models.ResponseModels.Form> listF = new List<Domain.Models.ResponseModels.Form>();
                                    Domain.Models.ResponseModels.Form F = new Domain.Models.ResponseModels.Form();
                                    F.Name = "No Record Found";
                                    App.IsFormsAdded = false;
                                    listF.Add(F);

                                    listPatientDeatils.ItemsSource = listF;
                                }
                            }
                            else
                            {
                                stkTop.IsVisible = false;
                                List<Domain.Models.ResponseModels.Form> listF = new List<Domain.Models.ResponseModels.Form>();
                                Domain.Models.ResponseModels.Form F = new Domain.Models.ResponseModels.Form();
                                F.Name = "No Record Found";

                                listF.Add(F);

                                listPatientDeatils.ItemsSource = listF;
                                App.IsFormsAdded = false;
                            }
                        }
                        if (record.Success)
                        {
                            if (record != null)
                            {
                                DateTime vardate = record.Patient.DateOfBirth;

                                string dateformat = vardate.ToString("MM/dd/yyyy");

                                String patient = String.Format("{0}, {1}",
                                                           "Patient:" + " " + record.Patient.FirstName, record.Patient.LastName);
                                lblPatient.FormattedText = patient;
                                lblPatientDateOfBirth.Text = dateformat;
                            }

                            if (record != null)
                            {
                                lblMobileNumber.Text = record.Patient.AccountNumber;
                            }
                        }

                        listPatientDeatils.IsVisible = true;

                        stkSearch.IsVisible = false;
                        eSearch.Text = "";
                    }
            }
            catch
            {


            }
		}

        private async void refresh()
        {
            if (IsSearched && !string.IsNullOrEmpty(patientNumber))
            {
                try
                {
                    record = await ViewModel.GetPatientData(patientNumber);
                    //patientNumber = eSearch.Text;
                }
                catch
                {
                    await DisplayAlert("Sorry!", "Something went wrong", "OK");
                }

                if (record != null)
                {
                    if (record.Success)
                    {
                        eSearch.Unfocus();

                        if (record.Patient.Forms.Count > 0)
                        {
                            listPatientDeatils.ItemsSource = record.Patient.Forms;
                            stkTop.IsVisible = true;
                            Settings.SetVisitId(record.Patient.VisitID);

                            Settings.SetFormsList(record.Patient.Forms);
                            string accountNumber = eSearch.Text;
                            string accNumber = Convert.ToString(eSearch.Text);

                            Settings.SetAccoiuntNumber(accNumber);

                            IsSearched = true;

                            Application.Current.MainPage = new NavigationPage(new MainViewsMasters());
                        }
                        else
                        {
                            stkTop.IsVisible = false;
                            List<Domain.Models.ResponseModels.Form> listF = new List<Domain.Models.ResponseModels.Form>();
                            Domain.Models.ResponseModels.Form F = new Domain.Models.ResponseModels.Form();
                            F.Name = "No Records Found";

                            listF.Add(F);

                            listPatientDeatils.ItemsSource = listF;
                        }
                    }
                    else
                    {
                        stkTop.IsVisible = false;
                        List<Domain.Models.ResponseModels.Form> listF = new List<Domain.Models.ResponseModels.Form>();
                        Domain.Models.ResponseModels.Form F = new Domain.Models.ResponseModels.Form();
                        F.Name = "No records found";

                        listF.Add(F);

                        listPatientDeatils.ItemsSource = listF;
                    }
                }

                if (record.Success)
                {
                    if (record != null)
                    {
                        DateTime vardate = record.Patient.DateOfBirth;

                        string dateformat = vardate.ToString("MM/dd/yyyy");

                        String patient = String.Format("{0}, {1}",
                                                       "Patient:" + " " + record.Patient.FirstName, record.Patient.LastName);
                        lblPatient.FormattedText = patient;
                        lblPatientDateOfBirth.Text = dateformat;
                    }

                    if (record != null)
                    {
                        lblMobileNumber.Text = record.Patient.AccountNumber;
                    }
                }

                listPatientDeatils.IsVisible = true;

                stkSearch.IsVisible = false;
                eSearch.Text = String.Empty;
            }
        }
    }
}
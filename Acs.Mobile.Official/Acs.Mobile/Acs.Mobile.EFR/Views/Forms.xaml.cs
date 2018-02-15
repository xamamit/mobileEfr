using System;
using Acs.Mobile.EFR.Helpers;
using Acs.Mobile.EFR.ViewModels;
using Acs.Mobile.EFR.Views.Base;
using Xamarin.Forms;
using Plugin.DeviceInfo;

namespace Acs.Mobile.EFR.Views
{
    public partial class Forms : ViewBase<LoginViewModel>
    {
        private String _urlReceived = String.Empty;

        double widthScreen;
        int indexReceived = 0;
        public static string item;

        public Forms()
        {            
            indexReceived = MainView.ProcessFormRecords.URLs.IndexOf(item);
            _urlReceived = item;

            InitializeComponent();

            ac.Color = Color.FromHex(AppStyle.ActivityIndicatorColor);
            btnDone.BackgroundColor = Color.FromHex(AppStyle.ButtonBackGroundColor);

            webV.Source = _urlReceived;

            btnDone.Clicked += (snd, args) =>
            {
                if ((MainView.ProcessFormRecords.URLs.Count - 1) > indexReceived)
                {
                    indexReceived = indexReceived + 1;
                    item = MainView.ProcessFormRecords.URLs[indexReceived];
                    Navigation.PushAsync(new Forms());
                }
                else
                {
                   
                   if(CrossDevice.Hardware.OperatingSystem=="WINDOWS")
                    {
                        Application.Current.MainPage = new NavigationPage(new MainViewsMasters());
                    }
                    else
                    {
                        Navigation.PopAsync();
                        Navigation.PushAsync(new MainViewsMasters());

                    }

                  
                }
            };
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            widthScreen = width;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            webV.Source = _urlReceived;

            webV.IsVisible = true;
            ac.IsVisible = false;
        }
    }
}
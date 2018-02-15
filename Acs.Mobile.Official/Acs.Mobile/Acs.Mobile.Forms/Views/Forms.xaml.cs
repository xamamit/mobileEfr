
using System;
using System.Collections.Generic;
using System.Net.Http;
using Acs.Mobile.ESig.ViewModels;
using Acs.Mobile.ESig.Views.Base;
using Xamarin.Forms;

namespace Acs.Mobile.ESig.Views
{
    public partial class Forms : ViewBase<LoginViewModel>
    {
        private String _urlReceived = String.Empty;
        private double _widthScreen;

        public Forms(UrlPvt item, int index)
        {
            var indexReceived = ListToProcessForms.listUrlPvtToBeProcessed.IndexOf(item);
            _urlReceived = item.UrlName;

            InitializeComponent();

            webV.Source = _urlReceived;

            btnDone.Clicked += (snd, args) =>
            {
                if ((ListToProcessForms.listUrlPvtToBeProcessed.Count - 1) > indexReceived)
                {
                    indexReceived = indexReceived + 1;
                    Navigation.PushAsync(new Forms(ListToProcessForms.listUrlPvtToBeProcessed[indexReceived], indexReceived));
                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(new MainViewsMasters());
                }
            };
        }
        
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            _widthScreen = width;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            webV.Source = _urlReceived;
            webV.IsVisible = true;
            ac.IsVisible = false;
        }
    }
}
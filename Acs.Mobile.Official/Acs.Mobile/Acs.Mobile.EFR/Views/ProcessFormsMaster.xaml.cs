using System;
using System.Collections.Generic;
using Acs.Mobile.EFR.Controls;
using Acs.Mobile.EFR.ViewModels;
using Acs.Mobile.EFR.Views.Base;
using Xamarin.Forms;

namespace Acs.Mobile.EFR.Views
{
    public partial class ProcessFormsMaster : MasterDetailPage
    {
        public List<MasterPageItem> MenuList { get; set; }

        public ProcessFormsMaster()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            navigationDrawerList.ItemsSource = new MenuFactory()
                .CreatePageMenu(Constants.Menu_LoadPageOption_MainView);

            // Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ListOfFormsView)));
        }


        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;


            if (item.ItemTitle == Constants.MenuText_Logout)
            {
                App.IsUserLoggedIn = false;
                MainView.IsSearched = false;
                MainView.Record = null;
            }

            if (MainView.IsSearched)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            }

            else
            {
                if (item.ItemTitle != Constants.MenuText_ViewPatient)
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(page));
                }
            }

            IsPresented = false;
        }
    }
}
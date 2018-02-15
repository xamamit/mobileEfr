using System;
using System.Collections.Generic;
using Acs.Mobile.EFR.Controls;
using Plugin.DeviceInfo;
using Xamarin.Forms;

namespace Acs.Mobile.EFR.Views
{
    public partial class MainViewsMasters : MasterDetailPage
    {
        public List<MasterPageItem> MenuList { get; set; }

        public MainViewsMasters()
        {
            InitializeComponent();

            // Slider Side menu
            MasterBehavior = MasterBehavior.Popover;
            if(CrossDevice.Hardware.OperatingSystem=="WINDOWS")
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }
            else
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }

           

            navigationDrawerList.ItemsSource = new MenuFactory()
                .CreatePageMenu(Constants.Menu_LoadPageOption_MainView);

            // Initial navigation, this can be used for our home page
            if (App.IsUserLoggedIn)
            {
                if(CrossDevice.Hardware.OperatingSystem=="WINDOWS")
                {
                    Detail = new NavigationPage(new MainView());
                }
                else
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MainView)));
                }
               
            }
            else{

                if(CrossDevice.Hardware.OperatingSystem=="WINDOWS")
                {
                    Detail =new NavigationPage(new LoginView());
                }
                else
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(LoginView)));
                }

              
            }
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            if (item.ItemTitle == Constants.MenuText_Logout) {
                App.IsUserLoggedIn = false;
                MainView.IsSearched = false;
                MainView.Record = null;

                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MainViewsMasters)));
                IsPresented = false;
                return;
            }

            if (item.ItemTitle == Constants.MenuText_Home) {
              

                if(CrossDevice.Hardware.OperatingSystem=="WINDOWS")
                {
                    Application.Current.MainPage = new NavigationPage(new MainViewsMasters());
                }
                else
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MainViewsMasters)));
                    IsPresented = false;
                }

              
               // Navigation.PushAsync((Page)Activator.CreateInstance(page)); 
               // Navigation.PushAsync(new MainViewsMasters());
               // Detail = new MainView();
                return;
            }


            if (item.ItemTitle == Constants.MenuText_ViewPatient) {
             

                if(CrossDevice.Hardware.OperatingSystem=="WINDOWS")
                {
                     Application.Current.MainPage = new NavigationPage(new MainViewsMasters());
                }
                else
                {
                    Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MainViewsMasters)));
                    IsPresented = false;
                }


             
               // Navigation.PushAsync(new MainViewsMasters());
               
                return;
            }

            /*
            if (MainView.IsSearched) {
                Detail = (Page)Activator.CreateInstance(page);
            }
            else {
                if (item.ItemTitle != Constants.MenuText_ViewPatient) {
                    Detail =(Page)Activator.CreateInstance(page);
                }
            }
            */

            
            if (item.ItemTitle == Constants.MenuText_Settings)
            {

                if(CrossDevice.Hardware.OperatingSystem=="WINDOWS")
                {
                    try
                    {
                        Navigation.PushAsync(new SettingsView());
                    }
                    catch
                    {

                    }
                }
                else{

                    Navigation.PushAsync((Page)Activator.CreateInstance(page)); 
                }


                return;
            }

            if (item.ItemTitle == Constants.MenuText_About)
            {
                if(CrossDevice.Hardware.OperatingSystem=="WINDOWS")
                {
                    Navigation.PushAsync(new AboutView());
                }
                else{
                   // Application.Current.MainPage =new NavigationPage((Page)Activator.CreateInstance(page));
                    Navigation.PushAsync((Page)Activator.CreateInstance(page));
                }

                if(CrossDevice.Hardware.OperatingSystem!="WINDOWS")
                {
                    IsPresented = false;
                }
                
               

                return;
            }
            

            IsPresented = false;
        }


      

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsGestureEnabled = true;

        }
    }


    public class MasterPageItem
    {
        public string ItemTitle { get; set; }

        public string ItemIconSource { get; set; }

        public Type TargetType { get; set; }

        public Color ItemTextColor { get; set; }

        public bool IsItemVisible
        {
            get
            {
                if (!App.IsUserLoggedIn && ItemTitle == Constants.MenuText_Logout)
                {
                    return false;
                }

                if (!MainView.IsSearched && ItemTitle == Constants.MenuText_ViewPatient)
                {
                    return false;
                }

                return true;
            }
            set { }
        }
    }
}
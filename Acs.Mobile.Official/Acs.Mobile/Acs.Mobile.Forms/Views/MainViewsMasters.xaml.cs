using System;
using System.Collections.Generic;
using Acs.Mobile.ESig.Controls;
using Xamarin.Forms;

namespace Acs.Mobile.ESig.Views
{
    public partial class MainViewsMasters : MasterDetailPage
    {
        public List<MasterPageItem> menuList { get; set; }

        public MainViewsMasters()
        {
            InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			
			menuList = new List<MasterPageItem>();
			MenuFactory mf = new MenuFactory();

			menuList = mf.CreatePageMenu("MainViews");

			navigationDrawerList.ItemsSource = menuList;
	
            mf = null;
            menuList = null;
           
			// Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MainViews)));
        }

		private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = (MasterPageItem)e.SelectedItem;
			Type page = item.TargetType;

			if (item.Title == "Logout")
			{				
				App.IsUserLoggedIn = false;
				MainViews.IsSearched = false;
				MainViews.record = null;
			}

			if (MainViews.IsSearched)
			{
				Detail = new NavigationPage((Page)Activator.CreateInstance(page));
			}

			else
			{
				if (item.Title != "View Patient")
				{
					Detail = new NavigationPage((Page)Activator.CreateInstance(page));
				}
			}

			IsPresented = false;
		}
    }
}

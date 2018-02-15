using System;
using System.Collections.Generic;
using Acs.Mobile.ESig.Controls;
using Xamarin.Forms;

namespace Acs.Mobile.ESig.Views
{
    public partial class LogViewMasters : MasterDetailPage
    {
		public List<MasterPageItem> menuList { get; set; }
        public LogViewMasters()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            MenuFactory mf = new MenuFactory();

            menuList = mf.CreatePageMenu("LogViews");

			
			navigationDrawerList.ItemsSource = menuList;

            mf = null;
            menuList = null;
			// Initial navigation, this can be used for our home page
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(LogView)));
        }

		private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
		{

			var item = (MasterPageItem)e.SelectedItem;
			Type page = item.TargetType;

            if(MainViews.IsSearched)
            {
				
					Detail = new NavigationPage((Page)Activator.CreateInstance(page));
				
            }

            else{

				if (item.Title != "View Patient")
				{
					Detail = new NavigationPage((Page)Activator.CreateInstance(page));
				}

            }

			IsPresented = false;
		}
    }




    public class MasterPageItem {

		public string Title { get; set; }
		public string IconSource { get; set; }
		public Type TargetType { get; set; }
		public Color txtColor { get; set; }

		public bool isShown
		{
			get
			{

                if (!App.IsUserLoggedIn && Title=="Logout")
				{

                    return false;
				}

				else if(!MainViews.IsSearched && Title == "View Patient")
				{
                    return false;

				}
                else{

                    return true;
                }

               



			}


			set { }
		}

		

    }
}

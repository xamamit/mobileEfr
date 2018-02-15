using System;
using System.Collections.Generic;
using Acs.Mobile.ESig.Views;

namespace Acs.Mobile.ESig.Controls
{
    public class MenuFactory
    {
        public MenuFactory()
        {
        }

        public List<MasterPageItem> CreatePageMenu(string apppages)
        {
            List<MasterPageItem> menuList = new List<MasterPageItem>();
            
            switch(apppages)
            {
                case "LogView":
                    menuList = CreateMenuForLoginPage();
                    break;
                    
                case "MainViews":
                    menuList = CreateMenuForMainViewPage();
                    break;
                
                default:
                    menuList = CreateMenuForLoginPage();
                    break;
	            }
            
            return menuList;        
        }

        public List<MasterPageItem> CreateMenuForLoginPage()
        {
            List<MasterPageItem> menuList = new List<MasterPageItem>();

			var page2 = new MasterPageItem() { Title = "Settings", TargetType = typeof(SettingsView) };
			var page3 = new MasterPageItem() { Title = "About", TargetType = typeof(AbouttView) };
			var page4 = new MasterPageItem() { Title = "Logout", TargetType = typeof(SettingsView) };


			menuList.Add(page2);
			menuList.Add(page3);


			if (App.IsUserLoggedIn)
			{
				menuList.Add(page4);
			}

            return menuList;
            
        }


		public List<MasterPageItem> CreateMenuForMainViewPage()
		{
			List<MasterPageItem> menuList = new List<MasterPageItem>();

			var page0 = new MasterPageItem() { Title = "Home", TargetType = typeof(MainViewsMasters) };
			var page1 = new MasterPageItem() { Title = "View Patient", TargetType = typeof(MainViewsMasters) };
			var page2 = new MasterPageItem() { Title = "Settings", TargetType = typeof(SettingsView) };
			var page3 = new MasterPageItem() { Title = "About", TargetType = typeof(AbouttView) };
			var page4 = new MasterPageItem() { Title = "Logout", TargetType = typeof(LogViewMasters) };


			// Adding menu items to menuList


			menuList.Add(page0);

			if (MainViews.IsSearched)
			{
				menuList.Add(page1);
			}
			menuList.Add(page2);
			menuList.Add(page3);

			if (App.IsUserLoggedIn)
			{
				menuList.Add(page4);
			}


			return menuList;
		}
    }
}

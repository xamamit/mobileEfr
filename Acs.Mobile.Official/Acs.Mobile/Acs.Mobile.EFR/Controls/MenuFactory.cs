using System;
using System.Collections.Generic;
using Acs.Mobile.EFR.Views;

namespace Acs.Mobile.EFR.Controls
{
    public class MenuFactory
    {
        public MenuFactory() { }

        /// <summary>Creates the page menu.</summary>
        /// <param name="appPage">The application page.</param>
        /// <returns></returns>
        public List<MasterPageItem> CreatePageMenu(string appPage)
        {
            List<MasterPageItem> menuList = new List<MasterPageItem>();
            
            switch(appPage)
            {
                case Constants.Menu_LoadPageOption_LoginView: // "LoginView": // "LogView":
                    menuList = CreateMenuForLoginPage();
                    break;

                case Constants.Menu_LoadPageOption_MainView: // "MainView":
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

            var page2 = new MasterPageItem() { ItemTitle = Constants.MenuText_Settings, TargetType = typeof(SettingsView) };
            var page3 = new MasterPageItem() { ItemTitle = Constants.MenuText_About, TargetType = typeof(AboutView) };
            var page4 = new MasterPageItem() { ItemTitle = Constants.MenuText_Logout, TargetType = typeof(SettingsView) };


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

            var page0 = new MasterPageItem() { ItemTitle = Constants.MenuText_Home, TargetType = typeof(MainView) };
            var page1 = new MasterPageItem() { ItemTitle = Constants.MenuText_ViewPatient, TargetType = typeof(MainView) };
            var page2 = new MasterPageItem() { ItemTitle = Constants.MenuText_Settings, TargetType = typeof(SettingsView) };
            var page3 = new MasterPageItem() { ItemTitle = Constants.MenuText_About, TargetType = typeof(AboutView) };
            var page4 = new MasterPageItem() { ItemTitle = Constants.MenuText_Logout, TargetType = typeof(MainView) };
            var page5 = new MasterPageItem() { ItemTitle = Constants.MenuText_Login, TargetType = typeof(LoginView) };

            // Adding menu items to menuList

            if(App.IsUserLoggedIn)
            {
                menuList.Add(page0);
            }
            else{

                menuList.Add(page5);
            }

            if (MainView.IsSearched)
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

using System;

namespace Acs.Mobile.EFR
{
    //  Used for testing only. 
    public static class Constants
    {
        // 
        // Page Title Text
        //
        public static string Title_AboutPage = "About Access EFR";        
        public static string Title_FormGroupForms = "Add Form";
        public static string Title_FormGroupList = "Available Form Types";
        public static string Title_ListOfFormsSelectionPage = "Form Selection";
        public static string Title_HomePage = "Access EFR";
        public static string Title_LoginPage = "Access EFR";
        public static string Title_SettingsPage = "Application Settings";

        //
        // Menu Factory - View option ultimately passed into Menu Factory to construct the proper menu
        //
        internal const string Menu_LoadPageOption_MainView = "MainView";
        internal const string Menu_LoadPageOption_LoginView = "LoginView";
        internal const string Menu_LoadPageOption_2 = "2";

        //
        // Menu option's Text
        //
        public static string MenuText_ViewPatient = "View Patient";
        public static string MenuText_Logout = "Logout";
        public static string MenuText_About = "About";
        public static string MenuText_Home = "Home";
        public static string MenuText_Settings = "Settings";
        public static string MenuText_Login = "Login";


        //
        // Settings Page Fields
        //
        public static readonly string DefaultPassportServerUrl = "https://passport-lab.accessefm.com";

        // TODO: These should go in a global config.
        public static readonly string DefaultDomainName = "accessefm";
        public static readonly string DefaultBaseUrl = "https://passport-lab.accessefm.com/api/v2/mobile";

        public static readonly string DefaultApplicationId = "2";   // "Acs.EFR.Mobile"
        
        public static readonly string ErrorMsgSorry = "We're Sorry, something went wrong";
        public static readonly string ErrorMsgTitleOops = "Oops!";

        public static readonly string EmailRegex = @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$";

        public static readonly string ddTitle = "Form Selection List";

    }
}
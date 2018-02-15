
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Acs.Mobile.EFR.Droid.Controls;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Lang.Reflect;
using Plugin.Geolocator;
using Plugin.Iconize;

using Xamarin.Forms.Internals;

using Debug = System.Diagnostics.Debug;
namespace Acs.Mobile.EFR.Droid
{
    [Activity(Label = "Acs.Mobile.EFR",
        Icon = "@drawable/icon",
        Theme = "@style/MainTheme",
              MainLauncher = false,// Splash screen is the main launcher.
         
       ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
    )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const int _requestLocationId = 0;

        readonly string[] _permissions =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.ReadPhoneState,
            Manifest.Permission.Camera,
            Manifest.Permission.Flashlight              
        };

        const int RequestLocationId = 0;

        protected override void OnSaveInstanceState(Bundle outState)
        {
           
            base.OnSaveInstanceState(outState);

        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);

        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            try{


            }
            catch{
                
            }

        }


        protected override void OnCreate(Bundle savedInstanceState)
         {
           
            global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity.ToolbarResource =
                Resource.Layout.Toolbar;
            global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity.TabLayoutResource =
                Resource.Layout.Tabbar;
            base.OnCreate(savedInstanceState);


            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            UserDialogs.Init(this);

            FormsPlugin.Iconize.Droid.IconControls.Init(Resource.Id.toolbar, Resource.Id.tabMode);

            App.PhoneType = "ANDROID";

            try
            {
                if ((int)Build.VERSION.SdkInt >= 23)
                {
                    const string permissionLocation = Manifest.Permission.AccessFineLocation;
                    const string permissionCoarseLocation = Manifest.Permission.AccessCoarseLocation;
                    const string permissionReadPhoneState = Manifest.Permission.ReadPhoneState;

                    if (CheckSelfPermission(permissionLocation) 
                        == 
                        (int)Android.Content.PM.Permission.Granted && CheckSelfPermission(permissionCoarseLocation) 
                        == 
                        (int)Android.Content.PM.Permission.Granted && CheckSelfPermission(permissionReadPhoneState) 
                        == 
                        (int)Android.Content.PM.Permission.Granted )
                    {
                        LoadApplication(new App(new Setup()));
                    }
                    else
                    {
                        // Local permissions being passed in here.
                        //ActivityCompat.RequestPermissions(this, Permissions, RequestLocationId);
                        ActivityCompat.RequestPermissions(this, _permissions, _requestLocationId);
                    }
                }
                else
                {
                    LoadApplication(new App(new Setup()));
                }                
            }
            catch (Java.Lang.Exception je)
            {
                LogDebugMessage(je, "OnCreate(Bundle()");
            }
            catch (System.Exception se)
            {
                LogDebugMessage(se, "OnCreate(Bundle()");
            }
        }


        async Task GetLocationPermissionAsync()
        {
            //Check to see if any permission in our group is available, if one, then all are
            const string permission = Manifest.Permission.AccessFineLocation;
            if (CheckSelfPermission(permission) == (int)Android.Content.PM.Permission.Granted)
            {
                //await GetLocationAsync();
                // return;
            }

            //need to request permission
            if (ShouldShowRequestPermissionRationale(permission))
            {
                //Explain to the user why we need to read the contacts
                //Snackbar.Make(layout, "Location access is required to show coffee shops ne.", Snackbar.LengthIndefinite)
                //  .SetAction("OK", v => RequestPermissions(PermissionsLocation, RequestLocationId))
                // .Show();
                return;
            }

            //Finally request permissions with the list of permissions and Id. Permissions is local [] passed in.
            ActivityCompat.RequestPermissions(this, _permissions, _requestLocationId);
        }

        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            switch (requestCode)
            {
                case _requestLocationId:
                    {
                        if (grantResults[0] == (int)Android.Content.PM.Permission.Granted && grantResults[1] 
                            == 
                            (int)Android.Content.PM.Permission.Granted && grantResults[2] 
                            == 
                            (int)Android.Content.PM.Permission.Granted)
                        {
                            LoadApplication(new App(new Setup()));
                        }
                        else
                        {
                            CreateAndShowDialog("Please provide location permission and phone state permission as requested because without the permissions the app will not be able to register your device","Grant Permissions");
                        }
                    }

                    break;
            }
        }

        async Task GetLocationAsync()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;
                var position = await locator.GetPositionAsync();

                //textLocation.Text = string.Format("Lat: {0}  Long: {1}", position.Latitude, position.Longitude);
            }
            catch(System.Exception ex)
            {
                LogDebugMessage(ex, "GetLocationAsync()");
            }
        }

        static Class ActionMenuItemViewClass = null;
        static Constructor ActionMenuItemViewConstructor = null;
        static Typeface _typeface = null;

        public static Typeface Typeface
        {
            get
            {
                if (_typeface == null)
                    _typeface = Typeface.CreateFromAsset(Xamarin.Forms.Forms.Context.ApplicationContext.Assets, "fontawesome-webfont.ttf");

                return _typeface;
            }
        }

        public override View OnCreateView(string name, Context context, IAttributeSet attrs)
        {
            // TODO: remove hard-coded version information
            if (name.Equals("android.support.v7.view.menu.ActionMenuItemView", StringComparison.InvariantCultureIgnoreCase))
            {
                System.Diagnostics.Debug.WriteLine(name);
                var customLoginIfNeeded = CreateCustomToolbarItem(name, context, attrs);
                if (customLoginIfNeeded != null)
                    return customLoginIfNeeded;
            }

            return base.OnCreateView(name, context, attrs);
        }
        

        public override View OnCreateView(View parent, string name, Context context, IAttributeSet attrs)
        {
            if (name.Equals("android.support.v7.view.menu.ActionMenuItemView",
                StringComparison.InvariantCultureIgnoreCase))
            {
                System.Diagnostics.Debug.WriteLine($"Droid.MainActivity.cs -> CreateViewHelper : Name = {name}");
                System.Diagnostics.Debug.WriteLine(name);

                var customLoginIfNeeded = CreateCustomToolbarItem(name, context, attrs);
                if (customLoginIfNeeded != null)
                    return customLoginIfNeeded;
            }

            return base.OnCreateView(parent, name, context, attrs);
        }


        // TODO: Refactor this method into multiple helper methods and possibly into a control for Toolbar
        private View CreateCustomToolbarItem(string name, Context context, IAttributeSet attrs)
        {
            View view = null;

            try
            {
                if (null == ActionMenuItemViewClass)
                {
                    ActionMenuItemViewClass = ClassLoader.LoadClass(name);
                }
            }
            catch (ClassNotFoundException cnf) 
            {
                LogDebugMessage(cnf, "CreateCustomToolbarItem()");
                return null;
            }

            if (ActionMenuItemViewClass == null) return null;

            if (ActionMenuItemViewConstructor == null)
            {
                try
                {
                    ActionMenuItemViewConstructor = ActionMenuItemViewClass.GetConstructor(new Class[] {
                           Class.FromType(typeof(Context)),
                                Class.FromType(typeof(IAttributeSet))
                     });
                }
                catch (SecurityException se)
                {
                    LogDebugMessage(se, "CreateCustomToolbarItem()");
                    return null;
                }
                catch (NoSuchMethodException nsme)
                {
                    LogDebugMessage(nsme, "CreateCustomToolbarItem()");
                    return null;
                }
            }
            if (ActionMenuItemViewConstructor == null)
                return null;

            try
            {
                Java.Lang.Object[] args = new Java.Lang.Object[] { context, (Java.Lang.Object)attrs };
                view = (View)(ActionMenuItemViewConstructor.NewInstance(args));
            }
            catch (IllegalArgumentException)
            {
                //LogDebugMessage(se, "CreateCustomToolbarItem()");
                return null;
            }
            catch (InstantiationException)
            {
                //LogDebugMessage(se, "CreateCustomToolbarItem()");
                return null;
            }
            catch (IllegalAccessException)
            {
                //LogDebugMessage(se, "CreateCustomToolbarItem()");
                return null;
            }
            catch (InvocationTargetException)
            {
                //LogDebugMessage(se, "CreateCustomToolbarItem()");
                return null;
            }

            if (null == view) return null;
            View v = view;

            new Handler().Post(() =>
            {
                try
                {
                    if (v is LinearLayout)
                    {
                        var linearLayout = (LinearLayout)v;
                        for (int i = 0; i < linearLayout.ChildCount; i++)
                        {
                            var button = linearLayout.GetChildAt(i) as Button;

                            var title = button?.Text;

                            if (!string.IsNullOrEmpty(title) && title.Length == 1)
                            {
                                button.SetTypeface(Typeface, TypefaceStyle.Normal);
                            }
                        }
                    }
                    else if (v is TextView)
                    {
                        var tv = (TextView)v;
                        string title = tv.Text;

                        if (!string.IsNullOrEmpty(title) && title.Length == 1)
                        {
                            tv.SetTypeface(Typeface, TypefaceStyle.Normal);
                        }
                    }
                }
                catch (ClassCastException cce)
                {
                    LogDebugMessage(cce, "CreateCustomToolbarItem()");
                }
            });

            return view;
        }

        /// <summary>Creates the dialog and show it.</summary>
        /// <param name="message">The message the dialog will display.</param>
        /// <param name="title">The title for the Alert Dialog.</param>
        /// <remarks>If no title is provided a default will be used: "Alert Dialog".</remarks>
        public void CreateAndShowDialog(System.String message, System.String title)
        {
            // Validate arguments.
            if (System.String.IsNullOrWhiteSpace(message)) return;
            if (System.String.IsNullOrWhiteSpace(title)) title = "Alert Dialog";

            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.SetNeutralButton("OK", handllerNotingButton);
            builder.Create().Show();
        }

        void handllerNotingButton(object sender, DialogClickEventArgs e) { }

        /// <summary>Logs the debug message.</summary>
        /// <param name="excp">The excp to log.</param>
        /// <param name="methodName">Name of the method that should be added to the log message.</param>
        private void LogDebugMessage(System.Exception excp, System.String methodName = "")
        {
            try
            {   // ================================== Don't reorder the FOLLOWING lines ======================================
                // Null conditional operator being used...order matters.

                // Do null checks on parm and chaining properties of the parm(s). Setting the logging variables
                System.String excpMsg = excp?.Message ?? "No Exception provided for logging";
                System.String innerMsg = excp.InnerException?.Message ?? "No InnerException.Message Available.";
                System.String location = $"An Exception was encountered at: Droid.MainActivity.cs -> {methodName}{System.Environment.NewLine}: ";
                // ================================== Don't reorder the ABOVE lines =========================================

                System.String log = $"{location} Exception Info: {excpMsg}{System.Environment.NewLine} Inner Exception Info: {innerMsg}";

                // Log the message.
                System.Diagnostics.Debug.WriteLine(log);
            }
            catch(System.Exception ex)
            {
                // this is for debugging.
                string msg = ex.Message;
                return;
                // One of the only places it will be ok to 'swollow' an exception because there was an exception
                // trying to log exception information, so we shouldn't try and log again.
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
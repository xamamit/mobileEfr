using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Acs.Mobile.EFR.iOS
{
    public class Application
    {
        /// <summary>Defines the entry point of the application.</summary>
        /// <remarks>If you choose to use a custom Application Delegate class <c>"AppDelegate"</c> you 
        /// would want to do it here in Main.</remarks>
        /// <param name="args">The arguments to get the train rolling down the tracks.</param>
        static void Main(string[] args)
        {
            // Here's your chance to change to a different Application Delegate class from "AppDelegate"
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
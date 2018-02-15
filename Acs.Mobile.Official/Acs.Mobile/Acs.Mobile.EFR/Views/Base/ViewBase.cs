
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Autofac;
using Acs.Mobile.EFR.Configuration;
using Acs.Mobile.EFR.ViewModels.Base;
using Acs.Mobile.EFR.ViewModels;

namespace Acs.Mobile.EFR.Views.Base
{
    /// <summary>Acts as the base for all Views in the app.</summary>
    /// <typeparam name="T">The concrete view type.</typeparam>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public class ViewBase<T> : ContentPage where T : ViewModelBase
    {
        protected readonly T _vmToBindTo;
        private double width;
        private double height;

        public T ViewModel { get { return _vmToBindTo; } }

        /// <summary>Initializes a new instance of the <see cref="ViewBase{T}"/> class.</summary>
        public ViewBase()
        {
        
            // Make sure the container is disposed properly.
            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                _vmToBindTo = AppContainer.Container.Resolve<T>();
            }

            BindingContext = _vmToBindTo;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);


            try
            {
                LoginViewModel lvm = BindingContext as LoginViewModel;
                if (lvm != null)
                {
                   

                    if (width > height)
                    {
                        lvm.MenuIconSetIsVisible = true;
                        lvm.SetSpacingMenuItems = width/11;
                        lvm.SetToolBarSize = 22;
                        lvm.searchFontSize = width /55;
                    }
                    else
                    {
                        lvm.MenuIconSetIsVisible = true;
                        lvm.SetSpacingMenuItems = 25;
                        lvm.SetToolBarSize = 18;
                        lvm.searchFontSize = width / 36;
                    }

                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"EXCEPTION: Acx.Mobile.EFR -> ViewBase.cs -> OnSizeAllocated(): {ex.Message}");
            }
        }


    }
}
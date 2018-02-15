
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Acs.Mobile.EFR.ViewModels.Base
{
    /// <summary>
    /// ViewModel base class that facilitates direct data binding and observing a property.
    /// </summary>
    public class ViewModelBase : ExtendedBindableObject, IViewModel
    {
        protected ViewModelBase() { }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set => _isBusy = value;
        }
    }
}
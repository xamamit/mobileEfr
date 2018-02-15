using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


// TODO: Ensure all viewmodels derive from ViewModelBase and Implement IViewModel

namespace Acs.Mobile.ESig.ViewModels.Base
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
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
               //RaisePropertyChanged(() => IsBusy);
            }
        }


        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        //{
        //    if (null == PropertyChanged) { return; } //|| null == caller) { return; }

        //    PropertyChanged(this, new PropertyChangedEventArgs(caller));
        //}
    }
}
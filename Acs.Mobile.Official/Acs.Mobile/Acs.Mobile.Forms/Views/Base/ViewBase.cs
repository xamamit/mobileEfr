using Xamarin.Forms;
using Autofac;
using Acs.Mobile.ESig.Configuration;
using Acs.Mobile.ESig.ViewModels.Base;

namespace Acs.Mobile.ESig.Views.Base
{
    public class ViewBase<T> : ContentPage where T : ViewModelBase
    {
        protected readonly T _vmToBindTo;

        public T ViewModel { get { return _vmToBindTo; } }

        public ViewBase()
        {
            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                _vmToBindTo = AppContainer.Container.Resolve<T>();
            }

            BindingContext = _vmToBindTo;
        }

         
    }
}
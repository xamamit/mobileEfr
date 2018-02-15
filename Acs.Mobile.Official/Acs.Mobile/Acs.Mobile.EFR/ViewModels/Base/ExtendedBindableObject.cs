using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System.Reflection;
using Xamarin.Forms;

namespace Acs.Mobile.EFR.ViewModels.Base
{
    public abstract class ExtendedBindableObject : BindableObject
    {
        public void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            OnPropertyChanged(caller);            
        }
    }
}
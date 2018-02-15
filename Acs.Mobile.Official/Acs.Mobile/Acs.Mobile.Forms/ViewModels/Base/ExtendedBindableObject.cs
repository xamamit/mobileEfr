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

namespace Acs.Mobile.ESig.ViewModels.Base
{
    public abstract class ExtendedBindableObject : BindableObject
    {

       // public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            //if (null == PropertyChanged) { return; } //|| null == caller) { return; }

            OnPropertyChanged(caller);

            // PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }

        //public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        //{
        //    var name = GetMemberInfo(property).Name;
        //    OnPropertyChanged(name);
        //}


        //private MemberInfo GetMemberInfo(Expression expression)
        //{
        //    MemberExpression operand;
        //    LambdaExpression lambdaExpression = (LambdaExpression)expression;
        //    if (lambdaExpression.Body as UnaryExpression != null)
        //    {
        //        UnaryExpression body = (UnaryExpression)lambdaExpression.Body;
        //        operand = (MemberExpression)body.Operand;
        //    }
        //    else
        //    {
        //        operand = (MemberExpression)lambdaExpression.Body;
        //    }
        //    return operand.Member;
        //}
    }
}
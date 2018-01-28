using System;
using System.Linq.Expressions;
using System.Reflection;
using Xamarin.Forms;

namespace XamFormsPrototype.UI.Validation
{
    public class ExtendedBindableObject : BindableObject
    {
        public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            var name = GetMemberInfo(property).Name;
            OnPropertyChanged(name);
        }

        private MemberInfo GetMemberInfo(Expression expression)
        {
            var lambdaExpression = (LambdaExpression)expression;
            if (lambdaExpression.Body as UnaryExpression != null)
            {
                UnaryExpression body = (UnaryExpression)lambdaExpression.Body;
                return ((MemberExpression)body.Operand).Member;
            }
            else
            {
                return ((MemberExpression)lambdaExpression.Body).Member;
            }
        }
    }
}

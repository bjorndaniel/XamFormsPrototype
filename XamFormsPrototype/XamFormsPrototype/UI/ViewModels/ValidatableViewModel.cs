using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using XamFormsPrototype.Contracts;
using XamFormsPrototype.Helpers;

namespace XamFormsPrototype.UI.ViewModels
{
    public abstract class ValidatableViewModel : BaseViewModel
    {
        public bool Validate()
        {
            var valid = true;
            var props = GetType().GetPropertiesWithInterface<IValidity>();
            props.ForEach(_ =>
            {
                var isValid = ((IValidity)_.GetValue(this, null)).Validate();
                valid = valid && isValid;
            });
            return valid;
        }

        public bool Validate(string propertyName)
        {
            var prop = GetType().GetPropertyWithInterface<IValidity>(propertyName);
            if (prop == null)
            {
                return false;
            }
            return ((IValidity)prop.GetValue(this, null)).Validate();
        }

        public bool Validate<T>(Expression<Func<T, object>> property) where T : ValidatableViewModel =>
            Validate(Extensions.GetMemberName(property));
        

        public IEnumerable<string> GetErrorMessages()
        {
            Validate();
            var props = GetType().GetPropertiesWithInterface<IValidity>();
            var result = props.SelectMany(_ => ((IValidity)_.GetValue(this, null)).Errors);
            return result;
        }
    }
}

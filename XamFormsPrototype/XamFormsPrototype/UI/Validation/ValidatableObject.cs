﻿using System.Collections.Generic;
using System.Linq;
using XamFormsPrototype.Contracts;

namespace XamFormsPrototype.UI.Validation
{
    public class ValidatableObject<T> : ExtendedBindableObject, IValidity
    {
        private readonly List<IValidationRule<T>> _validations;
        private List<string> _errors;
        private T _value;
        private bool _isValid;

        public ValidatableObject(bool isValid = true)
        {
            _isValid = isValid;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
        }

        public List<IValidationRule<T>> Validations => _validations;

        public List<string> Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                _errors = value;
                RaisePropertyChanged(() => Errors);
            }
        }

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                RaisePropertyChanged(() => Value);
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        public object GetValue() => _value;

        public bool Validate()
        {
            Errors.Clear();
            var errors = _validations.Where(v => !v.Check(Value)).Select(v => v.ValidationMessage);
            Errors = errors.ToList();
            IsValid = !Errors.Any();
            return IsValid;
        }
    }
}

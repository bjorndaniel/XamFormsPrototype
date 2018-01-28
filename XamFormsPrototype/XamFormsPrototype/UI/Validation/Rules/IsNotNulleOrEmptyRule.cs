using XamFormsPrototype.Contracts;

namespace XamFormsPrototype.UI.Validation.Rules
{
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            var strVal = value as string;
            return !string.IsNullOrWhiteSpace(strVal);
        }
    }
}

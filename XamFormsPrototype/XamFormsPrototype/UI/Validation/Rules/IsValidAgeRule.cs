using XamFormsPrototype.Contracts;

namespace XamFormsPrototype.UI.Validation.Rules
{
    public class IsValidAgeRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if(int.TryParse(value?.ToString() ?? string.Empty, out int age))
            {
                return age > 15;
            }
            return false;
        }
    }
}
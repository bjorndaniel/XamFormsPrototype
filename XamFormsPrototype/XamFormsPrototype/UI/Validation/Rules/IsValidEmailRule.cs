using System;
using System.Globalization;
using System.Text.RegularExpressions;
using XamFormsPrototype.Contracts;

//From https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
namespace XamFormsPrototype.UI.Validation.Rules
{
    public class IsValidEmailRule<T> : IValidationRule<T>
    {
        private bool _invalid = false;

        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            var address = value?.ToString() ?? string.Empty;
            _invalid = false;
            if (String.IsNullOrEmpty(address))
            {
                return false;
            }
            try
            {
                address = Regex.Replace(address, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            if (_invalid)
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(address,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private string DomainMapper(Match match)
        {
            var idn = new IdnMapping();
            var domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                _invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}

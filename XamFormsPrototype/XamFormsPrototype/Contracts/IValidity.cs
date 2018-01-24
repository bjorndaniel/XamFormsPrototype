using System.Collections.Generic;

namespace XamFormsPrototype.Contracts
{
    public interface IValidity
    {
        bool IsValid { get; set; }

        bool Validate();

        List<string> Errors { get; set; }

        object GetValue();
    }
}

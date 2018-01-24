using System.Threading.Tasks;

namespace XamFormsPrototype.Contracts
{
    public interface IViewModel
    {
        Task<bool> Initialize();
    }
}

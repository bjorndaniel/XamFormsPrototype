using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace XamFormsPrototype.Contracts
{
    public interface IRepository
    {
        Task<T> Add<T>(T entity) where T : IEntity, new();

        Task<int> Count<T>() where T : IEntity, new();

        Task Delete<T>(int id) where T : IEntity, new();

        Task Delete<T>(T entity) where T : IEntity, new();

        Task DeleteAll<T>() where T : IEntity, new();

        Task<T> FirstOrDefault<T>(Expression<Func<T, bool>> query) where T : IEntity, new();

        Task<List<T>> GetAll<T>() where T : IEntity, new();

        Task<IEnumerable<T>> Search<T>(Expression<Func<T, bool>> predicate) where T : IEntity, new();

        Task<T> Update<T>(T entity) where T : IEntity, new();
    }
}

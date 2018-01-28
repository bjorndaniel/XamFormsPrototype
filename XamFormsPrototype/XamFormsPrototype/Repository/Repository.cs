using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using XamFormsPrototype.Contracts;
using XamFormsPrototype.Model;

namespace XamFormsPrototype.Repository
{
    public class Repository : IRepository
    {
        private SQLiteAsyncConnection _database;

        public Repository(IFileHelper fileHelper)
        {
            var dbPath = fileHelper.GetLocalFilePath("XamFormsPrototype.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            InitDatabase();
        }

        public async Task<T> Add<T>(T entity) where T : IEntity, new()
        {
            await _database.InsertAsync(entity);
            return entity;
        }

        public async Task<int> Count<T>() where T : IEntity, new() =>
            await _database.Table<T>().CountAsync();

        public async Task Delete<T>(int id) where T : IEntity, new()
        {
            await _database.DeleteAsync<T>(id);
        }

        public async Task Delete<T>(T entity) where T : IEntity, new()
        {
            await _database.DeleteAsync(entity);
        }

        public async Task DeleteAll<T>() where T : IEntity, new()
        {
            await _database.DeleteAllAsync<T>();
        }

        public async Task<T> FirstOrDefault<T>(Expression<Func<T, bool>> query) where T : IEntity, new() =>
            await _database.Table<T>().FirstOrDefaultAsync(query);

        public async Task<List<T>> GetAll<T>() where T : IEntity, new() =>
            await _database.Table<T>().ToListAsync();

        public async Task<IEnumerable<T>> Search<T>(Expression<Func<T, bool>> predicate) where T : IEntity, new() =>
            await _database.Table<T>().Where(predicate).ToListAsync();

        public async Task<T> Update<T>(T entity) where T : IEntity, new()
        {
            var id = await _database.InsertOrReplaceAsync(entity);
            return await _database.GetAsync<T>(id);
        }

        private void InitDatabase()
        {
            foreach (var t in Helpers.Extensions.GetClassesWithAttribute<TableAttribute>(Assembly.GetAssembly(typeof(Album))))
            {
                if (!_database.TableMappings.Any(_ => _.TableName.Equals(t.GetCustomAttribute<TableAttribute>().Name)))
                {
                    _database.CreateTableAsync(t).Wait();
                }
            }
        }
    }
}

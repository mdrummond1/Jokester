using Jokester.Models;
using Jokester.Services.Interfaces;
using SQLite;

namespace Jokester.Services.Impl
{
    public class SqlLiteDataBaseDataService : IDataService
    {
        private SQLiteConnection connection;

        public SqlLiteDataBaseDataService(string filename, List<Type> types)
        {
            connection = new SQLiteConnection(filename);

            connection.CreateTables(types: types.ToArray());
        }

        public void Delete<T>(int id)
        {
            connection.Delete<T>(id);
        }

        public void Insert<T>(T entity)
        {
            connection.Insert(entity);
        }

        public IEnumerable<T> Load<T>() where T : new()
        {
            return connection.Table<T>().ToList();
        }

        public T LoadItem<T>(int id) where T : Entity<int>, new()
        {
            return connection.Table<T>().Where(i => i.ID == id).FirstOrDefault();
        }

        public void Update<T>(T entity) where T: Entity<int>, new()
        {
            connection.Update(entity);
        }
    }
}

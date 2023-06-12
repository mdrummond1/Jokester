using Jokester.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jokester.Services.Interfaces
{
    public interface IDataService
    {
        IEnumerable<T> Load<T>() where T : new();
        void Insert<T>(T entity);
        void Update<T>(T entity) where T : Entity<int>, new();
        void Delete<T>(int id);
    }
}

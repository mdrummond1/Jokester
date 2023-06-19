using Jokester.Models;
using Jokester.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jokester.Tests.Mocks
{
    public class MockDataService : IDataService
    {
        public void Delete<T>(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Load<T>() where T : new()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : Entity<int>, new()
        {
            throw new NotImplementedException();
        }
    }
}

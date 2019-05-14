using System;
using System.Collections.Generic;
using System.Text;

namespace Tocci.DataAccess
{
    public class InMemoryRepository<T> : IRepository<T>
    {
        static List<EndpointReport> Reports = new List<EndpointReport>(); 
        public T Get(string id)
        {
            throw new NotImplementedException();
        }

        public bool Save(T T)
        {
            throw new NotImplementedException();
        }
    }
}

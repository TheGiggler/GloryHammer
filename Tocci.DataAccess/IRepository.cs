using System;

namespace Tocci.DataAccess
{
    public interface IRepository<T>
    {
        bool Save(T T);
        T Get(string id);
    }
}

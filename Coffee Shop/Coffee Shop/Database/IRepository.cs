using System;
using System.Collections.Generic;

namespace Coffee_Shop.Database
{
    interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetIEnumerable();
        T? Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}
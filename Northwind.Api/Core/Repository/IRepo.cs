using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Northwind.Api.Core.Repository
{
    public interface IRepo<T>
    {
        T Get(int id);
        IQueryable<T> GetAll();
        T Insert(T o);
        T Update(T o);
        void Delete(int id);
        void Save();
    }
}
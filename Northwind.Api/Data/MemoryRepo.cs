using Northwind.Api.Core.Model;
using Northwind.Api.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Northwind.Api.Data
{
    public class MemoryRepo<T> : IRepo<T> where T : Entity, new()
    {
        private static Dictionary<int, T> memoryRepo = new Dictionary<int, T>();

        public void Delete(int id)
        {
            memoryRepo.Remove(id);
        }

        public T Get(int id)
        {
            if (memoryRepo.ContainsKey(id) == false)
                return null;
            else
                return memoryRepo[id];
        }

        public IQueryable<T> GetAll()
        {
            return memoryRepo.Values.ToList<T>().AsQueryable();
        }

        public T Insert(T o)
        {
            if (memoryRepo.Count > 0)
                o.Id = memoryRepo.Last().Key + 1;
            else
                o.Id = 1;

            memoryRepo.Add(o.Id, o);
            return o;
        }

        //public void Restore(T o)
        //{
        //    //throw new NotImplementedException();
        //}

        public void Save()
        {
            //throw new NotImplementedException();
        }

        public T Update(T o)
        {
            memoryRepo[o.Id] = o;

            return memoryRepo[o.Id];
        }
    }
}
using Northwind.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Northwind.Api.Core.Service
{
    public interface ICrudService<T> where T : Entity, new()
    {
        int Create(T item);

        void Update(T item);

        void Delete(int id);

        T Get(int id);

        IEnumerable<T> GetAll();

        //TODO: finish interface design - missing properties

        //IEnumerable<T> Where(Expression<Func<T, bool>> func, bool showDeleted = false);

    }
}
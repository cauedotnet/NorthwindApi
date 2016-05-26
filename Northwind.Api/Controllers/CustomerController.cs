using Northwind.Api.Core.Model;
using Northwind.Api.Core.Repository;
using Northwind.Api.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Northwind.Api.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly IRepo<Customer> _repo;
        private readonly ICrudService<Customer> _service;

        public CustomerController(ICrudService<Customer> service, IRepo<Customer> custRepo)
            //: base(service)
        {
            this._repo = custRepo;
            this._service = service;
        }

        // GET api/values
        public IEnumerable<Customer> Get()
        {
            return _service.GetAll().ToList();
        }

        // GET api/values/5
        public Customer Get(int id)
        {
            return _service.Get(id);
        }

        // POST api/values
        public int Post(Customer cust)
        {
            //TODO: Is this the best place to hold this logic?
            if (cust.Id == 0)
                cust.Id = _service.Create(cust);
            else
                _service.Update(cust);

            return cust.Id;
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            _service.Delete(id);

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }


    }
}

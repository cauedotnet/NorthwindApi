using Northwind.Api.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Northwind.Api.Core.Model;
using Northwind.Api.Core.Repository;
using Northwind.Api.Core.Model.Validation;
using System.Web.Mvc;

namespace Northwind.Api.Service
{
    public class CustomerService : ICustomerService
    {
        private IRepo<Customer> _repo;
        private IValidate<Customer> _validator;
        public CustomerService(IRepo<Customer> repository, IValidate<Customer> validator)
        {
            _repo = repository;
            _validator = validator;
        }

        public int Create(Customer item)
        {
            // Validation will be used as an example of MOQ setups - see customerservicetests.
            var secValidation = _validator.RunValidation(item);

            //
            var isCompanyAuth = AuthCompanyNameViaWSCall(item.CompanyName);

            var ret = _repo.Insert(item);
            _repo.Save();

            return ret.Id;
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
            _repo.Save();
        }

        public Customer Get(int id)
        {
            return _repo.Get(id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _repo.GetAll();
        }

        public void Update(Customer cust)
        {
            _repo.Update(cust);
            _repo.Save();
        }

        // Silly example use of OutputCache for some lazy operation. In this case the best would be: to execute the task on
        // background to not compromise the performance of the application when a user is executing the resquest. 
        [OutputCache(Duration = 10, VaryByParam = "companyName")]
        // Lets pretend we need to auth something in a distance WS.
        public bool AuthCompanyNameViaWSCall(string companyName)
        {
            return true;
        }
    }
}
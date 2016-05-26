using System;
using NUnit.Framework;
using Northwind.Api.Service;
using Northwind.Api.Core.Repository;
using Northwind.Api.Core.Model;
using Northwind.Api.Data;
using Northwind.Api.Core.Model.Validation;
using Moq;

namespace Northwind.Api.Tests
{
    [TestFixture]
    public class CustomerServiceTest
    {
        private CustomerService _service; 
        private IRepo<Customer> _repo;
        private IValidate<Customer> _validator;
        private Customer _simpleCustomer;
        [SetUp]
        public void SetUp()
        {
            // This show MOQ in action
            //_validator = new ValidateCustomer();
            var mock = new Mock<IValidate<Customer>>();
            mock.Setup(foo => foo.RunValidation(null)).Returns(true);
            _validator = mock.Object;

            _repo = new MemoryRepo<Customer>();
            _service = new CustomerService(_repo, _validator);
            _simpleCustomer = new Customer { CompanyName = "Soft Company", ContactName = "Martin Fowler" };
        }

        [Test]
        public void TestInsert()
        {
            var cust = _service.Create(_simpleCustomer);
            _repo.Save();

            var cust1 = _service.Get(cust);

            Assert.AreNotEqual(cust, 0);
            Assert.AreEqual(_simpleCustomer.ContactName, cust1.ContactName);
        }

        [Test]
        public void TestUpdate()
        {
            var newCompanyName = "Oracle";

            var custId = _service.Create(_simpleCustomer);
            _repo.Save();

            var cust = _service.Get(custId);
            cust.CompanyName = newCompanyName;

            _service.Update(cust);

            var testCust = _service.Get(cust.Id);

            Assert.AreEqual(testCust.CompanyName, newCompanyName);
            Assert.AreNotEqual(cust.Id, 0);
        }

        [Test]
        public void TestDelete()
        {
            var custId = _service.Create(_simpleCustomer);
            _repo.Save();

            _repo.Delete(custId);
            _repo.Save();

            var cust = _repo.Get(custId);

            Assert.AreEqual(cust, null);
        }
    }
}

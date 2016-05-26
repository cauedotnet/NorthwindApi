using System;
using NUnit.Framework;
using Northwind.Api.Controllers;
using Northwind.Api.Core.Model;
using Northwind.Api.Service;
using Northwind.Api.Data;
using Northwind.Api.Core.Repository;
using System.Net;
using Northwind.Api.Core.Model.Validation;
using Moq;

namespace Northwind.Api.Tests
{
    [TestFixture]
    public class CustomerControllerTest
    {
        private CustomerService _service;
        private IRepo<Customer> _repo;
        private IValidate<Customer> _validator;
        private CustomerController _controller;
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
            _controller = new CustomerController(_service, _repo);
            _simpleCustomer = new Customer { CompanyName = "Microsoft", ContactName = "Bill Gates" };

        }

        [Test]
        public void CreateCustomerAndReturnsId()
        {
            var ret = _controller.Post(_simpleCustomer);

            Assert.AreNotEqual(ret, 0);
            //Assert.AreEqual(ret.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public void UpdateCustomer()
        {
            var newCompany = "DELL";
            var ret = _controller.Post(_simpleCustomer);

            var cust = _controller.Get(ret);

            cust.CompanyName = newCompany;
            _controller.Post(cust);

            cust = _controller.Get(ret);

            Assert.AreEqual(cust.CompanyName, newCompany);
        }

        [Test]
        public void DeleteCustomerAndReturns204()
        {
            var cust = _controller.Post(_simpleCustomer);

            var ret = _controller.Delete(cust);

            Assert.AreEqual(ret.StatusCode, HttpStatusCode.NoContent);
        }
    }
}

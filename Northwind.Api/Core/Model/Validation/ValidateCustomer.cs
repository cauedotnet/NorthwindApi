using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Northwind.Api.Core.Model.Validation
{
    public class ValidateCustomer : IValidate<Customer>
    {
        public bool RunValidation(Customer o)
        {
            throw new NotImplementedException();
        }
    }
}
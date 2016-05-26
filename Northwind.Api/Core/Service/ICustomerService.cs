using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Northwind.Api.Core.Model;

namespace Northwind.Api.Core.Service
{
    public interface ICustomerService : ICrudService<Customer>
    {
        // Lets pretend we need to auth something in a distance WS.
        bool AuthCompanyNameViaWSCall(string companyName);
    }
}
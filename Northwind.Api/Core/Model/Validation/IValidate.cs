using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Northwind.Api.Core.Model.Validation
{
    public interface IValidate<T>
    {
        bool RunValidation(T o);
    }
}
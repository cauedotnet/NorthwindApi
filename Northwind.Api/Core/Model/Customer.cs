﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Northwind.Api.Core.Model
{
    public class Customer : Entity
    {
        public string CompanyName { get; set; }
        public string ContactName { get; set; }

        //TODO: Missing Properties
    }
}
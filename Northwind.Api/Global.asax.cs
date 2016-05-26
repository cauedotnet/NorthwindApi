using Northwind.Api.Core.Model;
using Northwind.Api.Core.Model.Validation;
using Northwind.Api.Core.Repository;
using Northwind.Api.Core.Service;
using Northwind.Api.Data;
using Northwind.Api.Service;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Northwind.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            InitializeSimpleInjector();


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void InitializeSimpleInjector()
        {
            // Create the container as usual.
            var container = new Container();

            // Register your types, for instance using the RegisterWebApiRequest
            // extension from the integration package:
            container.RegisterWebApiRequest<IRepo<Customer>, MemoryRepo<Customer>>();
            container.RegisterWebApiRequest<ICrudService<Customer>, CustomerService>();
            container.RegisterWebApiRequest<IValidate<Customer>, ValidateCustomer>();

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            //TODO: in the future we can use alt configs for using a DB instead of MemoryRepo (see above ;D)
#if DEBUG
            SeedFakeData(container);
    #endif
        }

        //TODO: SeedFakeData - Future Refactoring...Move and organize.
        protected void SeedFakeData(Container container)
        {
            using (container.BeginExecutionContextScope())
            {
                var cust = container.GetInstance<IRepo<Customer>>();

                cust.Insert(new Customer() { CompanyName = "1", ContactName = "1" });
                cust.Insert(new Customer() { CompanyName = "2", ContactName = "2" });
                cust.Insert(new Customer() { CompanyName = "3", ContactName = "3" });
                cust.Insert(new Customer() { CompanyName = "4", ContactName = "4" });
                cust.Insert(new Customer() { CompanyName = "5", ContactName = "5" });

                cust.Save();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Northwind.Api
{
    //TODO: Evaluate the best approach for handling global exceptions. Full cover on following link.
    //  https://ruhul.wordpress.com/2014/09/05/how-to-handle-exceptions-globally-in-asp-net-webapi-2/
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;

            //TODO: IMPORTANT - Log global exceptions - Implement logging classes.

            //var logger = actionExecutedContext.ActionContext.ControllerContext.Configuration.DependencyResolver.GetService(
            //        typeof(ILogger)) as ILogger;
            //if (logger != null)
            //{
            //    logger.Error(exception, exception.Message);
            //}

            const string genericErrorMessage = "An unexpected error occured";

            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(genericErrorMessage)
            };

            response.Headers.Add("X-Error", genericErrorMessage);
        }
    }
}
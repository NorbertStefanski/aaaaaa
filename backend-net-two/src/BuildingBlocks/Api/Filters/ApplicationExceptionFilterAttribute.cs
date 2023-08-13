using Api.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Domain;
using Microsoft.AspNetCore.Http.Extensions;

namespace Api.Filters
{
    public class ApplicationExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public ApplicationExceptionFilterAttribute(ILogger logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ContractException _:
                case InvalidOperationException _:

                    _logger.LogWarning(context.Exception, $"Bad request detected: {GetRequestUrl(context)}");

                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Result = new JsonResult(new ErrorModel(context.Exception.Message));
                    break;
                default:
                    _logger.LogError(context.Exception,
                        $"An unhandled exception occurred in the application. Request: {GetRequestUrl(context)}");
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Result = new JsonResult(new ErrorModel("An unexpected error has occurred."));
                    break;
            }
        }

        private string GetRequestUrl(ExceptionContext context)
        {
            return $"{context.HttpContext.Request.Method} - {context.HttpContext.Request.GetDisplayUrl()}";
        }
    }
}

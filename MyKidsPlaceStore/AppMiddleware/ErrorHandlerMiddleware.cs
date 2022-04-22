using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Service.UnitOfWork;

namespace MyKidsPlaceStore.AppMiddleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private IServiceUnitOfWork _serviceUnitOfWork;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IWebHostEnvironment env, IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message;
            var exceptionType = exception.GetType().Name;

            if (exceptionType.Contains("ValidationException"))
            {
                status = HttpStatusCode.InternalServerError;
                message = "Somthing Went Wrong Please Contact Your Administrator";

                _serviceUnitOfWork.ApplicationExceptions.Value.WriteException(exception);
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
                message = "Somthing Went Wrong Please Contact Your Administrator";

                _serviceUnitOfWork.ApplicationExceptions.Value.WriteException(exception);
            }

            var result = JsonSerializer.Serialize(message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(result);
        }
    }
}

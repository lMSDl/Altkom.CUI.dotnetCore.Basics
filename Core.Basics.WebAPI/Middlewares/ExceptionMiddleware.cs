using System;
using System.Net;
using System.Threading.Tasks;
using Core.Basics.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Core.Basics.WebAPI.Middleware
{
    public class ExceptionMiddleware {

        private readonly RequestDelegate _request;
        private readonly ILogger _logger;
        public ExceptionMiddleware(RequestDelegate request, ILogger<ExceptionMiddleware> logger)
        {
            _request = request;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext) {
            try {
                await _request(httpContext);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
            
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

                return httpContext.Response.WriteAsync(
                    JsonConvert.SerializeObject(
                        new ErrorDetails {
                                StatusCode = httpContext.Response.StatusCode,
                                Message = ex.Message
                            }));
        }
    }
}
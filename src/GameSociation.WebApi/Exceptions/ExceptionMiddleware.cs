using System;
using System.Net;
using System.Threading.Tasks;
using Account.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace GameSociation.WebApi.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                httpContext.Response.StatusCode = (int) MapToStatusCode(e);
                httpContext.Response.ContentType = "text/plain";
                await httpContext.Response.WriteAsync(e.Message);
                return;
            }
        }

        private static HttpStatusCode MapToStatusCode(Exception ex)
        {
            return ex switch
            {
                InvalidOperationException _ => HttpStatusCode.BadRequest,
                ArgumentNullException _ => HttpStatusCode.BadRequest,
                FormatException _ => HttpStatusCode.BadRequest,
                UnauthorizedException _ => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError
            };
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Timesheet.Domain.Exceptions;

namespace Timesheet.API.Middelwares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            ProblemDetails? responseBody = null;

            //Toutes les exceptions gérées sont à mette dans un nouveau case ici :
            switch (exception)
            {
                case BadRequestException ex:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    responseBody = new()
                    {
                        Status = (int)httpStatusCode,
                        Title = "Bad Request",
                        Detail = ex.Message
                    };
                    break;
                case NonExistingMonthException ex:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    responseBody = new()
                    {
                        Status = (int)httpStatusCode,
                        Title = "Bad Request",
                        Detail = ex.Message
                    };
                    break;
                case ArgumentOutOfRangeException ex:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    responseBody = new()
                    {
                        Status = (int)httpStatusCode,
                        Title = "Bad Request",
                        Detail = ex.Message
                    };
                    break;
                case NoContentException ex:
                    httpStatusCode = HttpStatusCode.NoContent;
                    responseBody = new()
                    {
                        Status = (int)httpStatusCode,
                        Title = "No Content",
                        Detail = ex.Message
                    };
                    break;
                case Exception ex:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    responseBody = new()
                    {
                        Status = (int)httpStatusCode,
                        Title = "Internal Server Error",
                        Detail = ex.Message
                    };
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;

            if (responseBody is null)
            {
                return context.Response.WriteAsJsonAsync(new { error = exception.Message });
            }

            return context.Response.WriteAsJsonAsync(responseBody);

        }
    }
}
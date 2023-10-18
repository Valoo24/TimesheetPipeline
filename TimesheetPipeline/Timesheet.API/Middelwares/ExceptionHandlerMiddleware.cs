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

            var responseBody = string.Empty;

            //Toutes les exceptions gérées sont à mette dans un nouveau case ici :
            switch (exception)
            {
                case BadRequestException ex:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    responseBody = ex.Message;
                    break;
                case NonExistingMonthException ex:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    responseBody = ex.Message;
                    break;
                case NoContentException ex:
                    httpStatusCode = HttpStatusCode.NoContent;
                    responseBody = ex.Message;
                    break;
                case Exception ex:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    responseBody = ex.Message;
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;

            if (responseBody == string.Empty)
            {
                return context.Response.WriteAsJsonAsync(new { error = exception.Message });
            }

            return context.Response.WriteAsJsonAsync(responseBody);

        }
    }
}
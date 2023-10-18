using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Timesheet.API.Middelwares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.NoContent;

                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.NoContent,
                    Type = "Server error",
                    Title = "Server error",
                    Detail = "An internal server hs occured " + ex.Message
                };

                await context.Response.WriteAsJsonAsync(problem);

                context.Response.ContentType = "application/json";
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server error",
                    Title = "Server error",
                    Detail = "An internal server hs occured " + ex.Message
                };

                await context.Response.WriteAsJsonAsync(problem);

                context.Response.ContentType = "application/json";
            }
        }
    }
}
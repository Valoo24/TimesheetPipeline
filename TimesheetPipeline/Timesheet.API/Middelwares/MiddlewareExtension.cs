﻿namespace Timesheet.API.Middelwares
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            //return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using Timesheet.API.Mapper;
using Timesheet.Domain.Exceptions;
using Timesheet.Domain.Interfaces;

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

            switch (exception)
            {
                case ICustomException ex:
                    httpStatusCode = ex.ErrorDetail.HttpStatus;
                    responseBody = ex.ErrorDetail.ToProblemDetails();
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
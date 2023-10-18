using Microsoft.AspNetCore.Mvc;
using Timesheet.Domain.Exceptions;

namespace Timesheet.API.Mapper
{
    public static class ProblemDetailsMapper
    {
        public static ProblemDetails ToProblemDetails(this ExceptionDetail exceptionDetail) 
        {
            return new ProblemDetails()
            {
                Status = exceptionDetail.ErrorCode,
                Title = exceptionDetail.Title,
                Detail = exceptionDetail.Detail
            };
        }
    }
}
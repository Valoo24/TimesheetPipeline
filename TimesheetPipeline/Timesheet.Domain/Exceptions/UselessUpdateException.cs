using System.Net;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Domain.Exceptions
{
    public class UselessUpdateException : Exception, ICustomException
    {
        public ExceptionDetail ErrorDetail { get; set; } = new ExceptionDetail()
        {
            Title = "Bad Request",
            HttpStatus = HttpStatusCode.BadRequest
        };

        public UselessUpdateException() { }
        public UselessUpdateException(string message) : base(message) { }
    }
}
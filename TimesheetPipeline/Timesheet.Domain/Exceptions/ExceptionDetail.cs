using System.Net;

namespace Timesheet.Domain.Exceptions
{
    public class ExceptionDetail
    {
        public string Title { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        HttpStatusCode HttpStatus { get; set; } = HttpStatusCode.InternalServerError;
        public int ErrorCode { get { return (int)HttpStatus; } }
    }
}
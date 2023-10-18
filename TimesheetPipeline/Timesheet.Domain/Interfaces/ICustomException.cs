using Timesheet.Domain.Exceptions;

namespace Timesheet.Domain.Interfaces
{
    public interface ICustomException
    {
        public ExceptionDetail ErrorDetail { get; set; }
    }
}
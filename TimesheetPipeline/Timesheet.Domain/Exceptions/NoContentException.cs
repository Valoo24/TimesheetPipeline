using System.Net;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Domain.Exceptions
{
    public class NoContentException : Exception, ICustomException
    {
        public ExceptionDetail ErrorDetail { get; set; } = new ExceptionDetail()
        {
            Title = "Bad Request",
            HttpStatus = HttpStatusCode.NoContent
        };

        public NoContentException() { }
        public NoContentException(string message) : base(message) 
        {
            ErrorDetail.Detail = this.Message;
        }
        public NoContentException(int id) : base($"L'élément avec l'identifiant {id} n'existe pas.") 
        {
            ErrorDetail.Detail = this.Message;
        }
        public NoContentException(Guid id) : base($"L'élément avec l'identifiant {id} n'existe pas.") 
        {
            ErrorDetail.Detail = this.Message;
        }
        public NoContentException(IEnumerable<Holiday> holidayCollection) : base ("La collection d'entity Holiday demandée est vide.") 
        {
            ErrorDetail.Detail = this.Message;
        }
    }
}
using System.Net;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Domain.Exceptions
{
    public class BadRequestException : Exception, ICustomException
    {
        public ExceptionDetail ErrorDetail { get; set; } = new ExceptionDetail() 
        { 
            Title = "Bad Request",
            HttpStatus = HttpStatusCode.BadRequest
        };

        public BadRequestException() { }
        public BadRequestException(string message) : base(message) 
        {
            ErrorDetail.Detail = this.Message;
        }
        public BadRequestException(int id) : base($"L'élément avec l'identifiant {id} n'existe pas.") 
        {
            ErrorDetail.Detail = this.Message;
        }
        public BadRequestException(Guid id) : base($"L'élément avec l'identifiant {id} n'existe pas.") 
        {
            ErrorDetail.Detail = this.Message;
        }
    }
}
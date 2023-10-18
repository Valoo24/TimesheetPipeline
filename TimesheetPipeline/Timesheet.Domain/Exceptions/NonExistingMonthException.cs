using System.Net;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Domain.Exceptions
{
    public class NonExistingMonthException : Exception, ICustomException
    {
        public ExceptionDetail ErrorDetail { get; set; } = new ExceptionDetail()
        {
            Title = "Bad Request",
            HttpStatus = HttpStatusCode.BadRequest
        };

        public NonExistingMonthException() { }
        public NonExistingMonthException(string message) : base(message) 
        {
            ErrorDetail.Detail = this.Message;
        }
        public NonExistingMonthException(int month) : base(
            month<=0 ? "Le numéro du mois ne peut pas être inférieur ou égal à 0." : "Le numéro du mois ne peut pas être supérieur à 12.") 
        {
            ErrorDetail.Detail = this.Message;
        }
    }
}
using System.Net;

namespace Timesheet.Domain.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() { }
        public BadRequestException(string message) : base(message) { }
        public BadRequestException(int id) : base($"L'élément avec l'identifiant {id} n'existe pas.") { }
        public BadRequestException(Guid id) : base($"L'élément avec l'identifiant {id} n'existe pas.") { }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
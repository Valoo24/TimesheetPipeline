using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Domain.Exceptions
{
    public class NonExistingMonthException : Exception
    { 
        public NonExistingMonthException() { }
        public NonExistingMonthException(string message) : base(message) { }
    }
}
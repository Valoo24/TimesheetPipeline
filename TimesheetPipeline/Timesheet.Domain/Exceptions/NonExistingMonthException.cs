namespace Timesheet.Domain.Exceptions
{
    public class NonExistingMonthException : Exception
    { 
        public NonExistingMonthException() { }
        public NonExistingMonthException(string message) : base(message) { }
    }
}
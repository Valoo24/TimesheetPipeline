namespace Timesheet.Domain.Exceptions
{
    public class UselessUpdateException : Exception
    {
        public UselessUpdateException() { }
        public UselessUpdateException(string message) : base(message) { }
    }
}
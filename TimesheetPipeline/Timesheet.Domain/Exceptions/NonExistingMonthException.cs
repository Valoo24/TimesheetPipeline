namespace Timesheet.Domain.Exceptions
{
    public class NonExistingMonthException : Exception
    { 
        public NonExistingMonthException() { }
        public NonExistingMonthException(string message) : base(message) { }
        public NonExistingMonthException(int month) : base(
            month<=0 ? "Le numéro du mois ne peut pas être inférieur ou égal à 0." : "Le numéro du mois ne peut pas être supérieur à 12.") { }
    }
}
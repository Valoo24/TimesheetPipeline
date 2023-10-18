namespace Timesheet.Domain.Exceptions
{
    public class NoContentException : Exception
    {
        public NoContentException() { }
        public NoContentException(string message) : base(message) { }
        public NoContentException(int id) : base($"L'élément avec l'identifiant {id} n'existe pas") { }
        public NoContentException(Guid id) : base($"L'élément avec l'identifiant {id} n'existe pas") { }
    }
}
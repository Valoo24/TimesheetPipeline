namespace Timesheet.Domain.Entities.Users
{
    /// <summary>
    /// Formulaire de création de nouveau User.
    /// </summary>
    public class UserAddForm
    {
        /// <summary>
        /// Prénom du nouveau User.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Nom de famille du nouveau User.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Adresse Mail du nouveau User.
        /// </summary>
        public string MailAdress { get; set; }

        /// <summary>
        /// Mot de passe du nouveau user
        /// </summary>
        public string Password { get; set; }
    }
}
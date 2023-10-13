namespace Timesheet.Domain.Entities.Users
{
    public class UserUpdateForm
    {
        /// <summary>
        /// Prénom du user modifié.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Nom de famille du user modifié.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Adresse Mail du user modifié.
        /// </summary>
        public string MailAdress { get; set; }

        /// <summary>
        /// Mot de passe du user modifié.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Rôle du User modifié.
        /// </summary>
        public RoleType Role { get; set; }
    }
}
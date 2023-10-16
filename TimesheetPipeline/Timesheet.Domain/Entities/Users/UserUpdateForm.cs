using System.ComponentModel.DataAnnotations;

namespace Timesheet.Domain.Entities.Users
{
    public class UserUpdateForm
    {
        /// <summary>
        /// Prénom du user modifié.
        /// </summary>
        [Required(ErrorMessage = "Le prénom du user est requis")]
        [MinLength(2), MaxLength(30)]
        public string FirstName { get; set; }

        /// <summary>
        /// Nom de famille du user modifié.
        /// </summary>
        [Required(ErrorMessage = "Le nom de famille du user est requis")]
        [MinLength(2), MaxLength(30)]
        public string LastName { get; set; }

        /// <summary>
        /// Adresse Mail du user modifié.
        /// </summary>
        [Required(ErrorMessage = "L'adresse mail du user est requis"), EmailAddress]
        public string MailAdress { get; set; }

        /// <summary>
        /// Mot de passe du user modifié.
        /// </summary>
        [Required(ErrorMessage = "Un mot de passe est requis"), DataType(DataType.Password)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Le mot de passe doit contenir entre 8 et 20 caractères, 1 lettre majuscule, 1 lettre minuscule et 1 nombre.")]
        public string Password { get; set; }

        /// <summary>
        /// Rôle du User modifié.
        /// </summary>
        [Required (ErrorMessage = "Le rôle du user est requis")]
        public RoleType Role { get; set; }
    }
}
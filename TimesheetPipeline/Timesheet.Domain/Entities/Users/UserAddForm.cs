using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Le Prénom du user est requis")]
        [MinLength(2)]
        [MaxLength(30)]
        public string FirstName { get; set; }

        /// <summary>
        /// Nom de famille du nouveau User.
        /// </summary>
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string LastName { get; set; }

        /// <summary>
        /// Adresse Mail du nouveau User.
        /// </summary>
        [Required]
        [EmailAddress]
        public string MailAdress { get; set; }

        /// <summary>
        /// Mot de passe du nouveau user
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Le mot de passe doit contenir entre 8 et 20 caractères, 1 lettre majuscule, 1 lettre minuscule et 1 nombre.")]
        [MinLength(8)]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}
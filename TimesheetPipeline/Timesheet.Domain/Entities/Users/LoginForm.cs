using System.ComponentModel.DataAnnotations;

namespace Timesheet.Domain.Entities.Users
{
    public class LoginForm
    {
        /// <summary>
        /// Adresse Mail du User.
        /// </summary>
        [Required(ErrorMessage = "L'adresse mail du user est requis"), EmailAddress]
        public string MailAdress { get; set; } = string.Empty;

        /// <summary>
        /// Mot de passe du User.
        /// </summary>
        [Required(ErrorMessage = "Le mot de passe est requis"), DataType(DataType.Password)]
        [MinLength(8), MaxLength(20)]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Le mot de passe doit contenir entre 8 et 20 caractères, 1 lettre majuscule, 1 lettre minuscule et 1 nombre.")]
        public string Password { get; set; } = string.Empty;
    }
}
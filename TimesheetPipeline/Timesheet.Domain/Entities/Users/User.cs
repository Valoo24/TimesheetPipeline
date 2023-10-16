using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Domain.Entities.Users
{
    /// <summary>
    /// Rôle possibles pour un user.
    /// </summary>
    public enum RoleType { Admin = 0, Regular = 1, Premium = 2}

    /// <summary>
    /// Objet représentant un User.
    /// </summary>
    public class User : IEntity<Guid>
    {
        /// <summary>
        /// Id du User.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Prénom du User.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Nom de famille du User.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Adresse Mail du User.
        /// </summary>
        public string MailAdress { get; set; }

        /// <summary>
        /// Mot de passe hashé du user.
        /// </summary>
        public string HashedPassword { get; set; }

        /// <summary>
        /// Rôle du user.
        /// </summary>
        public RoleType Role { get; set; }

        /// <summary>
        /// Token du user.
        /// </summary>
        [NotMapped]
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Liste des Timesheets liés au user.
        /// </summary>
        public List<TimesheetEntity> Timesheets { get; set; } = new List<TimesheetEntity>();
    }
}
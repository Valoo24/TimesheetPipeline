using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities.Timesheets;

namespace Timesheet.Domain.Entities.Users
{
    public class UserDTO
    {
        /// <summary>
        /// Id du User.
        /// </summary>
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
        /// Liste des Timesheets liés au user.
        /// </summary>
        public List<TimesheetEntity> Timesheets { get; set; }

        public UserDTO()
        {
            Timesheets = new List<TimesheetEntity>();
        }
    }
}
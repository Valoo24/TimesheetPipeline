using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Domain.Entities.Users
{
    /// <summary>
    /// Formulaire de création de nouveau User.
    /// </summary>
    public class UserAddForm
    {
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
    }
}
﻿using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Domain.Entities.Users
{
    /// <summary>
    /// Objet représentant un User.
    /// </summary>
    public class User : IEntity<Guid>
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

        public List<TimesheetEntity> Timesheets { get; set; }

        public User()
        {
            Timesheets = new List<TimesheetEntity>();
        }
    }
}
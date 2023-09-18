using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities.Users;

namespace Timesheet.Domain.Entities.Timesheets
{
    /// <summary>
    /// Formulaire de création de nouveau Timesheet.
    /// </summary>
    public class TimesheetCreateForm
    {
        /// <summary>
        /// Guid du User lié à la timesheet.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Année de la timesheet.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Mois de la timesheet.
        /// </summary>
        public int Month { get; set; }
    }
}
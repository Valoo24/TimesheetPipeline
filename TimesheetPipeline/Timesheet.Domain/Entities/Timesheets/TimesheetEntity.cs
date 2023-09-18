using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Domain.Entities.Timesheets
{
    /// <summary>
    /// Objet représentant une timesheet.
    /// </summary>
    public class TimesheetEntity : IEntity<Guid>
    {
        /// <summary>
        /// Id de la timesheet.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User de la timesheet.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Année de la timesheet.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Mois de la timesheet.
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Liste d'occupations qui composent la timesheet.
        /// </summary>
        public List<Occupation> OccupationList { get; set; }
    }
}
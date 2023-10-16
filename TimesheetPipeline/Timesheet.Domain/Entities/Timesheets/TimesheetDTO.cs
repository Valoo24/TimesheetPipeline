using System.ComponentModel.DataAnnotations;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Domain.Entities.Timesheets
{
    public class TimesheetDTO : IEntity<Guid>
    {
        /// <summary>
        /// Id de la Timesheet.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Objet User lié à la timesheet.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Année de la timesheet
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Mois de la timesheet.
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Liste des occupations de la timesheet.
        /// </summary>
        public List<Occupation> OccupationList { get; set; }

        public TimesheetDTO()
        {
            OccupationList = new List<Occupation>();
            User = new User();
        }
    }
}
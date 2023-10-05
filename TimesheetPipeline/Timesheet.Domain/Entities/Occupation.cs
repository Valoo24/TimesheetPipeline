using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Domain.Entities
{
    /// <summary>
    /// Objet représentant une occupation dans la timesheet.
    /// </summary>
    public class Occupation : IEntity<int>
    {
        public int Id { get; set; }
        /// <summary>
        /// Date de l'occupation.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Nom de l'occupation.
        /// </summary>
        public string Title { get; set; }

        public Guid TimesheetId { get; set; }
        public TimesheetEntity Timesheet { get; set; }
    }
}
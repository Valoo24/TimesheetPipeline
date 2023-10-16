using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        /// <summary>
        /// Année de la timesheet.
        /// </summary>
        [Required]
        [Range(1, 10_000, ErrorMessage = "L'année ne peut être infrieure à 0 ni supérieure à 9999.")]
        public int Year { get; set; }

        /// <summary>
        /// Mois de la timesheet.
        /// </summary>
        [Required]
        [Range(1, 13, ErrorMessage = "Le mois ne peut être inférieur à 1 ni supérieur à 12.")]
        public int Month { get; set; }
    }
}
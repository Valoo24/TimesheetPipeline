using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Id du User lié à la Timesheet.
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        /// <summary>
        /// User concret de l'entity Timesheet.
        /// </summary>
        public User User { get; set; }

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

        /// <summary>
        /// Liste d'occupations qui composent la timesheet.
        /// </summary>
        public List<Occupation> OccupationList { get; set; } = new List<Occupation>();
    }
}
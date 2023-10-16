using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Domain.Entities
{
    /// <summary>
    /// Objet représentant une occupation dans la timesheet.
    /// </summary>
    public class Occupation : IEntity<int>
    {
        /// <summary>
        /// Id de l'occupation.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Date de l'occupation.
        /// </summary>
        [Required, DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Nom de l'occupation.
        /// </summary>
        [Required]
        [MinLength(2), MaxLength(255)]
        public string Title { get; set; }

        /// <summary>
        /// Id de la Timesheet qui contient cette occupation.
        /// </summary>
        [Required]
        [ForeignKey(nameof(TimesheetEntity))]
        public Guid TimesheetId { get; set; }

        public TimesheetEntity Timesheet { get; set; }
    }
}
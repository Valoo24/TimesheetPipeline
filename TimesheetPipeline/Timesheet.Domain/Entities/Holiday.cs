using System.ComponentModel.DataAnnotations;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Domain.Entities
{
    /// <summary>
    /// Objet représentant un congé légal.
    /// </summary>
    public class Holiday : IEntity<int>
    {
        /// <summary>
        /// Id d'un congé légal.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nom du congé légal.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Date du congé légal.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
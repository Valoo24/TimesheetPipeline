using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Domain.Entities
{
    /// <summary>
    /// Objet représentant une occupation dans la timesheet.
    /// </summary>
    public class Occupation
    {
        /// <summary>
        /// Date de l'occupation.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Nom de l'occupation.
        /// </summary>
        public string Title { get; set; }
    }
}
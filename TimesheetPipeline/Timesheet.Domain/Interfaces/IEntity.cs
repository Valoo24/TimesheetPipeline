using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Domain.Interfaces
{
    /// <summary>
    /// Interface qui doit étendre toutes les Entity concrètes.
    /// </summary>
    /// <typeparam name="TKey">Type que doit implémenter l'Entity</typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// Id de l'entity.
        /// </summary>
        TKey Id { get; set; }
    }
}
